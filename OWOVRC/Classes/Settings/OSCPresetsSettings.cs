using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class OSCPresetsSettings: EffectSettingsBase
    {
        [JsonInclude]
        public Dictionary<string, OSCSensationPreset> Presets { get; }
        [JsonInclude]
        public Dictionary<string, OSCAdvancedSensationPreset> AdvancedPresets { get; }
        [JsonInclude]
        public bool AdvancedMode { get; }

        public OSCPresetsSettings(Dictionary<string, OSCSensationPreset>? presets= null, Dictionary<string, OSCAdvancedSensationPreset>? advancedPresets = null, bool advancedMode = false) : base(true, 10)
        {
            Presets = presets ?? [];
            AdvancedPresets = advancedPresets ?? [];
            AdvancedMode = advancedMode;
        }

        [JsonConstructor]
        public OSCPresetsSettings(bool enabled, int priority, Dictionary<string, OSCSensationPreset> presets, Dictionary<string, OSCAdvancedSensationPreset>? advancedPresets = null, bool advancedMode = false) : base(enabled, priority)
        {
            Presets = presets;
            AdvancedPresets = advancedPresets ?? [];
            AdvancedMode = advancedMode;
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "oscPresets.json", "OSC Presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
        }
    }
}
