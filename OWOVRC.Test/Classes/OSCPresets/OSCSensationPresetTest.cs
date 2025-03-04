using OWOVRC.Classes.Effects.OSCPresets;
using System.Text.Json;

namespace OWOVRC.Test.Classes.OSCPresets
{
    [TestClass]
    public class OSCSensationPresetTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            OSCSensationPreset preset = new(false, "Test", 9, 84, true, true, "4~Ball~100,1,100,0,0,0,Impact|0%100~impact-0~Impacts");

            string json = JsonSerializer.Serialize(preset);
            Assert.AreNotEqual(0, json.Length);

            OSCSensationPreset? decodedPreset = JsonSerializer.Deserialize<OSCSensationPreset>(json);
            Assert.IsNotNull(decodedPreset);

            Assert.AreEqual(preset.Enabled, decodedPreset.Enabled);
            Assert.AreEqual(preset.Path, decodedPreset.Path);
            Assert.AreEqual(preset.Priority, decodedPreset.Priority);
            Assert.AreEqual(preset.Intensity, decodedPreset.Intensity);
            Assert.AreEqual(preset.SensationString, decodedPreset.SensationString);
            Assert.AreEqual(preset.Loop, decodedPreset.Loop);
            Assert.AreEqual(preset.Interruptable, decodedPreset.Interruptable);
        }
    }
}
