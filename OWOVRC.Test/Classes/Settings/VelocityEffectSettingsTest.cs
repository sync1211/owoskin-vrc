using System.Text.Json;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class VelocityEffectSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            VelocityEffectSettings settings = new()
            {
                Priority = 2,
                Threshold = 22,
                StopVelocityThreshold = 11,
                SpeedCap = 201.0f,
                IgnoreWhenGrounded = true,
                IgnoreWhenSeated = true,
                StopVelocityTime = TimeSpan.FromSeconds(2),
                ImpactEnabled = false
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            VelocityEffectSettings? decodedSettings = JsonSerializer.Deserialize<VelocityEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Threshold, decodedSettings.Threshold);
            Assert.AreEqual(settings.StopVelocityThreshold, decodedSettings.StopVelocityThreshold);
            Assert.AreEqual(settings.SpeedCap, decodedSettings.SpeedCap);
            Assert.AreEqual(settings.IgnoreWhenGrounded, decodedSettings.IgnoreWhenGrounded);
            Assert.AreEqual(settings.IgnoreWhenSeated, decodedSettings.IgnoreWhenSeated);
            Assert.AreEqual(settings.StopVelocityTime, decodedSettings.StopVelocityTime);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.ImpactEnabled, decodedSettings.ImpactEnabled);
        }
    }
}
