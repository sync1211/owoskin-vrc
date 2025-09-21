using OWOGame;
using OWOVRC.Classes.Settings;
using System.Text.Json;

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

            Dictionary<int, int> intensities = new() { { Muscle.Arm_R.id, 50 }, { Muscle.Arm_L.id, 70 } };
            WorldIntegratorSettings settings = new(false, 2, sensations, intensities)
            {
                UpdateInterval = 90
            };


            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            WorldIntegratorSettings? decodedSettings = JsonSerializer.Deserialize<WorldIntegratorSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.UpdateInterval, decodedSettings.UpdateInterval);

            Assert.AreEqual(settings.EnabledSensations.Count, decodedSettings.EnabledSensations.Count);
            foreach (KeyValuePair<string, int> sensation in sensations)
            {
                Assert.IsTrue(decodedSettings.EnabledSensations.ContainsKey(sensation.Key));
                Assert.AreEqual(sensation.Value, decodedSettings.EnabledSensations[sensation.Key]);
            }

            foreach (KeyValuePair<int, int> intensity in settings.MuscleIntensities)
            {
                Assert.IsTrue(decodedSettings.MuscleIntensities.ContainsKey(intensity.Key));
                Assert.AreEqual(intensity.Value, decodedSettings.MuscleIntensities[intensity.Key]);
            }
        }
    }
}
