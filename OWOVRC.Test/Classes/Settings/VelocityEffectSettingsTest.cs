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
                MinSpeed = 22,
                MaxSpeed = 201.0f,
                IgnoreWhenGrounded = true,
                IgnoreWhenSeated = true,
                Intensity = 22
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            VelocityEffectSettings? decodedSettings = JsonSerializer.Deserialize<VelocityEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.MinSpeed, decodedSettings.MinSpeed);
            Assert.AreEqual(settings.MaxSpeed, decodedSettings.MaxSpeed);
            Assert.AreEqual(settings.IgnoreWhenGrounded, decodedSettings.IgnoreWhenGrounded);
            Assert.AreEqual(settings.IgnoreWhenSeated, decodedSettings.IgnoreWhenSeated);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.Intensity, decodedSettings.Intensity);
        }
    }
}
