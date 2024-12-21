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

        private void Analyzer_OnSampleRead(object? sender, Tuple<AnalyzedAudioSample, AnalyzedAudioSample> samples)
        {
            try
            {
                Invoke(UpdateBars, [samples.Item1, samples.Item2]);
            }
            catch (ObjectDisposedException)
            {
                return; //TODO: Fix this properly!
            }
        }

        private void UpdateBars(AnalyzedAudioSample leftSample, AnalyzedAudioSample rightSample)
        {
            // Sub-Bass
            subBassIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.SubBass);
            subBassIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.SubBass);

            double bassDbLeft = Math.Round(leftSample.Bass, 2);
            double bassDBRight = Math.Round(rightSample.Bass, 2);

            leftBassDBLabel.Text = $"{bassDbLeft}db";
            rightBassDBLabel.Text = $"{bassDBRight}db";

            // Bass
            bassIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.Bass);
            bassIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.Bass);

            double subBassDbLeft = Math.Round(leftSample.SubBass, 2);
            double subBassDBRight = Math.Round(rightSample.SubBass, 2);

            leftSubBassDBLabel.Text = $"{subBassDbLeft}db";
            rightSubBassDBLabel.Text = $"{subBassDBRight}db";

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
