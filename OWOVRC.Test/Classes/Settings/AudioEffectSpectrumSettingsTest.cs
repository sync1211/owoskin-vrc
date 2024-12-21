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
    }
}
