using System.Text.Json;
using OWOVRC.Classes.Effects.OWI;

namespace OWOVRC.Test.Classes.OWI
{
    [TestClass]
    public class OWIMessageTest
    {
        [DataTestMethod]
        [DataRow("{\"priority\": 1,\"sensation\": \"Front Wind\",\"frequency\": 100,\"duration\": 1,\"intensity\": 70,\"rampup\":0.2,\"rampdown\":0.2,\"exitdelay\":0,\"Muscles\": {\"frontMuscles\": 100}}", 1, "Front Wind", 100, 1, 70, 0.2f, 0.2f, 0)]
        [DataRow("{ \"sensation\": \"PaintBall\", \"frequency\": 5, \"duration\": 0.2, \"intensity\": 100, \"rampup\": 0, \"rampdown\": 0.1, \"exitdelay\": 0, \"Muscles\": { \"pectoral_R\": 50, \"abdominal_L\": 50, \"abdominal_R\": 25, \"arm_L\": 25 } }", 0, "PaintBall", 5, 0.2f, 100, 0, 0.1f, 0)]
        [DataRow("{\"priority\": 2,\"sensation\": \"Weight\",\"frequency\": 35,\"duration\": 2,\"intensity\": 50,\"rampup\":0.8,\"rampdown\":0,\"exitdelay\":0,\"Muscles\": {\"arm_R\": 100,\"pectoral_R\": 50,\"dorsal_R\": 25}}", 2, "Weight", 35, 2f, 50, 0.8f, 0f, 0f)]
        public void TestDeserialize(string jsonString, int priority, string sensation, int frequency, float duration, int intensity, float rampup, float rampdown, float exitdelay)
        {
            OWISensation? message = JsonSerializer.Deserialize<OWISensation>(jsonString);

            Assert.IsNotNull(message);
            Assert.AreEqual(priority, message.Priority);
            Assert.AreEqual(sensation, message.Sensation);
            Assert.AreEqual(frequency, message.Frequency);
            Assert.AreEqual(duration, message.Duration);
            Assert.AreEqual(intensity, message.Intensity);
            Assert.AreEqual(rampup, message.Rampup);
            Assert.AreEqual(rampdown, message.Rampdown);
            Assert.AreEqual(exitdelay, message.ExitDelay);
        }
    }
}
