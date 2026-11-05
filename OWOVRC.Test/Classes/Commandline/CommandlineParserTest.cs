using OWOVRC.Classes;
using OWOVRC.Classes.Commandline;
using OWOVRC.Classes.Helpers;
using Serilog.Events;
using System.Diagnostics;

namespace OWOVRC.Test.Classes.Commandline
{
    [TestClass]
    public class CommandlineParserTest
    {
        [TestMethod]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity" }, true, null, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity=", "--process-priority=0" }, true, null, ProcessPriorityClass.Normal, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity=FFFFFF", "--process-priority=-1" }, true, 0xFFFFFF, ProcessPriorityClass.BelowNormal, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity=0xFFFFFF", "--process-priority=10" }, true, 0xFFFFFF, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity=0000" }, true, null, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--affinity=0x000", "--process-priority=2" }, true, null, ProcessPriorityClass.High, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--affinity=0x0F0" }, false, 0x0F0, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--affinity=0xF00", "--start" }, true, 0xF00, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start" }, true, null, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--vrc-affinity=0xF0", "--affinity=0xF00" }, true, 0xF00, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "", "--vrc-affinity=0xF0", "--affinity=0xF00" }, false, 0xF00, null, null)]
        [DataRow(new string[] { "OWOVRC.exe", "--start", "--log-level=debug" }, true, null, null, LogEventLevel.Debug)]
        [DataRow(new string[] { "OWOVRC.exe", "--log-level=FATAL", "--affinity=0xF00" }, false, 0xF00, null, LogEventLevel.Fatal)]
        [DataRow(new string[] { "OWOVRC.exe", "--log-level=Informatio_n" }, false, null, null, null)]
        public void TestParseArguments(string[] args, bool autostart, int? affinity, ProcessPriorityClass? priority, LogEventLevel? logLevel)
        {
            CommandlineArgs parsedArgs = CommandlineParser.Parse(args);

            Assert.AreEqual(autostart, parsedArgs.Autostart);
            Assert.AreEqual(affinity, parsedArgs.CpuAffinity);
            Assert.AreEqual(priority, parsedArgs.Priority);
            Assert.AreEqual(logLevel, parsedArgs.LogLevel);
        }

        [TestMethod] //NOTE: Separate test as the inverse is dependent on the core count of the machine executing the test.
        public void TestParseVrcAffinity()
        {
            string[] args = ["OWOVRC.exe", "--start", "--affinity=0xF00", "--vrc-affinity=0x2"]; // 0x2 -> Core #2 (assuming at least 2 cores for compatbility with GitHub Actions)
            CommandlineArgs parsedArgs = CommandlineParser.Parse(args);

            Assert.IsTrue(parsedArgs.Autostart);
            Assert.AreEqual(CPUHelper.InvertAffinityValue(0x2), parsedArgs.CpuAffinity);
            Assert.IsNull(parsedArgs.Priority);
        }
    }
}
