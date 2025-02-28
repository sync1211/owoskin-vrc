using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
    {
        public readonly AnalyzedAudioChannel Left;
        public readonly AnalyzedAudioChannel Right;
        public readonly double Period;

        public AnalyzedAudioSample(Complex[] bufferR, Complex[] bufferL, double period, int amplification = 1_000)
        {
            Period = period;
            Left = new(bufferL, period, amplification);
            Right = new(bufferR, period, amplification);
        }
    }
}
