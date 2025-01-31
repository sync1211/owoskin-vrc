using NAudio.Dsp;
using static OWOVRC.Audio.Classes.AnalyzedAudioSample;

namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioChannel
    {
        private readonly int Amplification;
        public float SubBass
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.SubBass.Start, AudioSpectrum.SubBass.End);
            }
        }
        public float Bass
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.Bass.Start, AudioSpectrum.Bass.End);
            }
        }
        public float LowMid
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.LowMid.Start, AudioSpectrum.LowMid.End);
            }
        }
        public float Mid
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.Mid.Start, AudioSpectrum.Mid.End);
            }
        }
        public float HighMid
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.HighMid.Start, AudioSpectrum.HighMid.End);
            }
        }
        public float Presence
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.Presence.Start, AudioSpectrum.Presence.End);
            }
        }
        public float Brilliance
        {
            get
            {
                return GetFrequencyRange(AudioSpectrum.Brilliance.Start, AudioSpectrum.Brilliance.End);
            }
        }

        public readonly Complex[] Buffer;
        public readonly double Period;
        public readonly AudioChannel ChannelIndentifier;
        public readonly Func<int, float> GetChannelValue;

        public AnalyzedAudioChannel(Complex[] fftBuffer, double period, AudioChannel channel, int amplification = 1_000)
        {
            this.Buffer = fftBuffer;
            this.Period = period;
            Amplification = amplification;
            ChannelIndentifier = channel;

            if (channel == AudioChannel.Left)
            {
                GetChannelValue = GetChannelValueL;
            }
            else
            {
                GetChannelValue = GetChannelValueR;
            }
        }

        public float GetFrequency(int frequency)
        {
            int actualFrequency = (int)(frequency / Period);

            return GetChannelValue(actualFrequency) * Amplification;
        }

        public float GetFrequencyRange(int start, int end)
        {
            int actualStart = (int)(start / Period);
            int actualEnd = (int)(end / Period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(GetChannelValue(i), highest);
            }

            return (float)highest * Amplification;
        }

        // Helper functions for getting the value corresponding to the channel of the object (chosen during initialization)
        private float GetChannelValueR(int i)
        {
            return Buffer[i].Y;
        }

        private float GetChannelValueL(int i)
        {
            return Buffer[i].X;
        }
    }
}
