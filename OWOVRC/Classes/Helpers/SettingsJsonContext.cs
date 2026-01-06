using OWOVRC.Classes.Settings;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Helpers
{
    // Required for deserialization via System.Text.Json
    [JsonSerializable(typeof(ConnectionSettings))]
    [JsonSerializable(typeof(VelocityEffectSettings))]
    [JsonSerializable(typeof(InertiaEffectSettings))]
    [JsonSerializable(typeof(CollidersEffectSettings))]
    [JsonSerializable(typeof(OSCPresetsSettings))]
    [JsonSerializable(typeof(WorldIntegratorSettings))]
    [JsonSerializable(typeof(AudioEffectSettings))]
    public partial class SettingsJsonContext : JsonSerializerContext;
}
