using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using Serilog;

namespace OWOVRC.Audio.Classes
{
    public class AudioCapture : IDisposable
    {
        public EventHandler<AnalyzedAudioSample>? OnSampleRead;
        public bool IsListening
        {
            get
            {
                return capture.CaptureState == CaptureState.Capturing;
            }
        }
        public CaptureState CaptureState
        {
            get
            {
                return capture.CaptureState;
            }
        }

        public readonly AnalyzedAudioSample Analyzer;

        private readonly WasapiLoopbackCapture capture;

        private readonly Complex[] bufferR;
        private readonly Complex[] bufferL;

        private readonly int bytesPerSampleChannel;
        private readonly int bytesPerSample;

        private readonly int bufferLength;

        private Func<ReadOnlySpan<byte>, Complex> CreateComplexFromBuffer = null!;

        public AudioCapture(MMDevice? device = null)
        {
            if (device != null)
            {
                capture = new(device);
            }
            else
            {
                capture = new();
            }

            int sampleRate = capture.WaveFormat.SampleRate;

            bufferLength = sampleRate / 2;
            bufferR = new Complex[bufferLength];
            bufferL = new Complex[bufferLength];

            bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;

            double fftPeriod = (double)capture.WaveFormat.SampleRate / bufferLength;
            Analyzer = new(bufferR, bufferL, fftPeriod);

            SelectFormatProcessor(capture.WaveFormat.Encoding);

            capture.DataAvailable += OnDataAvailable;
        }

        private void SelectFormatProcessor(WaveFormatEncoding waveEncoding)
        {
            if (waveEncoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSampleChannel == 2)
                {
                    // 16-bit PCM
                    Log.Debug("Audio mode: 16-bit PCM");
                    CreateComplexFromBuffer = CreateComplexFromBuffer_PCM16;
                }
                else if (bytesPerSampleChannel == 4)
                {
                    // 32-bit PCM
                    Log.Debug("Audio mode: 32-bit PCM");
                    CreateComplexFromBuffer = CreateComplexFromBuffer_PCM32;
                }
                else
                {
                    // Not supported!
                    throw new FormatException("Unsupported PCM format!");
                }
            }
            else if (waveEncoding == WaveFormatEncoding.IeeeFloat && (bytesPerSampleChannel == 4))
            {
                // 32-bit IEEE float
                Log.Debug("Audio mode: 32-bit IEEE float");
                CreateComplexFromBuffer = CreateComplexFromBuffer_IeeeFloat32;
            }
            else
            {
                // Not supported!
                throw new FormatException("Unsupported encoding!");
            }
        }

        public void Start()
        {
            capture.StartRecording();
            Log.Information("Audio capture started!");
        }

        public void Stop()
        {
            capture.StopRecording();
            Log.Information("Audio capture stopped!");
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, bufferR.Length);

            ReadOnlySpan<byte> bufferSpan = e.Buffer.AsSpan(0, e.BytesRecorded);

            for (int i = 0; i < sampleCount; i++)
            {
                bufferL[i] = CreateComplexFromBuffer(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                bufferR[i] = CreateComplexFromBuffer(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel));
            }

            OnSampleRead?.Invoke(this, AnalyzeAudio());
        }

        // 32-bit IEEE float
        private Complex CreateComplexFromBuffer_IeeeFloat32(ReadOnlySpan<byte> sliceX)
        {
            return new Complex()
            {
                X = BitConverter.ToSingle(sliceX),
                Y = 0
            };
        }

        // 16-bit PCM
        private Complex CreateComplexFromBuffer_PCM16(ReadOnlySpan<byte> sliceX)
        {
            return new Complex()
            {
                X = BitConverter.ToInt16(sliceX),
                Y = 0
            };
        }

        // 32-bit PCM
        private Complex CreateComplexFromBuffer_PCM32(ReadOnlySpan<byte> sliceX)
        {
            return new Complex()
            {
                X = BitConverter.ToInt32(sliceX),
                Y = 0
            };
        }

        public AnalyzedAudioSample AnalyzeAudio()
        {
            FastFourierTransform.FFT(true, (int)Math.Log2(bufferR.Length), bufferR);
            FastFourierTransform.FFT(true, (int)Math.Log2(bufferL.Length), bufferL);

            return Analyzer;
        }

        private async Task DisposeAsync()
        {
            // Wait for audio capture to stop without blocking the thread used by the audio capture.
            while (capture.CaptureState == CaptureState.Stopping)
            {
                Log.Information("DISPOSE: Waiting for audio capture to stop...");
                await Task.Delay(1000)
                    .ConfigureAwait(false);
            }

            Dispose();
        }

        public void Dispose()
        {
            capture.DataAvailable -= OnDataAvailable;

            if (capture.CaptureState != CaptureState.Stopped)
            {
                capture.StopRecording();
            }

            if (capture.CaptureState == CaptureState.Stopping)
            {
                // Wait asynchronously for audio capture to stop and call Dispose again.
                // Can't be done via Thread.Sleep as it blocks the thread used by the audio capture.
                _ = Task.Run(DisposeAsync);
                return;
            }

            capture.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}