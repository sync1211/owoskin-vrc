using OWOGame;
using OWOVRC.Audio.Classes;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class AudioEffectSpectrumSettings
    {
        [JsonInclude]
        public bool Enabled { get; set; } = true;
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public int Priority { get; set; } = 0;
        [JsonInclude]
        public float MinDB { get; set; } = 12;
        [JsonInclude]
        public float MaxDB { get; set; } = 20;
        [JsonInclude]
        public int AudioFrequencyStart { get; set; }
        [JsonInclude]
        public int AudioFrequencyEnd { get; set; }

        [JsonInclude]
        public int SensationFrequency { get; set; } = 10;
        [JsonInclude]
        public float SensationSeconds { get; set; } = 0.1f;
        [JsonInclude]
        public Dictionary<int, int> Intensities { get; set; } = [];

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
            SensationFrequency = sensationFrequency;
            SensationSeconds = sensationSeconds;
            Intensities = intensities ?? [];

            if (MaxDB < MinDB)
            {
                throw new ArgumentException("MaxDB must be greater than or equal to MinDB");
            }

            AddMissingIntensities();
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
                throw new ArgumentException("MaxDB must be greater than or equal to MinDB");
            }

            foreach (Muscle muscle in muscles)
            {
                Intensities[muscle.id] = baseIntensity;
            }

            AddMissingIntensities();
        }

        public Sensation CreateSensation()
        {
            return SensationsFactory
                .Create(SensationFrequency, SensationSeconds)
                .WithPriority(Priority);
        }
    }
}
