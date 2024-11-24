using OWOVRC.Classes.Settings;
using System.Text.Json;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class CollidersEffectSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            CollidersEffectSettings settings = new()
            {
                Priority = 2,
                AllowContinuous = false,
                BaseIntensity = 200,
                Frequency = 100,
                Enabled = false,
                MaxTimeDiff = TimeSpan.FromSeconds(2),
                MinIntensity = 100,
                SensationSeconds = 0.5f,
                SpeedMultiplier = 100.0f,
                UseVelocity = false
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            CollidersEffectSettings? decodedSettings = JsonSerializer.Deserialize<CollidersEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.AllowContinuous, decodedSettings.AllowContinuous);
            Assert.AreEqual(settings.BaseIntensity, decodedSettings.BaseIntensity);
            Assert.AreEqual(settings.Frequency, decodedSettings.Frequency);
            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.MaxTimeDiff, decodedSettings.MaxTimeDiff);
            Assert.AreEqual(settings.MinIntensity, decodedSettings.MinIntensity);
            Assert.AreEqual(settings.SensationSeconds, decodedSettings.SensationSeconds);
            Assert.AreEqual(settings.SpeedMultiplier, decodedSettings.SpeedMultiplier);
            Assert.AreEqual(settings.UseVelocity, decodedSettings.UseVelocity);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
        }
    }
}
