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
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.SubBass.Start, AudioSpectrum.SubBass.End) * Amplification;
            }
        }
        public float Bass
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.Bass.Start, AudioSpectrum.Bass.End) * Amplification;
            }
        }
        public float LowMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.LowMid.Start, AudioSpectrum.LowMid.End) * Amplification;
            }
        }
        public float Mid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.Mid.Start, AudioSpectrum.Mid.End) * Amplification;
            }
        }
        public float HighMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.HighMid.Start, AudioSpectrum.HighMid.End) * Amplification;
            }
        }
        public float Presence
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.Presence.Start, AudioSpectrum.Presence.End) * Amplification;
            }
        }
        public float Brilliance
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(fftBuffer, period, AudioSpectrum.Brilliance.Start, AudioSpectrum.Brilliance.End) * Amplification;
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
