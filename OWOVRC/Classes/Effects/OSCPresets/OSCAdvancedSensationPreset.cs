using System.Text.Json.Serialization;
using Windows.Media.Audio;

namespace OWOVRC.Classes.Effects.OSCPresets
{
    public class OSCAdvancedSensationPreset : OSCSensationPreset
    {
        [JsonInclude]
        public string Path;
        [JsonInclude]
        public float MinValue;
        [JsonInclude]
        public float MaxValue;

        public OSCAdvancedSensationPreset(bool enabled, string name, int priority, int intensity, bool loop, bool interruptable, string sensationString, string path, float minValue, float maxValue) : base(enabled, name, priority, intensity, loop, interruptable, sensationString)
        {
            Path = path;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}
