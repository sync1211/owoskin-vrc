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

        public ConnectionSettings() {}

        [JsonConstructor]
        public ConnectionSettings(string owoAddress, int oscPort, bool resolveHostnames)
        {
            OWOAddress = owoAddress;
            OSCPort = oscPort;
            ResolveHostnames = resolveHostnames;
        }

        public void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "connection.json", "Connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
        }
    }
}
