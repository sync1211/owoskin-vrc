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
#pragma warning disable IDE1006 // Naming conventions
        [JsonIgnore]
        private int frequency { get; set; } = 50; // Set by Frequency property
        [JsonIgnore]
        private float sensationSeconds { get; set; } = 0.3f; // Set by SensationSeconds property
#pragma warning restore IDE1006 // Naming conventions
        [JsonInclude]
        public int Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                UpdateSensation();
            }
        }
        [JsonInclude]
        public float SensationSeconds
        {
            get
            {
                return sensationSeconds;
            }
            set
            {
                sensationSeconds = value;
                UpdateSensation();
            }
        }
        [JsonInclude]
        public float SpeedMultiplier { get; set; } = 2.0f;
        [JsonInclude]
        public TimeSpan MaxTimeDiff { get; set; } = TimeSpan.FromSeconds(1);
        [JsonInclude]
        public Dictionary<int, int> MuscleIntensities { get; } = [];
        [JsonIgnore]
        private Sensation sensation = null!;

        public CollidersEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority)
        {
            foreach (Muscle muscle in Muscle.All)
            {
                MuscleIntensities[muscle.id] = 100;
            }
            UpdateSensation();
        }

        [JsonConstructor]
        public CollidersEffectSettings(bool enabled, int priority, bool useVelocity, bool allowContinuous, int minIntensity, int frequency, float sensationSeconds, float speedMultiplier, TimeSpan maxTimeDiff, Dictionary<int, int> muscleIntensities) : base(enabled, priority)
        {
            UseVelocity = useVelocity;
            AllowContinuous = allowContinuous;
            MinIntensity = minIntensity;
            this.frequency = frequency;
            this.sensationSeconds = Math.Min(0.2f, sensationSeconds);
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
            UpdateSensation();
        }

        private void UpdateSensation()
        {
            sensation = SensationsFactory
                .Create(frequency, sensationSeconds, 100, 0, 0, 0);
        }

        public Sensation GetSensation()
        {
            return sensation
                .WithPriority(Priority);
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
        }
    }
}
