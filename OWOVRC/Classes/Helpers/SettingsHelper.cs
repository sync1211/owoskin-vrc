using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace OWOVRC.Classes.Helpers
{
    public static partial class SettingsHelper
    {
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

            using (FileStream fileStream = new(settingsFilePath, FileMode.Open, FileAccess.Read))
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

            using (FileStream fileStream = new(settingsFilePath, FileMode.Create, FileAccess.Write))
            {
                using (Utf8JsonWriter writer = new(fileStream))
                {
                    JsonSerializer.Serialize(writer, settings, jsonTypeInfo);
                }
            }

            Log.Information("{0} settings saved", displayName);
        }
    }
}
