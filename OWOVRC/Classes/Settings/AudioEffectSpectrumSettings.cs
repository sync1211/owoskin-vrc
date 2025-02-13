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
#pragma warning disable IDE1006 // Naming conventions
        private int priority { get; set; } = 0; // Set by Priority property
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

        [JsonIgnore]
        private int sensationFrequency { get; set; } = 10;
        [JsonIgnore]
        private float sensationSeconds { get; set; } = 0.1f;
#pragma warning restore IDE1006 // Naming conventions
        [JsonInclude]
        public int SensationFrequency
        {
            get
            {
                return sensationFrequency;
            }
            set
            {
                sensationFrequency = value;
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
            this.priority = priority;
            MinDB = minDB;
            MaxDB = maxDB;
            AudioFrequencyStart = audioFrequencyStart;
            AudioFrequencyEnd = audioFrequencyEnd;
            this.sensationFrequency = sensationFrequency;
            this.sensationSeconds = sensationSeconds;
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
