using FftSharp.Windows;
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

        public AudioAnalyzer()
        {
            capture = new();
            capture.DataAvailable += OnDataAvailable;
            int sampleRate = capture.WaveFormat.SampleRate;
            leftBuffer = new Complex[sampleRate / 2];
            rightBuffer = new Complex[sampleRate / 2];

            bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;
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
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, leftBuffer.Length);

            WaveFormatEncoding waveEncoding = capture.WaveFormat.Encoding;

            if (waveEncoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSampleChannel == 2)
                {
                    // 16-bit PCM
                    for (int i = 0; i < sampleCount; i++)
                    {
                        leftBuffer[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
                        rightBuffer[i] = BitConverter.ToInt16(e.Buffer, (i * bytesPerSample) + bytesPerSampleChannel);
                    }
                }
                else if (bytesPerSampleChannel == 4)
                {
                    // 32-bit PCM
                    for (int i = 0; i < sampleCount; i++)
                    {
                        leftBuffer[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
                        rightBuffer[i] = BitConverter.ToInt32(e.Buffer, (i * bytesPerSample) + bytesPerSampleChannel);
                    }
                }
                else
                {
                    // Not supported!
                    throw new Exception("Unsupported PCM format!");
                }
            }
            else if (waveEncoding == WaveFormatEncoding.IeeeFloat)
            {
                if (bytesPerSampleChannel == 4)
                {
                    // 32-bit IEEE float
                    for (int i = 0; i < sampleCount; i++)
                    {
                        leftBuffer[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
                        rightBuffer[i] = BitConverter.ToSingle(e.Buffer, (i * bytesPerSample) + bytesPerSampleChannel);
                    }
                }
                else
                {
                    // Not supported!
                    throw new Exception("Unsupported IEEE format!");
                }
            }
            else
            {
                // Not supported!
                throw new Exception("Unsupported encoding!");
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
            capture.DataAvailable -= OnDataAvailable;
            capture.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
