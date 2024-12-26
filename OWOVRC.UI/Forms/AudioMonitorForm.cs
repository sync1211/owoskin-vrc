using OWOVRC.Audio.Classes;
using OWOVRC.Audio.WinForms.Classes;
using OWOVRC.Classes.Effects;

namespace OWOVRC.UI.Forms
{
    public partial class AudioMonitorForm : Form
    {
        private readonly AudioEffect effect;
        private readonly ScalingHelper scalingHelper = new(0);

        public AudioMonitorForm(AudioEffect effect)
        {
            this.effect = effect;
            InitializeComponent();
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
                Invoke(UpdateBars, [samples.Left, samples.Right]);
            }
            catch (ObjectDisposedException)
            {
                this.Close();
            }
        }

        private void UpdateBars(AnalyzedAudioChannel leftSample, AnalyzedAudioChannel rightSample)
        {
            // Sub-Bass
            subBassIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.SubBass);
            subBassIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.SubBass);

            double bassDBLeft = Math.Round(leftSample.Bass, 2);
            double bassDBRight = Math.Round(rightSample.Bass, 2);

            leftBassDBLabel.Text = $"{bassDBLeft}db";
            rightBassDBLabel.Text = $"{bassDBRight}db";

            // Bass
            bassIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.Bass);
            bassIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.Bass);

            double subBassDBLeft = Math.Round(leftSample.SubBass, 2);
            double subBassDBRight = Math.Round(rightSample.SubBass, 2);

            leftSubBassDBLabel.Text = $"{subBassDBLeft}db";
            rightSubBassDBLabel.Text = $"{subBassDBRight}db";

            // Treble
            trebleIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.Brilliance);
            trebleIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.Brilliance);

            double trebleDBLeft = Math.Round(leftSample.Brilliance, 2);
            double trebleDBRight = Math.Round(rightSample.Brilliance, 2);

            leftTrebleDBLabel.Text = $"{trebleDBLeft}db";
            rightTrebleDBLabel.Text = $"{trebleDBRight}db";

            // Max amplitude
            double maxAmplitude = Math.Round(scalingHelper.MaxAmplitude, 2);
            maxDBLabel.Text = $"{maxAmplitude}db";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
