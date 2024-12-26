using OWOVRC.Audio.Classes;
using OWOVRC.Audio.WinForms.Classes;
using System.Diagnostics;

namespace OWOVRC.Audio.Demo
{
    public partial class AudioDemoForm : Form
    {
        private readonly AudioCapture analyzer = new();

        private AnalyzedAudioSample? lastSample;

        private readonly ScalingHelper scalingHelper = new(20);

        public AudioDemoForm()
        {
            InitializeComponent();

            analyzer.OnSampleRead += Analyzer_OnSampleRead;
        }

        void Analyzer_OnSampleRead(object? sender, AnalyzedAudioSample sample)
        {
            lastSample = sample;

            try
            {
                Invoke(UpdateTrackBars);
            }
            catch (ObjectDisposedException)
            {
                analyzer.OnSampleRead -= Analyzer_OnSampleRead;
            }
        }

        private void UpdateTrackBars()
        {
            subBassIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.SubBass ?? 0);
            bassIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.Bass ?? 0);
            lowMidIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.LowMid ?? 0);
            midIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.Mid ?? 0);
            highMidIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.HighMid ?? 0);
            presenceIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.Presence ?? 0);
            brillianceIndicatorLeft.Value = scalingHelper.ToPercentage(lastSample?.Left.Brilliance ?? 0);

            subBassIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.SubBass ?? 0);
            bassIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.Bass ?? 0);
            lowMidIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.LowMid ?? 0);
            midIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.Mid ?? 0);
            highMidIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.HighMid ?? 0);
            presenceIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.Presence ?? 0);
            brillianceIndicatorRight.Value = scalingHelper.ToPercentage(lastSample?.Right.Brilliance ?? 0);

            maxAmplitudeLabel.Text = $"{Math.Round(scalingHelper.MaxAmplitude, 2)}db";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            analyzer.Stop();

            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void Start()
        {
            analyzer.Start();

            stopButton.Enabled = true;
            startButton.Enabled = false;
        }

        private void AudioDemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();

            analyzer.OnSampleRead -= Analyzer_OnSampleRead;

            analyzer.Dispose();
        }
    }
}
