using OWOVRC.Classes.Helpers;

namespace OWOVRC.Test.Classes.Helpers
{
    [TestClass]
    public class CPUHelperTest
    {
        [TestMethod]
        public void TestGetMaxAffinityForProcessorCount()
        {
            const long expected = 0xFF;
            const int processorCount = 8;

            long result = CPUHelper.GetMaxAffinityForProcessorCount(processorCount);

            Assert.AreEqual(expected, result, $"Expected {expected:X}, got {result:X}");
        }

        [DataTestMethod]
        [DataRow(0xFFFFFFFF, 0xFFFF, 0xFFFF0000)] // 16 cores, first 8 cores
        [DataRow(0xFFFFFFFF, 0xFFFF0000, 0xFFFF)] // 16 cores, last 8 cores
        public void TestInvertAffinityValue(long maxAffinity, long input, long expected)
        {
            long result = CPUHelper.InvertAffinityValue(input, maxAffinity);

            Assert.AreEqual(expected, result, $"Expected {expected:X}, got {result:X}");
        }
    }
}
