using NAudio.Dsp;
using NAudio.Wave;

namespace OWOVRC.Audio.Classes
{
    public class AudioAnalyzer: IDisposable
    {
        private readonly WasapiLoopbackCapture capture;
        private const int M = 6; //TODO: Comment what this value does during FFT calculation!

        private WaveBuffer? buffer;

        public AudioAnalyzer()
        {
            capture = new();
            capture.DataAvailable += OnDataAvailable;
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
            buffer = new(e.Buffer);
        }

        public AnalyzedAudioFrame? AnalyzeAudio()
        {
            if (buffer == null)
            {
                return null;
            }

            int bufferLength = buffer.FloatBuffer.Length / 8;

            // Perform FFT
            Complex[] fftBuffer = new Complex[bufferLength];
            for (int i = 0; i < bufferLength; i++)
            {
                float bufferVar = buffer.FloatBuffer[i];
                fftBuffer[i] = new()
                {
                    Y = 0,
                    X = bufferVar
                };
            }

            FastFourierTransform.FFT(true, M, fftBuffer);

            int subBassLevel    = GetFrequencyRange(fftBuffer, bufferLength, 16, 60);
            int bassLevel       = GetFrequencyRange(fftBuffer, bufferLength, 60, 250);
            int lowMidLevel     = GetFrequencyRange(fftBuffer, bufferLength, 250, 500);
            int midLevel        = GetFrequencyRange(fftBuffer, bufferLength, 500, 2000);
            int highMidLevel    = GetFrequencyRange(fftBuffer, bufferLength, 2000, 4000);
            int presenceLevel   = GetFrequencyRange(fftBuffer, bufferLength, 4000, 6000);
            int brillianceLevel = GetFrequencyRange(fftBuffer, bufferLength, 6000, 20_000);

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

        private int GetFrequencyRange(Complex[] fftBuffer, int bufferLength, int start, int end)
        {
            int sampleRate = capture.WaveFormat.SampleRate;

            int startIndex = (int)((float) start / sampleRate * bufferLength);
            int endIndex = (int)((float) end / sampleRate * bufferLength);
            float level = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                level += (float)Math.Sqrt(fftBuffer[i].X * fftBuffer[i].X + fftBuffer[i].Y * fftBuffer[i].Y);
            }

            // Maximum level
            //TODO: Clarify how we calculate this!
            float maxLevel = (end - start) / M;

            // Convert to percentage value
            float percentage = (level / maxLevel) * 100;

            //TODO: Is this a percentage or how should we interpret this value?
            return (int) Math.Min(percentage, 100);
        }

        public float GetVolume()
        {
            if (buffer == null)
            {
                return 0;
            }
            float volume = 0;
            for (int i = 0; i < buffer.FloatBuffer.Length; i++)
            {
                volume += Math.Abs(buffer.FloatBuffer[i]);
            }
            return volume;
        }

        public float GetFrequencyRange(Complex[] buffer, int start, int end)
        {
            int sampleRate = capture.WaveFormat.SampleRate;

            int startIndex = (int) (start / (double) (sampleRate * buffer.Length));
            int endIndex = (int) (end / (double) (sampleRate * buffer.Length));

            float frequencyLevel = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                frequencyLevel += (float) Math.Abs(buffer[i].X);
            }

            return frequencyLevel;
        }

        public void Dispose()
        {
            capture.DataAvailable -= OnDataAvailable;
            capture.Dispose();
        }
    }
}
