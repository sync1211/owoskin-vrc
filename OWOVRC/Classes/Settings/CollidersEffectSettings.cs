using OWOGame;
using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class CollidersEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public bool UseVelocity { get; set; } = true;
        [JsonInclude]
        public bool AllowContinuous { get; set; } = true;
        [JsonInclude]
        public int MinIntensity { get; set; } = 50; // Min intensity when calculating speed
        [JsonInclude]
        public int Frequency { get; set; } = 50;
        [JsonInclude]
        public float SensationSeconds { get; set; } = 0.3f;
        [JsonInclude]
        public float SpeedMultiplier { get; set; } = 2.0f;
        [JsonInclude]
        public TimeSpan MaxTimeDiff { get; set; } = TimeSpan.FromSeconds(1);
        [JsonInclude]
        public Dictionary<int, int> MuscleIntensities { get; } = [];

        public CollidersEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority)
        {
            foreach (Muscle muscle in Muscle.All)
            {
                MuscleIntensities[muscle.id] = 100;
            }
        }

        [JsonConstructor]
        public CollidersEffectSettings(bool enabled, int priority, bool useVelocity, bool allowContinuous, int minIntensity, int frequency, float sensationSeconds, float speedMultiplier, TimeSpan maxTimeDiff, Dictionary<int, int> muscleIntensities) : base(enabled, priority)
        {
            UseVelocity = useVelocity;
            AllowContinuous = allowContinuous;
            MinIntensity = minIntensity;
            Frequency = frequency;
            SensationSeconds = sensationSeconds;
            SpeedMultiplier = speedMultiplier;
            MaxTimeDiff = maxTimeDiff;
            MuscleIntensities = muscleIntensities ?? [];

            foreach (Muscle muscle in Muscle.All)
            {
                if (!MuscleIntensities.ContainsKey(muscle.id))
                {
                    MuscleIntensities[muscle.id] = 100;
                }
            }
        }

        public Sensation CreateSensation()
        {
            return SensationsFactory
                .Create(Frequency, SensationSeconds, 100, 0, 0, 0)
                .WithPriority(Priority);
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
        }
    }
}
