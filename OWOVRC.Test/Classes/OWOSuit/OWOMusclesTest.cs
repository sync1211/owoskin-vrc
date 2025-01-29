using OWOGame;
using OWOVRC.Classes.OWOSuit;
using System.Reflection;

namespace OWOVRC.Test.Classes.OWOSuit
{
    [TestClass]
    public class OWOMusclesTest
    {
        [TestMethod]
        public void TestMuscleSide()
        {
            // (Future proofing) Make sure identification of muscle side via ID still works
            // * ID even => right
            // * ID odd => left

            foreach (FieldInfo prop in typeof(Muscle).GetFields())
            {
                if (!(prop.Name.EndsWith("_L") || prop.Name.EndsWith("_R")))
                {
                    continue;
                }

                if (prop.GetValue(null) is not Muscle muscle)
                {
                    continue;
                }

                bool expected = prop.Name.EndsWith("_R");
                bool result = OWOMuscles.IsRightMuscle(muscle);

                Assert.AreEqual(expected, result, $"Muscle {prop.Name} (ID: {muscle.id}): Side detection incorrect!");
            }
        }
    }
}
