﻿namespace OWOVRC.Classes.Settings
{
    public class VelocityEffectSettings : EffectSettingsBase
    {
        public float Threshold { get; set; } = 8;
        public bool ImpactEnabled { get; set; } = true;
        public int StopPriority { get; set; } = 1;
        public float StopVelocityThreshold { get; set; } = 10;
        public float SpeedCap { get; set; } = 50.0f;
        public bool IgnoreWhenGrounded { get; set; }
        public bool IgnoreWhenSeated { get; set; }
        public TimeSpan StopVelocityTime { get; set; } = TimeSpan.FromSeconds(1);
    }
}
