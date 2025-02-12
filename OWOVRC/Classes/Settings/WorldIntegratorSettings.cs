﻿using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class WorldIntegratorSettings : EffectSettingsBase
    {
        [JsonInclude]
        public int UpdateInterval { get; set; } = 100;
        [JsonInclude]
        public int Intensity { get; set; } = 100;
        [JsonInclude]
        public Dictionary<string, int> EnabledSensations { get; } = new()
        {
            { "Back Wind", 100 },
            { "Death", 100 },
            { "Front Wind", 100 },
            { "Massage", 100 },
            { "PaintBall", 100 },
            { "Raindrop", 100 },
            { "Recoil", 100 },
            { "Sword Bleeding", 100 },
            { "Sword Stab", 100 },
            { "Weight", 100 }
        };

        public WorldIntegratorSettings() : base(true, 10) { }
        [JsonConstructor]
        public WorldIntegratorSettings(bool enabled, int priority = 10, Dictionary<string, int>? enabledSensations = null) : base(enabled, priority)
        {
            EnabledSensations = enabledSensations ?? EnabledSensations;
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "owi.json", "OWO World Integrator", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
        }
    }
}
