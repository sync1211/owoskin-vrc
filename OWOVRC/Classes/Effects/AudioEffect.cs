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
        public const string SENSATION_NAME = "AudioEffect";
        public bool IsRunning => AudioCapture?.IsListening ?? false;
        public CaptureState CaptureState => AudioCapture?.CaptureState ?? CaptureState.Stopped;
        public readonly AudioEffectSettings Settings;
        public EventHandler<AnalyzedAudioSample>? OnSampleRead;

        private AudioCapture? AudioCapture;
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

        /// <summary>
        /// </summary>
        /// <param name="device"></param>
        /// <returns>True, if the device has been changed. False if the device is identical.</returns>
        private bool ChangeDevice(MMDevice? device)
        {
            if (this.device == device)
            {
                Log.Information("Audio device unchanged");
                return false;
            }

            this.device = device;
            Log.Information("Audio device changed to {Device}", device);
            return true;
        }

        public bool SetDevice(MMDevice? device)
        {
            bool restartRequired = ChangeDevice(device);

            if (IsRunning && restartRequired)
            {
                Log.Information("Audio device has been changed, restarting audio capture...");
                Stop();
                return Start();
            }

            return true;
        }

        public async ValueTask<bool> SetDeviceAsync(MMDevice? device)
        {
            bool restartRequired = ChangeDevice(device);

            if (IsRunning && restartRequired)
            {
                Log.Information("Audio device has been changed, restarting audio capture...");
                await StopAsync();
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
                Log.Information("Searching for VRChat process '{Name}' to capture its audio output...", Settings.VRChatProcessName);
                Process[] processes = Process.GetProcessesByName(Settings.VRChatProcessName);
                if (processes.Length == 0)
                {
                    Log.Warning("Failed to start audio capture: VRChat process could not be found!");
                    return false;
                }

                processId = (uint)processes[0].Id;
                Log.Information("Found VRChat process: {Name}[{PID}]", processes[0].ProcessName, processId);
            }

            // Create new audio capture
            AudioCapture = new(device, processId, Settings.Amplification, Settings.UseHammingWindow);
            AudioCapture.OnSampleRead += Analyzer_OnSampleRead;

            return AudioCapture.Start();
        }

        public async Task StopAsync()
        {
            if (AudioCapture == null)
            {
                return;
            }

            AudioCapture.OnSampleRead -= Analyzer_OnSampleRead;
            AudioCapture.Stop();
            await AudioCapture.DisposeAsync();
            AudioCapture = null;
        }

        //NOTE: Prefer StopAsync due to weird behavior in AudioCapture.Dispose()
        public void Stop()
        {
            if (AudioCapture == null)
            {
                return;
            }

            AudioCapture.OnSampleRead -= Analyzer_OnSampleRead;
            AudioCapture.Stop();
            AudioCapture.Dispose();
            AudioCapture = null;
        }

        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
