using OWOVRC.Classes;
using OWOVRC.Classes.Helpers;
using System.Diagnostics;

namespace OWOVRC.Test.Classes.Helpers
{
    [TestClass]
    public class CommandlineParserTest
    {
        [DataTestMethod]
        [DataRow(new string[] { "--start", "--affinity" }, true, null, null)]
        [DataRow(new string[] { "--start", "--affinity=", "--process-priority=0" }, true, null, ProcessPriorityClass.Normal)]
        [DataRow(new string[] { "--start", "--affinity=FFFFFF", "--process-priority=-1" }, true, 0xFFFFFF, ProcessPriorityClass.BelowNormal)]
        [DataRow(new string[] { "--start", "--affinity=0xFFFFFF", "--process-priority=10" }, true, 0xFFFFFF, null)]
        [DataRow(new string[] { "--start", "--affinity=0000" }, true, null, null)]
        [DataRow(new string[] { "--start", "--affinity=0x000", "--process-priority=2" }, true, null, ProcessPriorityClass.High)]
        [DataRow(new string[] { "--affinity=0x0F0" }, false, 0x0F0, null)]
        [DataRow(new string[] { "--affinity=0xF00", "--start" }, true, 0xF00, null)]
        [DataRow(new string[] { "--start" }, true, null, null)]
        [DataRow(new string[] { "--start", "--vrc-affinity=0xF0", "--affinity=0xF00" }, true, 0xF00, null)]
        public void TestParseArguments(string[] args, bool autostart, int? affinity, ProcessPriorityClass? priority)
        {
           CommandlineParser parser = new(args);

            Assert.AreEqual(autostart, parser.Autostart);
            Assert.AreEqual(affinity, parser.CpuAffinity);
            Assert.AreEqual(priority, parser.Priority);
        }

        [TestMethod] //NOTE: Separate test as the inverse is dependent on the core count of the machine executing the test.
        public void TestParseVrcAffinity()
        {
            string[] args = ["--start", "--affinity=0xF00", "--vrc-affinity=0x2"]; // 0x2 -> Core #2 (assuming at least 2 cores for compatbility with GitHub Actions)
            CommandlineParser parser = new(args);

            Assert.IsTrue(parser.Autostart);
            Assert.AreEqual(CPUHelper.InvertAffinityValue(0x2), parser.CpuAffinity);
            Assert.IsNull(parser.Priority);
        }
    }
}
