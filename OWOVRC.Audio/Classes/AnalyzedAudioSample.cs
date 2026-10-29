using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
    {
        public readonly AnalyzedAudioChannel Left;
        public readonly AnalyzedAudioChannel Right;
        public readonly double Period;

        public AnalyzedAudioSample(Complex[] bufferR, Complex[] bufferL, double period, int amplification = 100, bool useHammingWindow = true)
        {
            float[] hammingWindow = useHammingWindow
               ? CreateHammingWindow(bufferL.Length)
               : CreateEmptyWindow(bufferL.Length);

            Period = period;
            Left = new(bufferL, period, hammingWindow, amplification);
            Right = new(bufferR, period, hammingWindow, amplification);
        }

        private static float[] CreateEmptyWindow(int length)
        {
            float[] window = new float[length];
            for (int i = 0; i < length; i++)
            {
                window[i] = 1;
            }
            return window;
        }

          private static float[] CreateHammingWindow(int length)
        {
            float[] window = new float[length];

            float N = length - 1f;
            for (int i = 0; i < length; i++)
            {
                // W(i) = 0.54 - 0.46 * cos(2π * (i/N)), 0 <= i <= N
                // Where N = length - 1
                // Source: https://www.mathworks.com/help/signal/ref/hamming.html
                window[i] = 0.54f - (0.46f * MathF.Cos((2f * MathF.PI) * (i / N)));
            }
            return window;
        }
    }
}
