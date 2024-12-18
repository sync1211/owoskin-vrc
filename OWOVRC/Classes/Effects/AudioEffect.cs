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
        public bool IsRunning => Analyzer.IsListening;
        public readonly AudioEffectSettings Settings;
        public EventHandler<Tuple<AnalyzedAudioSample, AnalyzedAudioSample>>? OnSampleRead;

        private readonly AudioAnalyzer Analyzer;
        private readonly OWOHelper owo;

        public AudioEffect(OWOHelper owo, AudioEffectSettings settings, MMDevice? device = null)
        {
            this.owo = owo;
            Settings = settings;

            Analyzer = new(device);
            Analyzer.OnSampleRead += Analyzer_OnSampleRead;
        }

        public virtual void Analyzer_OnSampleRead(object? sender, Tuple<AnalyzedAudioSample, AnalyzedAudioSample> sample)
        {
            ProcessAudioSample(sample.Item1, sample.Item2);
            OnSampleRead?.Invoke(this, sample);
        }

        private int CalculateIntensityPercentage(float level)
        {
            level = Math.Max(0, level - Settings.MinBass);

            int range = Settings.MaxBass - Settings.MinBass;
            float intensityPercent = (float)((float)level / (float)range);

            return (int)(intensityPercent * Settings.MaxIntensity);
        }

        private MicroSensation CreateSensation()
        {
            return SensationsFactory.Create(Settings.Frequency, Settings.SensationSeconds);
        }

        private void ProcessAudioSample(AnalyzedAudioSample leftSample, AnalyzedAudioSample rightSample)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            int leftIntensity = CalculateIntensityPercentage(leftSample.Bass);
            int rightIntensity = CalculateIntensityPercentage(rightSample.Bass);

            if ((leftIntensity == 0) && (rightIntensity == 0))
            {
                return;
            }

            Muscle[] muscles =
            [
                Muscle.Lumbar_L.WithIntensity(leftIntensity),
                Muscle.Abdominal_R.WithIntensity(rightIntensity),
                Muscle.Lumbar_R.WithIntensity(rightIntensity),
                Muscle.Abdominal_L.WithIntensity(leftIntensity)
            ];

            owo.AddSensation(CreateSensation(), muscles);
            Log.Debug("Audio sensation created. Right {rightIntensity}% Left {leftIntensity}%", rightIntensity, leftIntensity);
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
