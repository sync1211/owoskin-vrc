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
        public void TestParseArguments(string[] args, bool autostart, int? affinity, ProcessPriorityClass? priority)
        {
           CommandlineParser parser = new(args);

            Assert.AreEqual(autostart, parser.Autostart);
            Assert.AreEqual(affinity, parser.CpuAffinity);
            Assert.AreEqual(priority, parser.Priority);
        }
    }
}
