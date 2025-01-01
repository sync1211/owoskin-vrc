using OWOVRC.Classes.Effects.OSCPresets;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class OSCPresetsSettings: EffectSettingsBase
    {
        [JsonInclude]
        public Dictionary<string, OSCSensationPreset> Presets { get; }

        public OSCPresetsSettings(Dictionary<string, OSCSensationPreset>? presets=null) : base(true, 10)
        {
            Presets = presets ?? [];
        }

        [JsonConstructor]
        public OSCPresetsSettings(bool enabled, int priority, Dictionary<string, OSCSensationPreset> presets) : base(enabled, priority)
        {
            Presets = presets;
        }
    }
}
