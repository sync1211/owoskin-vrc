﻿namespace OWOVRC.Audio.Classes
{
    public class AnalyzedAudioSample
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

        private readonly double[] fftBuffer;
        private readonly double period;

        public AnalyzedAudioSample(double[] fftBuffer, double period, int amplification = 1_000)
        {
            this.fftBuffer = fftBuffer;
            this.period = period;
            Amplification = amplification;
        }

        public float GetFrequency(int frequency)
        {
            int actualFrequency = (int) (frequency / period);
            return (float) (fftBuffer[actualFrequency] * Amplification);
        }

        public float GetFrequencyRange(int start, int end)
        {
            int actualStart = (int)(start / period);
            int actualEnd = (int)(end / period);

            double highest = 0;
            for (int i = actualStart; i <= actualEnd; i++)
            {
                highest = Math.Max(fftBuffer[i], highest);
            }

            //int length = end - start;
            return (float)(highest * Amplification);
        }
    }
}
