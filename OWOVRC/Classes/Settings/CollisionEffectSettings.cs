namespace OWOVRC.Classes.Settings
{
    public class CollisionEffectSettings : EffectSettingsBase
    {
        public bool UseVelocity { get; set; } = true;
        public bool AllowContinuous { get; set; } = true;
        public int BaseIntensity { get; set; } = 100;
        public int MinIntensity { get; set; } = 50; // Min intensity when calculating speed
        public int Frequency { get; set; } = 50;
        public float SensationSeconds { get; set; } = 0.3f;
        public float SpeedMultiplier { get; set; } = 200.0f;
        public TimeSpan MaxTimeDiff { get; set; } = TimeSpan.FromSeconds(1);
    }
}
