using System.Text.Json;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Test.Classes.Settings
{
    [TestClass]
    public class OSCPresetsSettingsTest
    {
        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            Dictionary<string, OSCSensationPreset> presets = new()
            {
                {"Test1", new OSCSensationPreset(true, "TestPreset", 9, 2, false, "4~Ball~100,1,100,0,0,0,Impact|0%100~impact-0~Impacts") }
            };
            OSCPresetsSettings settings = new(presets)
            {
                Priority = 2,
                Enabled = false
            };

            string json = JsonSerializer.Serialize(settings);
            Assert.AreNotEqual(0, json.Length);

            OSCPresetsSettings? decodedSettings = JsonSerializer.Deserialize<OSCPresetsSettings>(json);
            Assert.IsNotNull(decodedSettings);

            Assert.AreEqual(settings.Enabled, decodedSettings.Enabled);
            Assert.AreEqual(settings.Priority, decodedSettings.Priority);
            Assert.AreEqual(settings.Presets.Count, decodedSettings.Presets.Count);
            Assert.AreEqual(settings.Presets["Test1"].Name, decodedSettings.Presets["Test1"].Name);
        }
    }
}
