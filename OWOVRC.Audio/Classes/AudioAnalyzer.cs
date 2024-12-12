using NAudio.Wave;
using System.Numerics;

namespace OWOVRC.Audio.Classes
{
    public class AudioAnalyzer: IDisposable
    {
        private readonly WasapiLoopbackCapture capture;

        private readonly Complex[] buffer;

        public AudioAnalyzer()
        {
            capture = new();
            capture.DataAvailable += OnDataAvailable;
            int sampleRate = capture.WaveFormat.SampleRate;
            buffer = new Complex[sampleRate / 2];
        }

        public void Start()
        {
            capture.StartRecording();
        }

        public void Stop()
        {
            capture.StopRecording();
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            int bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            int bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;
            int sampleCount = Math.Min(e.BytesRecorded / bytesPerSample, buffer.Length);

            WaveFormatEncoding waveEncoding = capture.WaveFormat.Encoding;

            if (waveEncoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSampleChannel == 2)
                {
                    // 16-bit PCM
                    for (int i = 0; i < sampleCount; i++)
                    {
                        buffer[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
                    }
                }
                else if (bytesPerSampleChannel == 4)
                {
                    // 32-bit PCM
                    for (int i = 0; i < sampleCount; i++)
                    {
                        buffer[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
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
                        buffer[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
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
        }

        public AnalyzedAudioFrame? AnalyzeAudio()
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
            double peakFrequency = fftPeriod * peakIndex;

            int subBassLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 16, 60);
            int bassLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 60, 250);
            int lowMidLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 250, 500);
            int midLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 500, 2000);
            int highMidLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 2000, 4000);
            int presenceLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 4000, 6000);
            int brillianceLevel = GetFrequencyRange(fftMagnitude, fftPeriod, 6000, 20_000);

            return new(
                subBassLevel,
                bassLevel,
                lowMidLevel,
                midLevel,
                highMidLevel,
                presenceLevel,
                brillianceLevel
            );
        }

        private int GetFrequencyRange(double[] fftBuffer, double period, int start, int end)
        {
            int actualStart = (int) (start / period);
            int actualEnd = (int) (end / period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(fftBuffer[i], highest);
            }

            int length = end - start;
            return Math.Min((int)(highest * 5000), 100);
        }

        public void Dispose()
        {
            capture.DataAvailable -= OnDataAvailable;
            capture.Dispose();
        }
    }
}
