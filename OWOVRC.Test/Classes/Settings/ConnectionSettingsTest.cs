using OWOVRC.Classes.Settings;
using System.Text.Json;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class ConnectionSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            ConnectionSettings settings = new("192.168.178.1", 9999);

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            ConnectionSettings? decodedSettings = JsonSerializer.Deserialize<ConnectionSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.OSCPort, decodedSettings.OSCPort);
            Assert.AreEqual(settings.OWOAddress, decodedSettings.OWOAddress);
        }
    }
}
