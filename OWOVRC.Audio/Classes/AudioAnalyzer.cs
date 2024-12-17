using NAudio.CoreAudioApi;
using NAudio.Wave;
using Serilog;
using System.Numerics;

namespace OWOVRC.Audio.Classes
{
    public partial class AudioAnalyzer: IDisposable
    {
        public EventHandler<Tuple<AnalyzedAudioSample, AnalyzedAudioSample>>? OnSampleRead;
        public bool IsListening
        {
            get
            {
                return capture.CaptureState == CaptureState.Capturing;
            }
        }

        private readonly WasapiLoopbackCapture capture;

        private readonly Complex[] leftBuffer;
        private readonly Complex[] rightBuffer;

        private readonly int bytesPerSampleChannel;
        private readonly int bytesPerSample;
        private readonly WaveFormatEncoding waveEncoding;

        public AudioAnalyzer()
        {
            capture = new();
            int sampleRate = capture.WaveFormat.SampleRate;
            leftBuffer = new Complex[sampleRate / 2];
            rightBuffer = new Complex[sampleRate / 2];

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
                leftBuffer[i] = BitConverter.ToSingle(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                rightBuffer[i] = BitConverter.ToSingle(bufferSpan.Slice(i * bytesPerSample + bytesPerSampleChannel, bytesPerSampleChannel));
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
                leftBuffer[i] = BitConverter.ToInt16(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                rightBuffer[i] = BitConverter.ToInt16(bufferSpan.Slice(i * bytesPerSample + bytesPerSampleChannel, bytesPerSampleChannel));
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
                leftBuffer[i] = BitConverter.ToInt32(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                rightBuffer[i] = BitConverter.ToInt32(bufferSpan.Slice(i * bytesPerSample + bytesPerSampleChannel, bytesPerSampleChannel));
            }

            OnSampleRead?.Invoke(this, AnalyzeAudioStereo());
        }

        public Tuple<AnalyzedAudioSample, AnalyzedAudioSample> AnalyzeAudioStereo()
        {
            return Tuple.Create(
                AnalyzeAudio(leftBuffer),
                AnalyzeAudio(rightBuffer)
            );
        }

        private AnalyzedAudioSample AnalyzeAudio(Complex[] buffer)
        {
            Complex[] paddedBuffer = FftSharp.Pad.ZeroPad(buffer);
            FftSharp.FFT.Forward(paddedBuffer);
            double[] fftMagnitude = FftSharp.FFT.Magnitude(paddedBuffer);

            // find the frequency peak
            int peakIndex = 0;
            for (int i = 0; i < fftMagnitude.Length; i++)
            {
                if (fftMagnitude[i] > fftMagnitude[peakIndex])
                    peakIndex = i;
            }
            double fftPeriod = FftSharp.FFT.FrequencyResolution(fftMagnitude.Length, capture.WaveFormat.SampleRate);
            //double peakFrequency = fftPeriod * peakIndex;

            return new(fftMagnitude, fftPeriod);
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
