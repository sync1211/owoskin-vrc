using OWOVRC.Audio.Classes;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Effects
{
    [TestClass]
    public class AudioEffectTest
    {
        [DataTestMethod]
        [DataRow(50, 0, 100, 50)]
        [DataRow(50, 10, 110, 40)]
        [DataRow(0.5f, 0, 1, 50)]
        [DataRow(1.5f, 1, 2, 50)]
        [DataRow(1.5f, 0, 1, 100)]
        [DataRow(1.5f, 2, 3, 0)]
        public void TestCalculateIntensityPercentage(float level, float min, float max, float expected)
        {
            AudioEffectSpectrumSettings spectrumSettings = new("Test", new FrequencyRange(0, 0))
            {
                MinDB = min,
                MaxDB = max
            };

            float result = AudioEffect.CalculateIntensityPercentage(level, spectrumSettings);

            Assert.AreEqual(expected, result);
        }
    }
}
