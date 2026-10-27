using BuildSoft.OscCore;
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
        [Browsable(false)]
        public string? Name { get; set; } // Required for backwards compatibility
        [JsonInclude]
        public string Path { get; set; }
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
        public bool Interruptible { get; set; }
        [JsonInclude]
        [Browsable(false)]
        public string SensationString { get; }
        [JsonIgnore]
        [Browsable(false)]
        public readonly Sensation SensationObject;
        [JsonIgnore]
        [Browsable(false)]
        public readonly Dictionary<string, Action<OscMessageValues>> MessageCallbacks = [];

        [JsonConstructor]
        public OSCSensationPreset(bool enabled, string? path, int priority, int intensity, bool loop, bool interruptible, string sensationString, string? name = null)
        {
            Enabled = enabled;

            // Migrate name to path, if necessary
            if (name != null)
            {
                Path = GetPrefixedPath(name);
            }
            else
            {
                Path = path ?? String.Empty;
            }

            Priority = priority;
            Intensity = intensity;
            Loop = loop;
            Interruptible = interruptible;
            SensationString = sensationString;
            SensationObject = Sensation.Parse(SensationString);
        }

        public static string GetPrefixedPath(string name)
        {
            return $"{OSCPresetTrigger.PATH_BASE}/{name}";
        }

        public OSCSensationPreset Clone()
        {
            return new(
                this.Enabled,
                this.Path,
                this.Priority,
                this.Intensity,
                this.Loop,
                this.Interruptible,
                this.SensationString
            );
        }
    }
}
