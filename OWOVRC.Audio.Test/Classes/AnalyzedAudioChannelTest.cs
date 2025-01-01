using NAudio.Dsp;
using OWOVRC.Audio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static OWOVRC.Audio.Classes.AnalyzedAudioSample;

namespace OWOVRC.Audio.Test.Classes
{
    [TestClass]
    public class AnalyzedAudioChannelTest
    {
        [TestMethod]
        public void TestGetFrequency()
        {
            Complex[] buffer = new Complex[1024];
            for (int i = 100; i < 200; i++)
            {
                buffer[i] = new Complex() { X = 34, Y = 2 };
            }

            for (int i = 200; i < buffer.Length; i++)
            {
                buffer[i] = new Complex() { X = 14, Y = 4 };
            }

            const int period = 1;
            AnalyzedAudioChannel channelL = new(buffer, period, AudioChannel.Left, 100);
            AnalyzedAudioChannel channelR = new(buffer, period, AudioChannel.Right, 100);

            float resultL = channelL.GetFrequency(198);
            float resultR = channelR.GetFrequency(198);

            Assert.AreEqual(3400, resultL);
            Assert.AreEqual(200, resultR);
        }

        [TestMethod]
        public void TestGetFrequencyRange()
        {
            Complex[] buffer = new Complex[1024];
            for (int i = 0; i < 100; i++)
            {
                buffer[i] = new Complex() { X = 44, Y = 2 };
            }

            for (int i = 100; i < buffer.Length; i++)
            {
                buffer[i] = new Complex() { X = 32, Y = 1 };
            }

            const int period = 1;
            AnalyzedAudioChannel channelL = new(buffer, period, AudioChannel.Left, 200);
            AnalyzedAudioChannel channelR = new(buffer, period, AudioChannel.Right, 200);

            float resultR = channelR.GetFrequencyRange(0, 100);
            float resultL = channelL.GetFrequencyRange(0, 100);

            Assert.AreEqual(8800, resultL);
            Assert.AreEqual(400, resultR);
        }
    }
}
