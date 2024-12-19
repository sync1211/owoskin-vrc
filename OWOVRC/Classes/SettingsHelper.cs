using OWOVRC.Classes.Settings;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace OWOVRC.Classes
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

            string settingsData = File.ReadAllText(settingsFilePath);
            if (string.IsNullOrWhiteSpace(settingsData))
            {
                Log.Error("Failed to load {0} settings: file is empty", displayName);
                return default;
            }

            T? settings = JsonSerializer.Deserialize(settingsData, jsonTypeInfo);
            if (settings == null)
            {
                Log.Error("Failed to load {0} settings: failed to deserialize", displayName);
                return default;
            }

            return settings;
        }

        public static void SaveSettingsToFile<T>(T settings, string fileName, string displayName, JsonTypeInfo<T> jsonTypeInfo)
        {
            string settingsData = JsonSerializer.Serialize(settings, jsonTypeInfo);
            string settingsFilePath = Path.Combine(settingsDir, fileName);

            if (!File.Exists(settingsFilePath))
            {
                FileStream newFile = File.Create(settingsFilePath);
                newFile.Close();
            }

            File.WriteAllText(settingsFilePath, settingsData);

            Log.Information("{0} settings saved", displayName);
        }
    }
}
