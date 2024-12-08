namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioFrame
    {
        public readonly int SubBass;
        public readonly int Bass;
        public readonly int LowMid;
        public readonly int Mid;
        public readonly int HighMid;
        public readonly int Presence;
        public readonly int Brilliance;

        public AnalyzedAudioFrame(int subBass, int bass, int lowMid, int mid, int highMid, int presence, int brilliance)
        {
            SubBass = subBass;
            Bass = bass;
            LowMid = lowMid;
            Mid = mid;
            HighMid = highMid;
            Presence = presence;
            Brilliance = brilliance;
        }
    }
}
