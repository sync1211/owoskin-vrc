namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioChannel
    {
        private readonly int Amplification;
        public float SubBass
        {
            get
            {
                return GetFrequencyRange(16, 60);
            }
        }
        public float Bass
        {
            get
            {
                return GetFrequencyRange(60, 250);
            }
        }
        public float LowMid
        {
            get
            {
                return GetFrequencyRange(250, 500);
            }
        }
        public float Mid
        {
            get
            {
                return GetFrequencyRange(500, 2000);
            }
        }
        public float HighMid
        {
            get
            {
                return GetFrequencyRange(2000, 4000);
            }
        }
        public float Presence
        {
            get
            {
                return GetFrequencyRange(4000, 6000);
            }
        }
        public float Brilliance
        {
            get
            {
                return GetFrequencyRange(6000, 20_000);
            }
        }

        private readonly double[] fftBuffer;
        private readonly double period;

        public AnalyzedAudioChannel(double[] fftBuffer, double period, int amplification = 1_000)
        {
            this.fftBuffer = fftBuffer;
            this.period = period;
            Amplification = amplification;
        }

        public float GetFrequency(int frequency)
        {
            int actualFrequency = (int) (frequency / period);
            return (float) (fftBuffer[actualFrequency] * Amplification);
        }

        public float GetFrequencyRange(int start, int end)
        {
            int actualStart = (int)(start / period);
            int actualEnd = (int)(end / period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(fftBuffer[i], highest);
            }

            //int length = end - start;
            return (float)(highest * Amplification);
        }
    }
}
