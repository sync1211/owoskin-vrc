using OWOVRC.Audio.WinForms.Classes;

namespace OWOVRC.Audio.WinForms.Test.Classes
{
    [TestClass]
    public class ScalingHelperTest
    {
        [DataTestMethod]
        [DataRow(0, 10, 0)]
        [DataRow(5, 10, 50)]
        [DataRow(10, 10, 100)]
        [DataRow(20, 10, 100)]
        public void TestToPercentage(int input, int maxAmplitude, int expected)
        {
            ScalingHelper scalingHelper = new(maxAmplitude);

            int result = scalingHelper.ToPercentage(input);
            Assert.AreEqual(expected, result);
        }
    }
}
