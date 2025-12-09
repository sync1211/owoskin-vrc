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
        public bool UseOSCQuery { get; set; } = true;
        public int OSCQuery_MaxWait { get; } = 600_000; // 10 minutes
        public int OSCQuery_RefreshInterval { get; } = 5_000; // 5 seconds

        public ConnectionSettings() {}

        [JsonConstructor]
        public ConnectionSettings(string owoAddress, int oscPort, bool resolveHostnames = true, bool useOscQuery = true, int oscQuery_MaxWait = 60_000, int oscQuery_RefreshInterval = 5_000)
        {
            OWOAddress = owoAddress;
            OSCPort = oscPort;

            ResolveHostnames = resolveHostnames;

            UseOSCQuery = useOscQuery;
            OSCQuery_MaxWait = oscQuery_MaxWait;
            OSCQuery_RefreshInterval = oscQuery_RefreshInterval;
        }

        public void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "connection.json", "Connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
        }
    }
}
