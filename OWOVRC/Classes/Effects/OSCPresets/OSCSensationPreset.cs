using OWOGame;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Effects.OSCPresets
{
    public class OSCSensationPreset
    {
        [JsonInclude]
        public bool Enabled { get; set; }
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public int Priority { get; set; }
        [JsonInclude]
        public int Intensity { get; set; }
        public string SensationString { get; private set; }
        [JsonIgnore]
        public readonly BakedSensation SensationObject;

        [JsonConstructor]
        public OSCSensationPreset(bool enabled, string name, int priority, int intensity, string sensationString)
        {
            Enabled = enabled;
            Name = name;
            Priority = priority;
            Intensity = intensity;
            SensationString = sensationString;
            SensationObject = BakedSensation.Parse(SensationString);
        }
    }
}
