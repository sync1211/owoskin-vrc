using OWOGame;
using OWOVRC.Audio.Classes;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class AudioEffectSpectrumSettings
    {
        [JsonInclude]
        public int Priority
        {
            get
            {
                return priority;
            }
            set
            {
                if (priority == value)
                {
                    return;
                }

                priority = value;
                OnPriorityChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        [JsonIgnore]
#pragma warning disable IDE1006 // Benennungsstile
        private int priority { get; set; } = 0;
#pragma warning restore IDE1006 // Benennungsstile
        [JsonInclude]
        public bool Enabled { get; set; } = true;
        [JsonInclude]
        public string Name { get; }
        [JsonInclude]
        public float MinDB { get; set; } = 12;
        [JsonInclude]
        public float MaxDB { get; set; } = 20;
        [JsonInclude]
        public int AudioFrequencyStart { get; }
        [JsonInclude]
        public int AudioFrequencyEnd { get; }

        [JsonInclude]
        public int SensationFrequency_Value { get; set; } = 10;
        [JsonInclude]
        public float SensationSeconds_Value { get; set; } = 0.1f;
        [JsonIgnore]
        public int SensationFrequency
        {
            get
            {
                return SensationFrequency_Value;
            }
            set
            {
                SensationFrequency_Value = value;
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
        public Dictionary<int, int> Intensities { get; } = [];
        [JsonIgnore]
        public EventHandler? OnPriorityChanged;
        [JsonIgnore]
        private Sensation sensation = null!;

        [JsonConstructor]
        public AudioEffectSpectrumSettings(bool enabled, string name, int priority, float minDB, float maxDB, int audioFrequencyStart, int audioFrequencyEnd, int sensationFrequency, float sensationSeconds, Dictionary<int, int>? intensities)
        {
            Enabled = enabled;
            Name = name;
            Priority = priority;
            MinDB = minDB;
            MaxDB = maxDB;
            AudioFrequencyStart = audioFrequencyStart;
            AudioFrequencyEnd = audioFrequencyEnd;
            SensationFrequency_Value = sensationFrequency;
            SensationSeconds_Value = sensationSeconds;
            Intensities = intensities ?? [];

            if (MaxDB < MinDB)
            {
                throw new ArgumentException("MaxDB must be greater than or equal to MinDB");
            }

            AddMissingIntensities();
            UpdateSensation();
        }

        private void AddMissingIntensities()
        {
            foreach (Muscle muscle in Muscle.All)
            {
                if (Intensities.ContainsKey(muscle.id))
                {
                    continue;
                }
                Intensities[muscle.id] = 0;
            }
        }

        private void UpdateSensation()
        {
            sensation = SensationsFactory
                .Create(SensationFrequency, SensationSeconds);
        }

        public AudioEffectSpectrumSettings(string name, FrequencyRange frequencyRange)
        {
            Name = name;
            AudioFrequencyStart = frequencyRange.Start;
            AudioFrequencyEnd = frequencyRange.End;
        }

        public AudioEffectSpectrumSettings(string name, FrequencyRange frequencyRange, Muscle[] muscles, int baseIntensity, int priority = 1, float minDB = 12, float maxDB = 20, int sensationFrequency = 10)
        {
            Name = name;
            AudioFrequencyStart = frequencyRange.Start;
            AudioFrequencyEnd = frequencyRange.End;

            SensationFrequency = sensationFrequency;

            MinDB = minDB;
            MaxDB = maxDB;
            Priority = priority;

            if (MaxDB < MinDB)
            {
                throw new ArgumentException($"MaxDB must be greater than or equal to MinDB: {MinDB} < {MaxDB}");
            }

            foreach (Muscle muscle in muscles)
            {
                Intensities[muscle.id] = baseIntensity;
            }

            AddMissingIntensities();
        }

        public Sensation GetSensation()
        {
            return sensation
                .WithPriority(Priority);
        }
    }
}
