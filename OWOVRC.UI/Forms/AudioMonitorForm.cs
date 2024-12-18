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
            bassIndicatorLeft.Value = scalingHelper.ToPercentage(leftSample.Bass);
            bassIndicatorRight.Value = scalingHelper.ToPercentage(rightSample.Bass);

            double dbLeft = Math.Round(leftSample.Bass, 2);
            double dbRight = Math.Round(rightSample.Bass, 2);

            leftBassDBLabel.Text = $"{dbLeft}db";
            rightBassDBLabel.Text = $"{dbRight}db";

            double maxAmplitude = Math.Round(scalingHelper.MaxAmplitude, 2);
            maxDBLabel.Text = $"{maxAmplitude}db";
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
