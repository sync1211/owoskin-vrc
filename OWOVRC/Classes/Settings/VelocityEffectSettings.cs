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

        public VelocityEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority) { }

        [JsonConstructor]
        public VelocityEffectSettings(bool enabled, int priority, float minSpeed, float maxSpeed, bool ignoreWhenGrounded, bool ignoreWhenSeated) : base(enabled, priority)
        {
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            IgnoreWhenGrounded = ignoreWhenGrounded;
            IgnoreWhenSeated = ignoreWhenSeated;
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "velocity.json", "velocity effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings);
        }
    }
}
