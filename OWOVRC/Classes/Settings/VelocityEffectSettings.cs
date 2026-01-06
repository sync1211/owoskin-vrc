using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class VelocityEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public float MinSpeed { get; set; } = 8;
        [JsonInclude]
        public float MaxSpeed { get; set; } = 50.0f;
        [JsonInclude]
        public bool IgnoreWhenGrounded { get; set; }
        [JsonInclude]
        public bool IgnoreWhenSeated { get; set; }
        [JsonInclude]
        public int Intensity { get; set; } = 100;

        public VelocityEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority) { }

        [JsonConstructor]
        public VelocityEffectSettings(bool enabled, int priority, float minSpeed, float maxSpeed, bool ignoreWhenGrounded, bool ignoreWhenSeated, int intensity) : base(enabled, priority)
        {
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            IgnoreWhenGrounded = ignoreWhenGrounded;
            IgnoreWhenSeated = ignoreWhenSeated;
            Intensity = intensity;
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "velocity.json", "Velocity effect", SettingsJsonContext.Default.VelocityEffectSettings);
        }
    }
}
