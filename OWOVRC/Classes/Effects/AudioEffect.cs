using NAudio.CoreAudioApi;
using OWOGame;
using OWOVRC.Audio.Classes;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public partial class AudioEffect : IDisposable
    {
        public const string SENSATION_NAME = "AudioEffect";
        public bool IsRunning => Analyzer.IsListening;
        public CaptureState CaptureState => Analyzer.CaptureState;
        public readonly AudioEffectSettings Settings;
        public EventHandler<AnalyzedAudioSample>? OnSampleRead;

        private readonly AudioCapture Analyzer;
        private readonly OWOHelper owo;

        public AudioEffect(OWOHelper owo, AudioEffectSettings settings, MMDevice? device = null)
        {
            this.owo = owo;
            Settings = settings;

            Analyzer = new(device);
            Analyzer.OnSampleRead += Analyzer_OnSampleRead;
        }

        public virtual void Analyzer_OnSampleRead(object? sender, AnalyzedAudioSample sample)
        {
            ProcessAudioSample(sample.Left, sample.Right);
            OnSampleRead?.Invoke(this, sample);
        }

        public static int CalculateIntensityPercentage(float level, AudioEffectSpectrumSettings spectrumSettings)
        {
            level = Math.Max(0f, level - spectrumSettings.MinDB);

            float range = spectrumSettings.MaxDB - spectrumSettings.MinDB;
            float intensityPercent = (level / range);

            return (int)Math.Round(intensityPercent * 100, 0);
        }

        private void ProcessAudioSample(AnalyzedAudioChannel leftSample, AnalyzedAudioChannel rightSample)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Attempt to create a sensation for each setting in order of priority
            for (int i = 0; i < Settings.SpectrumSettings.Length; i++)
            {
                AudioEffectSpectrumSettings spectrumSettings = Settings.SpectrumSettings[i];
                Sensation? sensation = ProcessSpectrum(rightSample, leftSample, spectrumSettings);

                // No sensation created -> Try next effect
                if (sensation == null)
                {
                    continue;
                }

                owo.AddSensation(sensation, SENSATION_NAME);
                break;
            }
        }

        private static Sensation? ProcessSpectrum(AnalyzedAudioChannel leftSample, AnalyzedAudioChannel rightSample, AudioEffectSpectrumSettings spectrumSettings)
        {
            if (!spectrumSettings.Enabled)
            {
                return null;
            }

            float rightDB = rightSample.GetFrequencyRange(spectrumSettings.AudioFrequencyStart, spectrumSettings.AudioFrequencyEnd);
            float leftDB = leftSample.GetFrequencyRange(spectrumSettings.AudioFrequencyStart, spectrumSettings.AudioFrequencyEnd);

            int leftIntensity = CalculateIntensityPercentage(rightDB, spectrumSettings);
            int rightIntensity = CalculateIntensityPercentage(leftDB, spectrumSettings);

            if ((leftIntensity == 0) && (rightIntensity == 0))
            {
                return null;
            }

            // Apply muscle intensities
            Muscle[] muscles = Muscle.All;
            for (int j = 0; j < muscles.Length; j++)
            {
                Muscle muscle = muscles[j];

                int soundIntensity = (muscle.id % 2 == 0)                                   // Intensity calculated via audio
                    ? rightIntensity // Even id -> Right muscle
                    : leftIntensity; // Odd id -> Left muscle
                int maxIntensity = spectrumSettings.Intensities[muscle.id];                 // User-configured max intensity
                int intensity = (int) (maxIntensity * ((float)soundIntensity / 100));       // Final muscle intensity based on audio, scaled to the user-configured maximum

                muscles[j] = muscle.WithIntensity(intensity);
            }

            Log.Debug("Audio sensation created. Right {rightIntensity}% Left {leftIntensity}%", rightIntensity, leftIntensity);
            return spectrumSettings
                .CreateSensation()
                .WithMuscles(muscles);
        }

        public void Start()
        {
            Analyzer.Start();
        }

        public void Stop()
        {
            Analyzer.Stop();
        }

        public void Dispose()
        {
            Analyzer.OnSampleRead -= OnSampleRead;
            Stop();
            Analyzer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
