using NAudio.Dsp;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
    {
        public enum AudioChannel
        {
            Left,
            Right
        }

        public readonly AnalyzedAudioChannel Left;
        public readonly AnalyzedAudioChannel Right;
        public readonly double Period;

        public AnalyzedAudioSample(Complex[] buffer, double period, int amplification = 1_000)
        {
            Period = period;
            Left = new(buffer, period, AudioChannel.Left, amplification);
            Right = new(buffer, period, AudioChannel.Right, amplification);
        }
    }
}
