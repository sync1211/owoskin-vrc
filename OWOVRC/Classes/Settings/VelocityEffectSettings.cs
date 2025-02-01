using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class VelocityEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public float Threshold { get; set; } = 8;
        [JsonInclude]
        public bool ImpactEnabled { get; set; } = true;
        [JsonInclude]
        public float StopVelocityThreshold { get; set; } = 10;
        [JsonInclude]
        public float SpeedCap { get; set; } = 50.0f;
        [JsonInclude]
        public bool IgnoreWhenGrounded { get; set; }
        [JsonInclude]
        public bool IgnoreWhenSeated { get; set; }
        [JsonInclude]
        public TimeSpan StopVelocityTime { get; set; } = TimeSpan.FromSeconds(1);

        public VelocityEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority) { }

        [JsonConstructor]
        public VelocityEffectSettings(bool enabled, int priority, float threshold, bool impactEnabled, float stopVelocityThreshold, float speedCap, bool ignoreWhenGrounded, bool ignoreWhenSeated, TimeSpan stopVelocityTime) : base(enabled, priority)
        {
            Threshold = threshold;
            ImpactEnabled = impactEnabled;
            StopVelocityThreshold = stopVelocityThreshold;
            SpeedCap = speedCap;
            IgnoreWhenGrounded = ignoreWhenGrounded;
            IgnoreWhenSeated = ignoreWhenSeated;
            StopVelocityTime = stopVelocityTime;
        }
    }
}
