using OWOGame;
using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class WorldIntegratorSettings : EffectSettingsBase
    {
        [JsonInclude]
        public int UpdateInterval { get; set; } = 100;
        [JsonInclude]
        public Dictionary<int, int> MuscleIntensities { get; } = [];

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

        public WorldIntegratorSettings() : base(enabled: true, priority: 10)
        {
            AddMissingMuscles();
        }

        [JsonConstructor]
        public WorldIntegratorSettings(bool enabled, int priority = 10, Dictionary<string, int>? enabledSensations = null, Dictionary<int, int>? muscleIntensities = null) : base(enabled, priority)
        {
            MuscleIntensities = muscleIntensities ?? MuscleIntensities;
            EnabledSensations = enabledSensations ?? EnabledSensations;

            AddMissingMuscles();
        }

        private void AddMissingMuscles()
        {
            foreach (Muscle muscle in Muscle.All)
            {
                if (!MuscleIntensities.ContainsKey(muscle.id))
                {
                    MuscleIntensities[muscle.id] = 100;
                }
            }
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "owi.json", "OWO World Integrator", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
        }
    }
}
