using OWOVRC.Classes.Settings;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace OWOVRC.Classes.Helpers
{
    public static partial class SettingsHelper
    {
        // Required for deserialization via System.Text.Json
        [JsonSerializable(typeof(ConnectionSettings))]
        public partial class ConnectionSettingsJsonContext : JsonSerializerContext;

        [JsonSerializable(typeof(VelocityEffectSettings))]
        public partial class VelocityEffectSettingsContext : JsonSerializerContext;

        [JsonSerializable(typeof(CollidersEffectSettings))]
        public partial class CollidersEffectSettingsContext : JsonSerializerContext;

        [JsonSerializable(typeof(OSCPresetsSettings))]
        public partial class OSCPresetsSettingsContext : JsonSerializerContext;

        [JsonSerializable(typeof(WorldIntegratorSettings))]
        public partial class WorldIntegratorSettingsContext : JsonSerializerContext;
        [JsonSerializable(typeof(AudioEffectSettings))]
        public partial class AudioEffectSettingsContext : JsonSerializerContext;

        // Settings directory
        public static readonly string settingsDir = Environment.CurrentDirectory;
        public static T? LoadSettingsFromFile<T>(string filename, string displayName, JsonTypeInfo<T> jsonTypeInfo)
        {
            string settingsFilePath = Path.Combine(settingsDir, filename);
            if (!File.Exists(settingsFilePath))
            {
                Log.Warning("Failed to load {0} settings: file does not exist", displayName);
                return default;
            }

            using (FileStream fileStream = new(settingsFilePath, FileMode.Open))
            {
                if (fileStream.Length == 0)
                {
                    Log.Error("Failed to load {0} settings: file is empty", displayName);
                    return default;
                }

                try
                {
                    T? settings = JsonSerializer.Deserialize(fileStream, jsonTypeInfo);
                    if (settings == null)
                    {
                        Log.Error("Failed to load {0} settings: failed to deserialize", displayName);
                        return default;
                    }

                    return settings;
                }
                catch (JsonException e)
                {
                    Log.Error(e, "Failed to load {0} settings: failed to deserialize", displayName);
                    return default;
                }
            }
        }

        public static void SaveSettingsToFile<T>(T settings, string fileName, string displayName, JsonTypeInfo<T> jsonTypeInfo)
        {
            string settingsFilePath = Path.Combine(settingsDir, fileName);

            using (FileStream fileStream = new(settingsFilePath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(new Utf8JsonWriter(fileStream), settings, jsonTypeInfo);
            }

            Log.Information("{0} settings saved", displayName);
        }
    }
}
