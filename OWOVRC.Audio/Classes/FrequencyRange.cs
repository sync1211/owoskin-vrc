namespace OWOVRC.Audio.Classes
{
    public readonly struct FrequencyRange
    {
        public readonly int Start;
        public readonly int End;

        public FrequencyRange(int startFrequency, int endFrequency)
        {
            Start = startFrequency;
            End = endFrequency;
        }
    }
}
