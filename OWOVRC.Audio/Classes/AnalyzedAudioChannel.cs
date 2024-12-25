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
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.SubBass.Start, AudioSpectrum.SubBass.End) * Amplification;
            }
        }
        public float Bass
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.Bass.Start, AudioSpectrum.Bass.End) * Amplification;
            }
        }
        public float LowMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.LowMid.Start, AudioSpectrum.LowMid.End) * Amplification;
            }
        }
        public float Mid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.Mid.Start, AudioSpectrum.Mid.End) * Amplification;
            }
        }
        public float HighMid
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.HighMid.Start, AudioSpectrum.HighMid.End) * Amplification;
            }
        }
        public float Presence
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.Presence.Start, AudioSpectrum.Presence.End) * Amplification;
            }
        }
        public float Brilliance
        {
            get
            {
                return AudioAnalyzer.GetFrequencyRange(Buffer, Period, AudioSpectrum.Brilliance.Start, AudioSpectrum.Brilliance.End) * Amplification;
            }
        }

        public readonly Complex[] Buffer;
        public readonly double Period;

        public AnalyzedAudioChannel(Complex[] fftBuffer, double period, int amplification = 1_000)
        {
            this.Buffer = fftBuffer;
            this.Period = period;
            Amplification = amplification;
        }
    }
}
