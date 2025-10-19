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
        public int MaxSensationCycles { get; set; } = 100; // Maximum cycles for muscles to be active without being updated. Fixes stuck muscles due to UDP being UDP.
        [JsonInclude]
        public int MinIntensity { get; set; } = 50; // Min intensity when calculating speed
        [JsonIgnore]
        private int frequency = 20; // Set by Frequency property
        [JsonIgnore]
        private const float sensationSeconds = 0.2f; // Set by SensationSeconds property
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
        public float SpeedMultiplier { get; set; } = 1.0f;
        [JsonInclude]
        public TimeSpan MaxTimeDiff { get; set; } = TimeSpan.FromSeconds(1);
        [JsonInclude]
        public Dictionary<int, int> MuscleIntensities { get; } = [];

        [JsonIgnore]
        private Sensation sensation = null!;

        // Intensity decay
        [JsonInclude]
        public int DecayTime
        {
            get
            {
                return decayTime;
            }
            set
            {
                decayTime = value;
                UpdateCycleCount();
            }
        }
        [JsonIgnore]
        private int decayTime = 500; // Decay time in ms
        [JsonIgnore]
        public int DecayCycleCount { get; private set; } = 1; // Decay time converted to timer cycles (use UpdateCycleCount to update)
        [JsonInclude]
        public bool DecayOnExit { get; set; } = true;
        [JsonInclude]
        public bool DecayOnChanges { get; set; } = true;

        public CollidersEffectSettings(bool enabled = true, int priority = 10) : base(enabled, priority)
        {
            MuscleIntensityHelper.AddMissingMuscles(MuscleIntensities);
            UpdateSensation();
        }

        [JsonConstructor]
        public CollidersEffectSettings(bool enabled, int priority, bool useVelocity, int minIntensity, int frequency, float speedMultiplier, TimeSpan maxTimeDiff, Dictionary<int, int> muscleIntensities, int decayTime = 500, bool decayOnExit = true, bool decayOnChanges = true) : base(enabled, priority)
        {
            UseVelocity = useVelocity;
            MinIntensity = minIntensity;
            this.frequency = frequency;
            SpeedMultiplier = speedMultiplier;
            MaxTimeDiff = maxTimeDiff;
            MuscleIntensities = muscleIntensities ?? [];
            this.decayTime = decayTime;
            DecayOnExit = decayOnExit;
            DecayOnChanges = decayOnChanges;

            MuscleIntensityHelper.AddMissingMuscles(MuscleIntensities);
            UpdateSensation();
            UpdateCycleCount();
        }

        private void UpdateCycleCount()
        {
            DecayCycleCount = (int)(DecayTime / (sensationSeconds * 1000));
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
