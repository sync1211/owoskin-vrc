using Newtonsoft.Json;

namespace OWOVRC.Classes.Settings
{
    public class CollisionEffectSettings
    {
        public bool IsEnabled = true;
        public bool UseVelocity = true;
        public bool AllowContinuous = true;
        public int BaseIntensity = 100;
        public int MinIntensity = 50; // Min intensity when calculating speed
        public int Frequency = 50;
        public float SensationSeconds = 0.3f;
        public float SpeedMultiplier = 200.0f;
        public TimeSpan MaxTimeDiff = TimeSpan.FromSeconds(1);
    }
}
