using System.Text.Json;
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
                Enabled = false,
                UpdateInterval = 90,
                Intensity = 89
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            WorldIntegratorSettings? decodedSettings = JsonSerializer.Deserialize<WorldIntegratorSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.UpdateInterval, decodedSettings.UpdateInterval);
            Assert.AreEqual(settings.Intensity, decodedSettings.Intensity);
        }
    }
}
