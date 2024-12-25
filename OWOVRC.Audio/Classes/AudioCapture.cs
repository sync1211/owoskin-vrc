using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using Serilog;

namespace OWOVRC.Audio.Classes
{
    public partial class AudioCapture : IDisposable
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

        private readonly WasapiLoopbackCapture capture;

        private readonly Complex[] leftBuffer;
        private readonly Complex[] rightBuffer;

        private readonly int bytesPerSampleChannel;
        private readonly int bytesPerSample;
        private readonly WaveFormatEncoding waveEncoding;

        private readonly int bufferLength;

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
            leftBuffer = new Complex[bufferLength];
            rightBuffer = new Complex[bufferLength];

            bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;
            waveEncoding = capture.WaveFormat.Encoding;

            RegisterDataAvaiableEvent();
        }

        private void RegisterDataAvaiableEvent()
        {
            if (waveEncoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSampleChannel == 2)
                {
                    // 16-bit PCM
                    Log.Debug("Audio mode: 16-bit PCM");
                    capture.DataAvailable += OnDataAvailable_PCM16;
                }
                else if (bytesPerSampleChannel == 4)
                {
                    // 32-bit PCM
                    Log.Debug("Audio mode: 32-bit PCM");
                    capture.DataAvailable += OnDataAvailable_PCM32;
                }
                else
                {
                    // Not supported!
                    throw new Exception("Unsupported PCM format!");
                }
            }
            else if (waveEncoding == WaveFormatEncoding.IeeeFloat && (bytesPerSampleChannel == 4))
            {
                // 32-bit IEEE float
                Log.Debug("Audio mode: 32-bit IEEE float");
                capture.DataAvailable += OnDataAvailable_IeeeFloat32;
            }
            else
            {
                // Not supported!
                throw new Exception("Unsupported encoding!");
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

        // 32-bit IEEE float
        private void OnDataAvailable_IeeeFloat32(object? sender, WaveInEventArgs e)
        {
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, leftBuffer.Length);

            ReadOnlySpan<byte> bufferSpan = e.Buffer.AsSpan(0, e.BytesRecorded);

            for (int i = 0; i < sampleCount; i++)
            {
                leftBuffer[i] = new Complex() { X = BitConverter.ToSingle(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel)), Y = 0 };
                rightBuffer[i] = new Complex() { X = BitConverter.ToSingle(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel)), Y = 0 };
            }

            // Set remaining values to 0
            for (int i = sampleCount; i < leftBuffer.Length; i++)
            {
                leftBuffer[i] = new Complex();
                rightBuffer[i] = new Complex();
            }

            OnSampleRead?.Invoke(this, AnalyzeAudioStereo());
        }

        // 16-bit PCM
        private void OnDataAvailable_PCM16(object? sender, WaveInEventArgs e)
        {
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, leftBuffer.Length);

            ReadOnlySpan<byte> bufferSpan = e.Buffer.AsSpan(0, e.BytesRecorded);

            for (int i = 0; i < sampleCount; i++)
            {
                leftBuffer[i] = new Complex() { X = BitConverter.ToInt16(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel)), Y = 0 };
                rightBuffer[i] = new Complex() { X = BitConverter.ToInt16(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel)), Y = 0 };
            }

            // Set remaining values to 0
            for (int i = sampleCount; i < leftBuffer.Length; i++)
            {
                leftBuffer[i] = new Complex();
                rightBuffer[i] = new Complex();
            }

            OnSampleRead?.Invoke(this, AnalyzeAudioStereo());
        }

        // 32-bit PCM
        private void OnDataAvailable_PCM32(object? sender, WaveInEventArgs e)
        {
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, leftBuffer.Length);

            ReadOnlySpan<byte> bufferSpan = e.Buffer.AsSpan(0, e.BytesRecorded);

            for (int i = 0; i < sampleCount; i++)
            {
                leftBuffer[i] = new Complex() { X = BitConverter.ToInt32(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel)), Y = 0 };
                rightBuffer[i] = new Complex() { X = BitConverter.ToInt32(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel)), Y = 0 };
            }

            // Set remaining values to 0
            for (int i = sampleCount; i < leftBuffer.Length; i++)
            {
                leftBuffer[i] = new Complex();
                rightBuffer[i] = new Complex();
            }

            OnSampleRead?.Invoke(this, AnalyzeAudioStereo());
        }

        public AnalyzedAudioSample AnalyzeAudioStereo()
        {
            FastFourierTransform.FFT(true, (int)Math.Log2(leftBuffer.Length), leftBuffer);
            FastFourierTransform.FFT(true, (int)Math.Log2(rightBuffer.Length), rightBuffer);

            double fftPeriod = (double)capture.WaveFormat.SampleRate / rightBuffer.Length;

            return new(leftBuffer, rightBuffer, fftPeriod);
        }

        public void Dispose()
        {
            capture.DataAvailable -= OnDataAvailable_PCM16;
            capture.DataAvailable -= OnDataAvailable_PCM32;
            capture.DataAvailable -= OnDataAvailable_IeeeFloat32;

            capture.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}