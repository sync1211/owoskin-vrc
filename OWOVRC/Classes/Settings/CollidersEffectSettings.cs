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
        public int Frequency_Value { get; set; } = 50;
        [JsonInclude]
        public float SensationSeconds_Value { get; set; } = 0.3f;
        [JsonIgnore]
        public int Frequency
        {
            get
            {
                return Frequency_Value;
            }
            set
            {
                Frequency_Value = value;
                UpdateSensation();
            }
        }
        [JsonIgnore]
        public float SensationSeconds
        {
            get
            {
                return SensationSeconds_Value;
            }
            set
            {
                SensationSeconds_Value = value;
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
            Frequency_Value = frequency;
            SensationSeconds_Value = sensationSeconds;
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
                .Create(Frequency_Value, SensationSeconds_Value, 100, 0, 0, 0);
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
