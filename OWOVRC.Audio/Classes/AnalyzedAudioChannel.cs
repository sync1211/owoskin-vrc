using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioChannel
    {
        private readonly int Amplification;
        public float SubBass
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 16, 60) * Amplification;
            }
        }
        public float Bass
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 60, 250) * Amplification;
            }
        }
        public float LowMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 250, 500) * Amplification;
            }
        }
        public float Mid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 500, 2000) * Amplification;
            }
        }
        public float HighMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 2000, 4000) * Amplification;
            }
        }
        public float Presence
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 4000, 6000) * Amplification;
            }
        }
        public float Brilliance
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, 6000, 20_000) * Amplification;
            }
        }

        private readonly Complex[] fftBuffer;
        private readonly double period;

        public AnalyzedAudioChannel(Complex[] fftBuffer, double period, int amplification = 1_000)
        {
            this.fftBuffer = fftBuffer;
            this.period = period;
            Amplification = amplification;
        }
    }
}
