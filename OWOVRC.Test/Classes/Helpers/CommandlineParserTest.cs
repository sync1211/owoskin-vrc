using OWOVRC.Classes.Helpers;

namespace OWOVRC.Test.Classes.Helpers
{
    [TestClass]
    public class CommandlineParserTest
    {
        [DataTestMethod]
        [DataRow(new string[] { "--start", "--affinity" }, true, null)]
        [DataRow(new string[] { "--start", "--affinity=" }, true, null)]
        [DataRow(new string[] { "--start", "--affinity=FFFFFF" }, true, 0xFFFFFF)]
        [DataRow(new string[] { "--start", "--affinity=0xFFFFFF" }, true, 0xFFFFFF)]
        [DataRow(new string[] { "--start", "--affinity=0000" }, true, null)]
        [DataRow(new string[] { "--start", "--affinity=0x000" }, true, null)]
        [DataRow(new string[] { "--affinity=0x0F0" }, false, 0x0F0)]
        [DataRow(new string[] { "--affinity=0xF00", "--start" }, true, 0xF00)]
        [DataRow(new string[] { "--start" }, true, null)]
        public void TestParseArguments(string[] args, bool autostart, int? affinity)
        {
           CommandlineParser parser = new(args);

            Assert.AreEqual(autostart, parser.Autostart);
            Assert.AreEqual(affinity, parser.CpuAffinity);
        }
    }
}
