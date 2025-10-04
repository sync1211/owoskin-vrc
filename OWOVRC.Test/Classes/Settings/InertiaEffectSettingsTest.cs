using System.Text.Json;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class InertiaEffectSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            InertiaEffectSettings settings = new()
            {
                Priority = 2,
                MinDelta = 22,
                MaxDelta = 201.0f,
                Intensity = 75,
                IgnoreWhenGrounded = true,
                IgnoreWhenSeated = true,
                AccelEnabled = true,
                DecelEnabled = false
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            InertiaEffectSettings? decodedSettings = JsonSerializer.Deserialize<InertiaEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.MinDelta, decodedSettings.MinDelta);
            Assert.AreEqual(settings.MaxDelta, decodedSettings.MaxDelta);
            Assert.AreEqual(settings.Intensity, decodedSettings.Intensity);
            Assert.AreEqual(settings.AccelEnabled, decodedSettings.AccelEnabled);
            Assert.AreEqual(settings.DecelEnabled, decodedSettings.DecelEnabled);
            Assert.AreEqual(settings.IgnoreWhenGrounded, decodedSettings.IgnoreWhenGrounded);
            Assert.AreEqual(settings.IgnoreWhenSeated, decodedSettings.IgnoreWhenSeated);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
        }
    }
}
