using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public class ConnectionSettings
    {
        // OSC
        public int OSCPort { get; set; } = 9001;

        // OWO
        public string OWOAddress { get; set; } = "127.0.0.1";

        public ConnectionSettings() {}

        [JsonConstructor]
        public ConnectionSettings(string owoAddress, int oscPort)
        {
            OWOAddress = owoAddress;
            OSCPort = oscPort;
        }
    }
}
