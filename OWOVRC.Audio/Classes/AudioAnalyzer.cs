using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public static class AudioAnalyzer
    {
        public static float GetFrequency(Complex[] buffer, double period, int frequency)
        {
            int actualFrequency = (int)(frequency / period);
            return buffer[actualFrequency].X;
        }

        public static float GetFrequencyRange(Complex[] buffer, double period, int start, int end)
        {
            int actualStart = (int)(start / period);
            int actualEnd = (int)(end / period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(buffer[i].X, highest);
            }

            return (float) highest;
        }
    }
}
