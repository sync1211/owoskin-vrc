using Newtonsoft.Json;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class ConnectionSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            ConnectionSettings settings = new("192.168.178.1", 9999);

            string json = JsonConvert.SerializeObject(settings);
            Assert.AreNotEqual(0, json.Length);

            ConnectionSettings? decodedSettings = JsonConvert.DeserializeObject<ConnectionSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.OSCPort, decodedSettings.OSCPort);
            Assert.AreEqual(settings.OWOAddress, decodedSettings.OWOAddress);
        }
    }
}
