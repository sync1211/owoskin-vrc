using OWOGame;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.UI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWOVRC.UI.Test.Forms.Dialogs
{
    [TestClass]
    public class PresetsFormTest
    {
        [TestMethod]
        public void TestGetNonCollidingNameNone()
        {
            const string name = "Testo";
            const string expected = "Testo (1)";

            string sensationStringDummy = Sensation.Ball.ToString();
            List<OSCSensationPreset> presets =
            [
                new OSCSensationPreset(true, "Test", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (1)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (3)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (4)", 1, 100, false, false, sensationStringDummy)
            ];

            string result = PresetsForm.GetNonCollidingName(name, presets, StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetNonCollidingNameSingle()
        {
            const string name = "Test";
            const string expected = "Test (2)";

            string sensationStringDummy = Sensation.Ball.ToString();
            List<OSCSensationPreset> presets =
            [
                new OSCSensationPreset(true, "Test", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (1)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (3)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (4)", 1, 100, false, false, sensationStringDummy)
            ];

            string result = PresetsForm.GetNonCollidingName(name, presets, StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestGetNonCollidingNameMultiple()
        {
            const string name = "Test";
            const string expected = "Test (3)";

            string sensationStringDummy = Sensation.Ball.ToString();
            List<OSCSensationPreset> presets =
            [
                new OSCSensationPreset(true, "Test", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (1)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (2)", 1, 100, false, false, sensationStringDummy),
                new OSCSensationPreset(true, "Test (4)", 1, 100, false, false, sensationStringDummy)
            ];

            string result = PresetsForm.GetNonCollidingName(name, presets, StringComparison.CurrentCultureIgnoreCase);

            Assert.AreEqual(expected, result);
        }
    }
}
