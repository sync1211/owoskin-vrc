using Newtonsoft.Json;

namespace OWOVRC.Classes.Settings
{
    public class VelocitySensationSettings
    {
        public double Threshold { get; set; } = 8;
        public double StopVelocityThreshold = 10;
        public double SpeedCap { get; set; } = 200.0;
        public bool IgnoreWhenGrounded { get; set; }
        public bool IgnoreWhenSeated { get; set; }
        public TimeSpan StopVelocityTime { get; set; } = TimeSpan.FromSeconds(1);

        public static VelocitySensationSettings? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<VelocitySensationSettings>(json);
        }
    }
}
