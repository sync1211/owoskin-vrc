namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioFrame
    {
        public int SubBass
        {
            get
            {
                return GetFrequencyRange(16, 60);
            }
        }
        public int Bass
        {
            get
            {
                return GetFrequencyRange(60, 250);
            }
        }
        public int LowMid
        {
            get
            {
                return GetFrequencyRange(250, 500);
            }
        }
        public int Mid
        {
            get
            {
                return GetFrequencyRange(500, 2000);
            }
        }
        public int HighMid
        {
            get
            {
                return GetFrequencyRange(2000, 4000);
            }
        }
        public int Presence
        {
            get
            {
                return GetFrequencyRange(4000, 6000);
            }
        }
        public int Brilliance
        {
            get
            {
                return GetFrequencyRange(6000, 20_000);
            }
        }

        private readonly double[] fftBuffer;
        private readonly double period;

        public AnalyzedAudioFrame(double[] fftBuffer, double period)
        {
            this.fftBuffer = fftBuffer;
            this.period = period;
        }

        public int GetFrequency(int frequency)
        {
            int actualFrequency = (int) (frequency / period);
            return Math.Min((int)(fftBuffer[actualFrequency] * 5000), 100);
        }

        public int GetFrequencyRange(int start, int end)
        {
            int actualStart = (int)(start / period);
            int actualEnd = (int)(end / period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(fftBuffer[i], highest);
            }

            //int length = end - start;
            return Math.Min((int)(highest * 5000), 100);
        }
    }
}
