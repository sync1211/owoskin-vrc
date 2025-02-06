using OWOGame;
using System.ComponentModel;
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
        [JsonInclude]
        public bool Loop { get; set; }
        [JsonInclude]
        [Browsable(false)]
        public readonly string SensationString;
        [JsonIgnore]
        [Browsable(false)]
        public readonly Sensation SensationObject;

        [JsonConstructor]
        public OSCSensationPreset(bool enabled, string name, int priority, int intensity, bool loop, string sensationString)
        {
            Enabled = enabled;
            Name = name;
            Priority = priority;
            Intensity = intensity;
            Loop = loop;
            SensationString = sensationString;
            SensationObject = Sensation.Parse(SensationString);
        }
    }
}
