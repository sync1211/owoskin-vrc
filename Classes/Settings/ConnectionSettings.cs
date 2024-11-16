using Newtonsoft.Json;
using System.Net;

namespace OWOVRC.Classes.Settings
{
    public class ConnectionSettings
    {
        // OSC
        public int OSCPort { get; set; } = 9001;

        // OWO
        public IPAddress OWOAddress { get; set; } = new(new byte[] { 127, 0, 0, 1 });

        public static ConnectionSettings? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ConnectionSettings>(json);
        }
    }
}
