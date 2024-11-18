using Newtonsoft.Json;
using OWOVRC.Classes.Settings;
using System.Net;

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
                Threshold = 22,
                StopVelocityThreshold = 11,
                SpeedCap = 201.0f,
                IgnoreWhenGrounded = true,
                IgnoreWhenSeated = true,
                StopVelocityTime = TimeSpan.FromSeconds(2)
            };

            string json = JsonConvert.SerializeObject(settings);
            Assert.AreNotEqual(0, json.Length);

            VelocityEffectSettings? decodedSettings = JsonConvert.DeserializeObject<VelocityEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Threshold, decodedSettings.Threshold);
            Assert.AreEqual(settings.StopVelocityThreshold, decodedSettings.StopVelocityThreshold);
            Assert.AreEqual(settings.SpeedCap, decodedSettings.SpeedCap);
            Assert.AreEqual(settings.IgnoreWhenGrounded, decodedSettings.IgnoreWhenGrounded);
            Assert.AreEqual(settings.IgnoreWhenSeated, decodedSettings.IgnoreWhenSeated);
            Assert.AreEqual(settings.StopVelocityTime, decodedSettings.StopVelocityTime);
        }
    }
}
