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
                Priority = 2
            };

            settings.BassSettings.Priority = 3;
            settings.SubBassSettings.Priority = 4;

            settings.BassSettings.Enabled = false;
            settings.BassSettings.MinDB = 13;
            settings.BassSettings.MaxDB = 200;
            settings.SubBassSettings.SensationFrequency = 30;
            settings.SubBassSettings.SensationSeconds = 0.3f;

            settings.SubBassSettings.Enabled = false;
            settings.SubBassSettings.MinDB = 20;
            settings.SubBassSettings.MaxDB = 400;
            settings.SubBassSettings.SensationFrequency = 20;
            settings.SubBassSettings.SensationSeconds = 0.2f;

            settings.TrebleSettings.Enabled = false;
            settings.TrebleSettings.MinDB = 55;
            settings.TrebleSettings.MaxDB = 448;
            settings.TrebleSettings.SensationFrequency = 14;
            settings.TrebleSettings.SensationSeconds = 0.3f;

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            AudioEffectSettings? decodedSettings = JsonSerializer.Deserialize<AudioEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);

            Assert.AreEqual(settings.BassSettings.Name, decodedSettings.BassSettings.Name);
            Assert.AreEqual(settings.BassSettings.Priority, decodedSettings.BassSettings.Priority);
            Assert.AreEqual(settings.BassSettings.SensationFrequency, decodedSettings.BassSettings.SensationFrequency);
            Assert.AreEqual(settings.BassSettings.SensationSeconds, decodedSettings.BassSettings.SensationSeconds);
            Assert.AreEqual(settings.BassSettings.MinDB, decodedSettings.BassSettings.MinDB);
            Assert.AreEqual(settings.BassSettings.MaxDB, decodedSettings.BassSettings.MaxDB);
            Assert.AreEqual(settings.BassSettings.AudioFrequencyStart, decodedSettings.BassSettings.AudioFrequencyStart);
            Assert.AreEqual(settings.BassSettings.AudioFrequencyEnd, decodedSettings.BassSettings.AudioFrequencyEnd);

            Assert.AreEqual(settings.SubBassSettings.Name, decodedSettings.SubBassSettings.Name);
            Assert.AreEqual(settings.SubBassSettings.Priority, decodedSettings.SubBassSettings.Priority);
            Assert.AreEqual(settings.SubBassSettings.SensationFrequency, decodedSettings.SubBassSettings.SensationFrequency);
            Assert.AreEqual(settings.SubBassSettings.SensationSeconds, decodedSettings.SubBassSettings.SensationSeconds);
            Assert.AreEqual(settings.SubBassSettings.MinDB, decodedSettings.SubBassSettings.MinDB);
            Assert.AreEqual(settings.SubBassSettings.MaxDB, decodedSettings.SubBassSettings.MaxDB);
            Assert.AreEqual(settings.SubBassSettings.AudioFrequencyStart, decodedSettings.SubBassSettings.AudioFrequencyStart);
            Assert.AreEqual(settings.SubBassSettings.AudioFrequencyEnd, decodedSettings.SubBassSettings.AudioFrequencyEnd);

            Assert.AreEqual(settings.TrebleSettings.Name, decodedSettings.TrebleSettings.Name);
            Assert.AreEqual(settings.TrebleSettings.Priority, decodedSettings.TrebleSettings.Priority);
            Assert.AreEqual(settings.TrebleSettings.SensationFrequency, decodedSettings.TrebleSettings.SensationFrequency);
            Assert.AreEqual(settings.TrebleSettings.SensationSeconds, decodedSettings.TrebleSettings.SensationSeconds);
            Assert.AreEqual(settings.TrebleSettings.MinDB, decodedSettings.TrebleSettings.MinDB);
            Assert.AreEqual(settings.TrebleSettings.MaxDB, decodedSettings.TrebleSettings.MaxDB);
            Assert.AreEqual(settings.TrebleSettings.AudioFrequencyStart, decodedSettings.TrebleSettings.AudioFrequencyStart);
            Assert.AreEqual(settings.TrebleSettings.AudioFrequencyEnd, decodedSettings.TrebleSettings.AudioFrequencyEnd);
        }
    }
}
