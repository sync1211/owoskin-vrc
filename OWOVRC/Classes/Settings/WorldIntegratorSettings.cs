using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class WorldIntegratorSettings : EffectSettingsBase
    {
        [JsonInclude]
        public int UpdateInterval { get; set; } = 200;
        [JsonInclude]
        public int Intensity { get; set; } = 100;

        public WorldIntegratorSettings() : base(true, 1) { }

        [JsonConstructor]
        public WorldIntegratorSettings(bool enabled, int priority) : base(enabled, priority) { }
    }
}
