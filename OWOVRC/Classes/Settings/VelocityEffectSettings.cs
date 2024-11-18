using Newtonsoft.Json;

namespace OWOVRC.Classes.Settings
{
    public class VelocityEffectSettings
    {
        public bool Enabled { get; set; } = true;
        public float Threshold { get; set; } = 8;
        public bool ImpactEnabled { get; set; } = true;
        public float StopVelocityThreshold { get; set; } = 10;
        public float SpeedCap { get; set; } = 200.0f;
        public bool IgnoreWhenGrounded { get; set; }
        public bool IgnoreWhenSeated { get; set; }
        public TimeSpan StopVelocityTime { get; set; } = TimeSpan.FromSeconds(1);
    }
}
