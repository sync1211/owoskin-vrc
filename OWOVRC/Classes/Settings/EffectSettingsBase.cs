using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public abstract class EffectSettingsBase
    {
        [JsonInclude]
        public bool Enabled { get; set; } = true;
        [JsonInclude]
        public int Priority { get; set; }

        [JsonConstructor]
        protected EffectSettingsBase(bool enabled, int priority)
        {
            Enabled = enabled;
            Priority = priority;
        }
    }
}
