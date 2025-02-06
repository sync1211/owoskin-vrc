using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;
using Windows.UI.ViewManagement;

namespace OWOVRC.Classes.Settings
{
    public class WorldIntegratorSettings : EffectSettingsBase
    {
        [JsonInclude]
        public int UpdateInterval { get; set; } = 100;
        [JsonInclude]
        public int Intensity { get; set; } = 100;
        [JsonInclude]
        public Dictionary<string, bool> EnabledSensations { get; } = new()
        {
            { "Back Wind", true },
            { "Death", true },
            { "Front Wind", true },
            { "Massage", true },
            { "PaintBall", true },
            { "Raindrop", true },
            { "Recoil", true },
            { "Sword Bleeding", true },
            { "Sword Stab", true },
            { "Weight", true }
        };

        public WorldIntegratorSettings() : base(true, 10) { }
        [JsonConstructor]
        public WorldIntegratorSettings(bool enabled, int priority = 10, Dictionary<string, bool>? enabledSensations = null) : base(enabled, priority)
        {
            EnabledSensations = enabledSensations ?? EnabledSensations;
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "owi.json", "OWO World Integrator", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
        }
    }
}
