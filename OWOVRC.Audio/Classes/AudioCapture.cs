using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using Serilog;
using System.ComponentModel;

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

        private readonly WasapiRecorder capture;

        private readonly Complex[] bufferR;
        private readonly Complex[] bufferL;

        private readonly int bytesPerSampleChannel;
        private readonly int bytesPerSample;

        private readonly int bufferLength;

        private Func<ReadOnlySpan<byte>, Complex> CreateComplexFromBuffer = null!;

        public AudioCapture(MMDevice? device = null, bool rawMode = true, int bufferLength = 100, WaveFormat? format = null, uint? processID = null)
        {
            format ??= new WaveFormat(44100, 16, 2);
            WasapiRecorderBuilder recorderBuilder = new WasapiRecorderBuilder()
                .WithLowLatency()
                .WithEventSync()                            // default: event sync (vs WithPollingSync)
                .WithBufferLength(bufferLength)             // default: 100ms
                .WithRawMode();
                //.WithFormat(format);  // default: device mix format;

            if (processID != null)
            {
                recorderBuilder = recorderBuilder.WithProcessLoopback(processID.Value, ProcessLoopbackMode.IncludeTargetProcessTree);
            }
            else if (device != null)
            {
                recorderBuilder = recorderBuilder.WithDevice(device);
            }
            else
            {
                recorderBuilder = recorderBuilder.WithLoopbackCapture();
            }

            if (rawMode)
            {
                recorderBuilder = recorderBuilder.WithRawMode();
            }

            capture = recorderBuilder.Build();

            this.bufferLength = bufferLength;
            bufferR = new Complex[1024];
            bufferL = new Complex[1024];

            bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;

            double fftPeriod = (double)capture.WaveFormat.SampleRate / bufferLength;
            Analyzer = new(bufferR, bufferL, fftPeriod);

            //SelectFormatProcessor(capture.WaveFormat.Encoding);

            capture.DataAvailable += OnDataAvailable;
        }

        private void SelectFormatProcessor(WaveFormatEncoding waveEncoding)
        {
            if (waveEncoding == WaveFormatEncoding.Pcm)
            {
                if (capture.WaveFormat.BitsPerSample == 16)
                {
                    // 16-bit PCM
                    Log.Debug("Audio mode: 16-bit PCM");
                    CreateComplexFromBuffer = CreateComplexFromBuffer_PCM16;
                }
                else if (capture.WaveFormat.BitsPerSample == 32)
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

        public bool Start()
        {
            try
            {
                capture.StartRecording();
            }
            catch (NAudio.CoreAudioApi.CoreAudioException e)
            {
                Log.Error("Exception when starting audio capture.", e);
                return false;
            }

            Log.Information("Audio capture started!");
            return true;
        }

        public void Stop()
        {
            capture.StopRecording();
            Log.Information("Audio capture stopped!");
        }

        private Complex ConvertTmp(ReadOnlySpan<byte> slice)
        {
            return new Complex()
            {
                X = BitConverter.ToSingle(slice),
                Y = 0
            };
        }

        private void OnDataAvailable(ReadOnlySpan<byte> bufferSpan, AudioClientBufferFlags flags, long devicePosition, long qpcPosition)
        {
            if (bufferSpan.Length == 0)
            {
                // Buffer empty -> ignore
                return;
            }

            int sampleCount = Math.Min(bufferSpan.Length / bytesPerSample, bufferR.Length);
            for (int i = 0; i < sampleCount; i++)
            {
                bufferL[i] = ConvertTmp(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                bufferR[i] = ConvertTmp(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel));
            }

            OnSampleRead?.Invoke(this, AnalyzeAudio());
        }

        // 32-bit IEEE float
        private Complex CreateComplexFromBuffer_IeeeFloat32(ReadOnlySpan<byte> sliceX)
        {
            return new Complex()
            {
                X = BitConverter.ToSingle(sliceX) / 21474836480f,
                Y = 0
            };
        }

        // 16-bit PCM
        private Complex CreateComplexFromBuffer_PCM16(ReadOnlySpan<byte> sliceX)
        {
            return new Complex()
            {
                X = BitConverter.ToInt16(sliceX) / 327680f,
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
            FastFourierTransform.FFT(true, (int)Math.Log2(bufferL.Length), bufferR);
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