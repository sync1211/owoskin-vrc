using OWOVRC.Audio.Classes;
using OWOVRC.Audio.WinForms.Classes;
using OWOVRC.Audio.WinForms.Controls;
using OWOVRC.Classes.Effects;

namespace OWOVRC.UI.Forms
{
    public partial class AudioMonitorForm : Form
    {
        private readonly AudioEffect effect;
        private readonly ScalingHelper scalingHelper;
        public float SubBassThreshold;
        public float BassThreshold;
        public float TrebleThreshold;
        public float LowMidThreshold;
        public float MidThreshold;

        private readonly Font regularFont = new("Segoe UI", 9F, FontStyle.Regular);
        private readonly Font boldFont = new("Segoe UI", 9F, FontStyle.Bold);

        public AudioMonitorForm(AudioEffect effect)
        {
            this.effect = effect;

            float maxThreshold = GetMaxThreshold();
            scalingHelper = new(maxThreshold);

            InitializeComponent();
            ImportThresholdsFromSettings();
        }

        /// <summary>
        /// Get the maximum threshold value
        /// </summary>
        private float GetMaxThreshold()
        {
            float[] floats = [0, BassThreshold, SubBassThreshold, TrebleThreshold, LowMidThreshold, MidThreshold];
            return floats.Max();
        }

        private void AudioMonitorForm_Shown(object sender, EventArgs e)
        {
            effect.OnSampleRead += Analyzer_OnSampleRead;
        }

        private void AudioMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            effect.OnSampleRead -= Analyzer_OnSampleRead;
        }

        private void Analyzer_OnSampleRead(object? sender, AnalyzedAudioSample samples)
        {
            try
            {
                Invoke(UpdateBars, samples);
            }
            catch (ObjectDisposedException)
            {
                this.Close();
            }
        }

        private void UpdateBar(float dbLeft, float dbRight, BarIndicator leftBar, BarIndicator rightBar, Label leftLabel, Label rightLabel, float threshold)
        {
            // Sub-Bass
            leftBar.Value = scalingHelper.ToPercentage(dbLeft);
            rightBar.Value = scalingHelper.ToPercentage(dbRight);

            double dbLeftRound = Math.Round(dbLeft, 2);
            double dbRightRound = Math.Round(dbRight, 2);

            leftLabel.Text = $"{dbLeftRound}db";
            rightLabel.Text = $"{dbRightRound}db";

            leftLabel.Font = dbLeftRound >= threshold ? boldFont : regularFont;
            rightLabel.Font = dbRightRound >= threshold ? boldFont : regularFont;

            leftBar.IndicatorValue = scalingHelper.ToPercentage(threshold);
            rightBar.IndicatorValue = scalingHelper.ToPercentage(threshold);
        }

        private void UpdateBars(AnalyzedAudioSample sample)
        {
            AnalyzedAudioChannel leftSample = sample.Left;
            AnalyzedAudioChannel rightSample = sample.Right;

            // Sub-Bass
            UpdateBar(
                leftSample.SubBass,
                rightSample.SubBass,
                subBassIndicatorLeft,
                subBassIndicatorRight,
                leftSubBassDBLabel,
                rightSubBassDBLabel,
                SubBassThreshold
            );

            // Bass
            UpdateBar(
                leftSample.Bass,
                rightSample.Bass,
                bassIndicatorLeft,
                bassIndicatorRight,
                leftBassDBLabel,
                rightBassDBLabel,
                BassThreshold
            );

            // Treble
            UpdateBar(
                leftSample.Brilliance,
                rightSample.Brilliance,
                trebleIndicatorLeft,
                trebleIndicatorRight,
                leftTrebleDBLabel,
                rightTrebleDBLabel,
                TrebleThreshold
            );

            // Low-Mid
            UpdateBar(
                leftSample.LowMid,
                rightSample.LowMid,
                lowMidIndicatorLeft,
                lowMidIndicatorRight,
                leftLowMidDBLabel,
                rightLowMidDBLabel,
                LowMidThreshold
            );

            // Mid
            UpdateBar(
                leftSample.Mid,
                rightSample.Mid,
                midIndicatorLeft,
                midIndicatorRight,
                leftMidDBLabel,
                rightMidDBLabel,
                MidThreshold
            );

            // Max amplitude
            double maxAmplitude = Math.Round(scalingHelper.MaxAmplitude, 2);
            maxDBLabel.Text = $"{maxAmplitude}db";
        }

        public void ImportThresholdsFromSettings()
        {
            SubBassThreshold = effect.Settings.SubBassSettings.MinDB;
            BassThreshold = effect.Settings.BassSettings.MinDB;
            TrebleThreshold = effect.Settings.TrebleSettings.MinDB;
            LowMidThreshold = effect.Settings.LowMidSettings.MinDB;
            MidThreshold = effect.Settings.MidSettings.MinDB;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
