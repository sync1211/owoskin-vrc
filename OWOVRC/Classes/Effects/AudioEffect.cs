using NAudio.CoreAudioApi;
using NAudio.Wave;
using OWOGame;
using OWOVRC.Audio.Classes;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Diagnostics;

namespace OWOVRC.Classes.Effects
{
    public partial class AudioEffect : IDisposable
    {
        public const string VRCHAT_PROCESS_NAME = "VRChat";
        public const string SENSATION_NAME = "AudioEffect";
        public bool IsRunning => Analyzer?.IsListening ?? false;
        public CaptureState CaptureState => Analyzer?.CaptureState ?? CaptureState.Stopped;
        public readonly AudioEffectSettings Settings;
        public EventHandler<AnalyzedAudioSample>? OnSampleRead;

        private AudioCapture? Analyzer;
        private MMDevice? device;
        private readonly OWOHelper owo;

        public AudioEffect(OWOHelper owo, AudioEffectSettings settings, MMDevice? device = null)
        {
            this.device = device;
            this.owo = owo;
            Settings = settings;
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

            return (int) Math.Clamp(intensityPercent * 100, 0, 100);
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
                Sensation? sensation = ProcessSpectrum(leftSample, rightSample, spectrumSettings);

                // No sensation created -> Try next effect
                if (sensation == null)
                {
                    continue;
                }

                owo.AddSensation(SENSATION_NAME, sensation);
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

            int leftIntensity = CalculateIntensityPercentage(leftDB, spectrumSettings);
            int rightIntensity = CalculateIntensityPercentage(rightDB, spectrumSettings);

            if ((leftIntensity == 0) && (rightIntensity == 0))
            {
                return null;
            }

            // Apply muscle intensities
            Muscle[] muscles = Muscle.All;
            for (int j = 0; j < muscles.Length; j++)
            {
                Muscle muscle = muscles[j];

                int soundIntensity = OWOMuscles.IsRightMuscle(muscle)                       // Intensity calculated via audio
                    ? rightIntensity // Even id -> Right muscle
                    : leftIntensity; // Odd id -> Left muscle
                int maxIntensity = spectrumSettings.Intensities[muscle.id];                 // User-configured max intensity
                int intensity = (int) (maxIntensity * ((float)soundIntensity / 100));       // Final muscle intensity based on audio, scaled to the user-configured maximum

                muscles[j] = muscle.WithIntensity(intensity);
            }

            Log.Debug("Audio sensation created. Right {RightIntensity}% Left {LeftIntensity}%", rightIntensity, leftIntensity);
            return spectrumSettings
                .GetSensation()
                .WithMuscles(muscles);
        }

        public bool SetDevice(MMDevice? device)
        {
            this.device = device;

            if (IsRunning)
            {
                Stop();
                return Start();
            }

            return true;
        }


        public bool Start()
        {
            // Get VRChat process ID if requested
            uint? processId = null;
            if (Settings.CaptureVRCOnly)
            {
                Process[] processes = Process.GetProcessesByName(VRCHAT_PROCESS_NAME);
                if (processes.Length == 0)
                {
                    Log.Error("Could not find the VRChat process.");
                    return false;
                }

                processId = (uint)processes[0].Id;
                Log.Information("Found VRChat process: {Name}[{PID}]", processes[0].ProcessName, processId);
            }

            // Create new analyzer
            Analyzer = new(device, true, 100, null, processId);
            Analyzer.OnSampleRead += Analyzer_OnSampleRead;

            return Analyzer.Start();
        }

        public void Stop()
        {
            if (Analyzer == null)
            {
                return;
            }

            Analyzer.OnSampleRead -= Analyzer_OnSampleRead;
            Analyzer.Stop();
            Analyzer.Dispose();
            Analyzer = null;
        }

        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
