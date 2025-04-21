using OWOGame;
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
            Dictionary<int, int> intensities = new() { { Muscle.Arm_R.id, 50 }, { Muscle.Arm_L.id, 70 } };
            CollidersEffectSettings settings = new(
                enabled: false,
                priority: 2,
                useVelocity: false,
                allowContinuous: false,
                minIntensity: 100,
                frequency: 100,
                sensationSeconds: 0.5f,
                speedMultiplier: 2.0f,
                maxTimeDiff: TimeSpan.FromSeconds(2),
                muscleIntensities: intensities,
                decayFactor: 2.25f
            );

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            CollidersEffectSettings? decodedSettings = JsonSerializer.Deserialize<CollidersEffectSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.AllowContinuous, decodedSettings.AllowContinuous);
            Assert.AreEqual(settings.Frequency, decodedSettings.Frequency);
            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.MaxTimeDiff, decodedSettings.MaxTimeDiff);
            Assert.AreEqual(settings.MinIntensity, decodedSettings.MinIntensity);
            Assert.AreEqual(settings.SensationSeconds, decodedSettings.SensationSeconds);
            Assert.AreEqual(settings.SpeedMultiplier, decodedSettings.SpeedMultiplier);
            Assert.AreEqual(settings.UseVelocity, decodedSettings.UseVelocity);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.DecayFactor, decodedSettings.DecayFactor);
            Assert.AreEqual(settings.MuscleIntensities.Count, decodedSettings.MuscleIntensities.Count);
            foreach (KeyValuePair<int, int> intensity in settings.MuscleIntensities)
            {
                Assert.IsTrue(decodedSettings.MuscleIntensities.ContainsKey(intensity.Key));
                Assert.AreEqual(intensity.Value, decodedSettings.MuscleIntensities[intensity.Key]);
            }
        }
    }
}
