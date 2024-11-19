using Newtonsoft.Json;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class WorldIntegratorSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            WorldIntegratorSettings settings = new()
            {
                Priority = 2,
                Enabled = false
            };

            string json = JsonConvert.SerializeObject(settings);
            Assert.AreNotEqual(0, json.Length);

            WorldIntegratorSettings? decodedSettings = JsonConvert.DeserializeObject<WorldIntegratorSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
        }
    }
}
