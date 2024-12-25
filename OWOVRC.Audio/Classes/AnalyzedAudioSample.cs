namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
    {
        public readonly AnalyzedAudioChannel Left;
        public readonly AnalyzedAudioChannel Right;
        public readonly double Period;

        public AnalyzedAudioSample(double[] leftBuffer, double[] rightBuffer, double period, int amplification = 1_000)
        {
            Period = period;
            Left = new(leftBuffer, period, amplification);
            Right = new(rightBuffer, period, amplification);
        }
    }
}
