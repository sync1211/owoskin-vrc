using OWOVRC.Audio.Classes;

namespace OWOVRC.Audio.UI
{
    public partial class AudioDemoForm : Form
    {
        private readonly AudioAnalyzer analyzer = new();
        private readonly System.Timers.Timer timer;

        private AnalyzedAudioFrame? lastFrameR;
        private AnalyzedAudioFrame? lastFrameL;

        public AudioDemoForm()
        {
            InitializeComponent();
            timer = new()
            {
                Interval = 10
            };
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, EventArgs args)
        {
            Tuple<AnalyzedAudioFrame?, AnalyzedAudioFrame?> frames = analyzer.AnalyzeAudioStereo();

            lastFrameL = frames.Item1;
            lastFrameR = frames.Item2;

            Invoke(UpdateTrackBars);
        }

        private void UpdateTrackBars()
        {
            subBassIndicatorLeft.Value = lastFrameL?.SubBass ?? 0;
            bassIndicatorLeft.Value = lastFrameL?.Bass ?? 0;
            lowMidIndicatorLeft.Value = lastFrameL?.LowMid ?? 0;
            midIndicatorLeft.Value = lastFrameL?.Mid ?? 0;
            highMidIndicatorLeft.Value = lastFrameL?.HighMid ?? 0;
            presenceIndicatorLeft.Value = lastFrameL?.Presence ?? 0;
            brillianceIndicatorLeft.Value = lastFrameL?.Brilliance ?? 0;

            subBassIndicatorRight.Value = lastFrameR?.SubBass ?? 0;
            bassIndicatorRight.Value = lastFrameR?.Bass ?? 0;
            lowMidIndicatorRight.Value = lastFrameR?.LowMid ?? 0;
            midIndicatorRight.Value = lastFrameR?.Mid ?? 0;
            highMidIndicatorRight.Value = lastFrameR?.HighMid ?? 0;
            presenceIndicatorRight.Value = lastFrameR?.Presence ?? 0;
            brillianceIndicatorRight.Value = lastFrameR?.Brilliance ?? 0;
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
            timer.Stop();
            analyzer.Stop();

            stopButton.Enabled = false;
            startButton.Enabled = true;
        }

        private void Start()
        {
            timer.Start();
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
