using OWOGame;
using OWOVRC.Audio.Classes;
using OWOVRC.Classes.Settings;
using System.Text.Json;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class AudioEffectSpectrumSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            AudioEffectSpectrumSettings settings = new("Test", AudioSpectrum.Bass)
            {
                Enabled = false,
                Priority = 9,
                MinDB = 16,
                MaxDB = 30,
                SensationFrequency = 50,
                SensationSeconds = 0.8f
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            AudioEffectSpectrumSettings? decodedSettings = JsonSerializer.Deserialize<AudioEffectSpectrumSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.MinDB, decodedSettings.MinDB);
            Assert.AreEqual(settings.MaxDB, decodedSettings.MaxDB);
            Assert.AreEqual(settings.SensationFrequency, decodedSettings.SensationFrequency);
            Assert.AreEqual(settings.SensationSeconds, decodedSettings.SensationSeconds);
            Assert.AreEqual(settings.AudioFrequencyStart, decodedSettings.AudioFrequencyStart);
            Assert.AreEqual(settings.AudioFrequencyEnd, decodedSettings.AudioFrequencyEnd);
        }

        [TestMethod]
        public void TestJsonEncodeDecodeIntensities()
        {
            Dictionary<int, int> intensitiesExpected = new()
            {
                { Muscle.Abdominal_L.id, 10 },
                { Muscle.Abdominal_R.id, 20 },
                { Muscle.Dorsal_L.id, 30 },
                { Muscle.Dorsal_R.id, 25 },
                { Muscle.Lumbar_R.id, 0 },
                { Muscle.Lumbar_L.id, 0 },
                { Muscle.Arm_L.id, 50 },
                { Muscle.Arm_R.id, 0 },
                { Muscle.Pectoral_L.id, 0 },
                { Muscle.Pectoral_R.id, 2 }
            };

            AudioEffectSpectrumSettings settings = new(true, "Test", 1, 10, 20, 100, 200, 10, 0.1f, intensitiesExpected);

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            AudioEffectSpectrumSettings? decodedSettings = JsonSerializer.Deserialize<AudioEffectSpectrumSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Dictionary<int, int> intensities = decodedSettings.Intensities;
            Assert.AreEqual(intensitiesExpected.Count, intensities.Count);

            foreach (KeyValuePair<int, int> entry in intensitiesExpected)
            {
                Assert.IsTrue(intensities.ContainsKey(entry.Key));
                Assert.AreEqual(entry.Value, intensities[entry.Key]);
            }
        }
    }
}
