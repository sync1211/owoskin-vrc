using OWOVRC.Classes.Effects.Muscles;

namespace OWOVRC.Test.Classes.Effects.Muscles
{
    [TestClass]
    public class MuscleCollisionDataTest
    {
        [TestMethod]
        public void TestDecay()
        {
            MuscleCollisionData data = new(22)
            {
                DecayStartIntensity = 34
            };

            data.UpdateProximity(0.7f, 5);

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(22, data.MaxCyclesLeft);
            Assert.AreEqual(5, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(0.7f, data.ProximityDelta);
            Assert.AreEqual(34, data.DecayIntensity);

            // Perform decay cycles to apply the decay effect
            data.ProcessCycle();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(21, data.MaxCyclesLeft);
            Assert.AreEqual(4, data.DecayCyclesLeft);
            Assert.AreEqual(0.8f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);
            Assert.AreEqual(27, data.DecayIntensity);

            data.ProcessCycle();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(20, data.MaxCyclesLeft);
            Assert.AreEqual(3, data.DecayCyclesLeft);
            Assert.AreEqual(0.6f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);
            Assert.AreEqual(20, data.DecayIntensity);

            // Reset the decay
            data.ResetDecay();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(20, data.MaxCyclesLeft);
            Assert.AreEqual(5, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);
            Assert.AreEqual(34, data.DecayIntensity);
        }

        [TestMethod]
        public void TestDelta()
        {
            MuscleCollisionData data = new(25);

            data.UpdateProximity(0.7f, 5);

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(25, data.MaxCyclesLeft);
            Assert.AreEqual(5, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(0.7f, data.ProximityDelta);

            // Perform decay cycles to apply the decay effect
            data.ProcessCycle();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(24, data.MaxCyclesLeft);
            Assert.AreEqual(4, data.DecayCyclesLeft);
            Assert.AreEqual(0.8f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);

            // Set a new proximity
            data.UpdateProximity(0.8f, 6);

            Assert.AreEqual(0.8f, data.CurrentProximity);
            Assert.AreEqual(25, data.MaxCyclesLeft);
            Assert.AreEqual(6, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(Math.Round(0.1f, 2), Math.Round(data.ProximityDelta, 2)); // For some reason this Assert runs into floating point errors (on a CONSTANT?????)

            data.UpdateProximity(0.6f, 2);

            Assert.AreEqual(0.6f, data.CurrentProximity);
            Assert.AreEqual(25, data.MaxCyclesLeft);
            Assert.AreEqual(2, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(0.3f, data.ProximityDelta);
        }

        [TestMethod]
        public void TestDisable()
        {
            MuscleCollisionData data = new(100);

            data.UpdateProximity(0.7f, 5);

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(5, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(0.7f, data.ProximityDelta);

            // Perform decay cycles to apply the decay effect
            data.ProcessCycle();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(4, data.DecayCyclesLeft);
            Assert.AreEqual(0.8f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);

            // Disable decay
            data.Disable();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(0, data.MaxCyclesLeft);
            Assert.AreEqual(4, data.DecayCyclesLeft);
            Assert.AreEqual(0.8f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);

            // Perform decay cycles to apply the decay effect
            data.ProcessCycle();

            Assert.AreEqual(0.7f, data.CurrentProximity);
            Assert.AreEqual(0, data.MaxCyclesLeft);
            Assert.AreEqual(3, data.DecayCyclesLeft);
            Assert.AreEqual(0.6f, data.DecayPercent);
            Assert.AreEqual(0f, data.ProximityDelta);

            // Update proximity again (will reset the maximum cycles)
            data.UpdateProximity(0.9f, 5);

            Assert.AreEqual(0.9f, data.CurrentProximity);
            Assert.AreEqual(100, data.MaxCyclesLeft);
            Assert.AreEqual(5, data.DecayCyclesLeft);
            Assert.AreEqual(1f, data.DecayPercent);
            Assert.AreEqual(Math.Round(0.2f, 2), Math.Round(data.ProximityDelta, 2));
        }
    }
}
