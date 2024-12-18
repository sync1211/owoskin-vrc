using OWOVRC.Classes.Settings;
using System.Text.Json;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class AudioEffectSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            AudioEffectSettings settings = new()
            {
                Enabled = false,
                Priority = 2,
                MinBass = 90,
                MaxBass = 120,
                MaxIntensity = 30,
                Frequency = 100,
                SensationSeconds = 0.5f
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            AudioEffectSettings? decodedSettings = JsonSerializer.Deserialize<AudioEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.MinBass, decodedSettings.MinBass);
            Assert.AreEqual(settings.MaxBass, decodedSettings.MaxBass);
            Assert.AreEqual(settings.MaxIntensity, decodedSettings.MaxIntensity);
            Assert.AreEqual(settings.Frequency, decodedSettings.Frequency);
            Assert.AreEqual(settings.SensationSeconds, decodedSettings.SensationSeconds);
        }
    }
}
