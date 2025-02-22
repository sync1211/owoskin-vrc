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
        public int Intensity
        {
            get
            {
                return intensity;
            }
            set
            {
                intensity = Math.Clamp(value, 0, 200);
            }
        }
        [JsonIgnore]
        private int intensity;
        [JsonInclude]
        public bool Loop { get; set; }
        [JsonInclude]
        public bool Interruptable { get; set; }
        [JsonInclude]
        [Browsable(false)]
        public readonly string SensationString;
        [JsonIgnore]
        [Browsable(false)]
        public readonly Sensation SensationObject;

        [JsonConstructor]
        public OSCSensationPreset(bool enabled, string name, int priority, int intensity, bool loop, bool interruptable, string sensationString)
        {
            Enabled = enabled;
            Name = name;
            Priority = priority;
            Intensity = intensity;
            Loop = loop;
            Interruptable = interruptable;
            SensationString = sensationString;
            SensationObject = Sensation.Parse(SensationString);
        }
    }
}
