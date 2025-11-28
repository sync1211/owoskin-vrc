using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class ConnectionSettings
    {
        // OSC
        public int OSCPort { get; set; } = 9001;

        // OWO
        public string OWOAddress { get; set; } = "127.0.0.1";

        // Additional settings
        public bool ResolveHostnames { get; set; } = true;
        public bool UseOSCQueryForPort { get; set; } = false;

        public ConnectionSettings() {}

        [JsonConstructor]
        public ConnectionSettings(string owoAddress, int oscPort, bool resolveHostnames = true, bool useOscQueryForPort = false)
        {
            OWOAddress = owoAddress;
            OSCPort = oscPort;
            ResolveHostnames = resolveHostnames;
            UseOSCQueryForPort = useOscQueryForPort;
        }

        public void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "connection.json", "Connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
        }
    }
}
