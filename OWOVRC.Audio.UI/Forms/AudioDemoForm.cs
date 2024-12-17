using OWOVRC.Audio.Classes;
using OWOVRC.Audio.UI.Classes;

namespace OWOVRC.Audio.UI
{
    public partial class AudioDemoForm : Form
    {
        private readonly AudioAnalyzer analyzer = new();
        //private readonly System.Timers.Timer timer;

        private AnalyzedAudioSample? lastSampleR;
        private AnalyzedAudioSample? lastSampleL;

        private readonly ScalingHelper scalingHelper = new(20);

        public AudioDemoForm()
        {
            InitializeComponent();
            //timer = new()
            //{
            //    Interval = 10
            //};
            //timer.Elapsed += Timer_Elapsed;

            analyzer.OnSampleRead += Analyzer_OnSampleRead;
        }

        void Analyzer_OnSampleRead(object? sender, Tuple<AnalyzedAudioSample, AnalyzedAudioSample> samples)
        {
            lastSampleL = samples.Item1;
            lastSampleR = samples.Item2;

            Invoke(UpdateTrackBars);
        }

        private void UpdateTrackBars()
        {
            subBassIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.SubBass ?? 0);
            bassIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.Bass ?? 0);
            lowMidIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.LowMid ?? 0);
            midIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.Mid ?? 0);
            highMidIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.HighMid ?? 0);
            presenceIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.Presence ?? 0);
            brillianceIndicatorLeft.Value = scalingHelper.ToPercentage(lastSampleL?.Brilliance ?? 0);

            subBassIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.SubBass ?? 0);
            bassIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.Bass ?? 0);
            lowMidIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.LowMid ?? 0);
            midIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.Mid ?? 0);
            highMidIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.HighMid ?? 0);
            presenceIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.Presence ?? 0);
            brillianceIndicatorRight.Value = scalingHelper.ToPercentage(lastSampleR?.Brilliance ?? 0);

            maxAmplitudeLabel.Text = scalingHelper.MaxAmplitude.ToString();
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
            //timer.Stop();
            analyzer.Stop();

            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void Start()
        {
            //timer.Start();
            analyzer.Start();

            stopButton.Enabled = true;
            startButton.Enabled = false;
        }

        private void AudioDemoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            analyzer.Dispose();
        }
    }
}
