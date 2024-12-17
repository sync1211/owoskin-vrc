using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class AudioEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public int MinBass { get; set; } = 12;
        [JsonInclude]
        public int MaxBass { get; set; } = 20;
        [JsonInclude]
        public int MaxIntensity { get; set; } = 25;
        [JsonInclude]
        public int Frequency { get; set; } = 10;
        [JsonInclude]
        public float SensationSeconds { get; set; } = 0.1f;

        [JsonConstructor]
        public AudioEffectSettings(int minBass, int maxBass, int maxIntensity, int frequency, float sensationSeconds, bool enabled = true, int priority = 1) : base(enabled, priority)
        {
            MinBass = minBass;
            MaxBass = maxBass;
            MaxIntensity = maxIntensity;
            Frequency = frequency;
            SensationSeconds = sensationSeconds;
        }

        public AudioEffectSettings(bool enabled = true, int priority = 1) : base(enabled, priority)
        {
        }
    }
}
