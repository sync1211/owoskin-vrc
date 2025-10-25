using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class InertiaEffectSettings: EffectSettingsBase
    {
        [JsonInclude]
        public float MaxDelta { get; set; } = 20.0f;
        [JsonInclude]
        public float MinDelta { get; set; } = 8.0f;
        [JsonInclude]
        public int Intensity { get; set; } = 100;
        [JsonInclude]
        public bool IgnoreWhenGrounded { get; set; }
        [JsonInclude]
        public bool IgnoreWhenSeated { get; set; }
        [JsonInclude]
        public bool AccelEnabled { get; set; }  = true;
        [JsonInclude]
        public bool DecelEnabled { get; set; } = true;

        [JsonConstructor]
        public InertiaEffectSettings(bool enabled, int priority, float maxDelta, float minDelta, int intensity, bool ignoreWhenGrounded, bool ignoreWhenSeated, bool accelEnabled, bool decelEnabled) : base(enabled, priority)
        {
            MaxDelta = maxDelta;
            MinDelta = minDelta;
            Intensity = intensity;
            IgnoreWhenGrounded = ignoreWhenGrounded;
            IgnoreWhenSeated = ignoreWhenSeated;
            AccelEnabled = accelEnabled;
            DecelEnabled = decelEnabled;
        }

        public InertiaEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority) { }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "inertia.json", "Inertia effect", SettingsHelper.InertiaEffectSettingsContext.Default.InertiaEffectSettings);
        }
    }
}
