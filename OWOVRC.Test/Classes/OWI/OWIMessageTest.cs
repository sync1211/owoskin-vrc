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

        [TestMethod]
        public void TestDeserializeMuscles()
        {
            string data = "{ \"priority\": 2,\"sensation\": \"Recoil\",\"frequency\": 1,\"duration\": 1,\"intensity\": 70,\"rampup\":0,\"rampdown\":1.5,\"exitdelay\":0,\"Muscles\": { \"arm_R\": 25,\"pectoral_R\": 50}}";
            Dictionary<string, int> muscles = new()
            {
                { "arm_R", 25 },
                { "pectoral_R", 50 }
            };

            OWISensation? message = JsonSerializer.Deserialize<OWISensation>(data);

            Assert.IsNotNull(message);
            Assert.IsNotNull(message.Muscles);

            foreach (KeyValuePair<string, int> muscle in muscles)
            {
                Assert.IsTrue(message.Muscles.ContainsKey(muscle.Key));
                Assert.AreEqual(muscle.Value, message.Muscles[muscle.Key]);
            }
        }

        [TestMethod]
        public void TestJsonEncodeDecode()
        {
            Dictionary<string, int> muscles = new()
            {
                { "arm_R", 66 },
                { "pectoral_R", 87 }
            };

            OWISensation message = new(
                priority: 2,
                sensation: "Recoil",
                frequency: 1,
                duration: 1,
                intensity: 70,
                rampup: 0,
                rampdown: 1.5f,
                exitdelay: 0,
                muscles: muscles
            );

            string json = JsonSerializer.Serialize(message);
            Assert.AreNotEqual(0, json.Length);

            OWISensation? decodedMessage = JsonSerializer.Deserialize<OWISensation>(json);
            Assert.IsNotNull(decodedMessage);

            Assert.AreEqual(message.Priority, decodedMessage.Priority);
            Assert.AreEqual(message.Sensation, decodedMessage.Sensation);
            Assert.AreEqual(message.Frequency, decodedMessage.Frequency);
            Assert.AreEqual(message.Duration, decodedMessage.Duration);
            Assert.AreEqual(message.Intensity, decodedMessage.Intensity);
            Assert.AreEqual(message.Rampup, decodedMessage.Rampup);
            Assert.AreEqual(message.Rampdown, decodedMessage.Rampdown);
            Assert.AreEqual(message.ExitDelay, decodedMessage.ExitDelay);
            Assert.AreEqual(message.Muscles.Count, decodedMessage.Muscles.Count);

            foreach (KeyValuePair<string, int> muscle in message.Muscles)
            {
                Assert.IsTrue(decodedMessage.Muscles.ContainsKey(muscle.Key));
                Assert.AreEqual(muscle.Value, decodedMessage.Muscles[muscle.Key]);
            }
        }
    }
}
