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

        [DataTestMethod]
        public void TestGetMusclesFromSensation()
        {
            // Normal sensation
            Sensation sensation = Sensation.ShotBleeding;
            Muscle[] musclesExpected = [Muscle.Pectoral_R, Muscle.Pectoral_L];

            Muscle[] musclesResult = OWOMuscles.GetMusclesFromSensation(sensation);
            CollectionAssert.AreEquivalent(musclesExpected, musclesResult, "Muscles for Sensation do not match expected.");

            // Micro sensation
            sensation = Sensation.Dart;
            musclesExpected = Muscle.All;

            musclesResult = OWOMuscles.GetMusclesFromSensation(sensation);
            CollectionAssert.AreEquivalent(musclesExpected, musclesResult, "Muscles for MicroSensation do not match expected.");

            // Baked sensation
            sensation = Sensation.Parse("0~Cast Fire~26,25,60,0,1000,0,|0%100,1%100,2%100,3%100,4%100~weapon-0~");
            musclesExpected = Muscle.Front;

            musclesResult = OWOMuscles.GetMusclesFromSensation(sensation);
            CollectionAssert.AreEquivalent(musclesExpected, musclesResult, "Muscles for BakedSensation do not match expected.");
        }
    }
}
