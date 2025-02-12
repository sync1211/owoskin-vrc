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
            Dictionary<string, int> sensations = new()
            {
                { "Test1", 44 },
                { "Test2", 12 }
            };

            WorldIntegratorSettings settings = new(false, 2, sensations)
            {
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

            Assert.AreEqual(settings.EnabledSensations.Count, decodedSettings.EnabledSensations.Count);
            foreach (KeyValuePair<string, int> sensation in sensations)
            {
                Assert.IsTrue(decodedSettings.EnabledSensations.ContainsKey(sensation.Key));
                Assert.AreEqual(sensation.Value, decodedSettings.EnabledSensations[sensation.Key]);
            }
        }
    }
}
