using Newtonsoft.Json;

namespace OWOVRC.Classes.Settings
{
    public class VelocityEffectSettings
    {
        public double Threshold { get; set; } = 8;
        public double StopVelocityThreshold = 10;
        public double SpeedCap { get; set; } = 200.0;
        public bool IgnoreWhenGrounded { get; set; }
        public bool IgnoreWhenSeated { get; set; }
        public TimeSpan StopVelocityTime { get; set; } = TimeSpan.FromSeconds(1);
    }
}
