using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
    {
        public readonly AnalyzedAudioChannel Left;
        public readonly AnalyzedAudioChannel Right;
        public readonly double Period;

        public AnalyzedAudioSample(Complex[] leftBuffer, Complex[] rightBuffer, double period, int amplification = 1_000)
        {
            Period = period;
            Left = new(leftBuffer, period, amplification);
            Right = new(rightBuffer, period, amplification);
        }
    }
}
