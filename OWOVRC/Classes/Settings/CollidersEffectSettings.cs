﻿using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class CollidersEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public bool UseVelocity { get; set; } = true;
        [JsonInclude]
        public bool AllowContinuous { get; set; } = true;
        [JsonInclude]
        public int BaseIntensity { get; set; } = 100;
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

        public CollidersEffectSettings(bool enabled = true, int priority = 1) : base(enabled, priority) { }

        [JsonConstructor]
        public CollidersEffectSettings(bool enabled, int priority, bool useVelocity, bool allowContinuous, int baseIntensity, int minIntensity, int frequency, float sensationSeconds, float speedMultiplier, TimeSpan maxTimeDiff) : base(enabled, priority)
        {
            UseVelocity = useVelocity;
            AllowContinuous = allowContinuous;
            BaseIntensity = baseIntensity;
            MinIntensity = minIntensity;
            Frequency = frequency;
            SensationSeconds = sensationSeconds;
            SpeedMultiplier = speedMultiplier;
            MaxTimeDiff = maxTimeDiff;
        }
    }
}
