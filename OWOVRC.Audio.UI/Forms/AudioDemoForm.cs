using NAudio.Dsp;
using OWOVRC.Audio.Classes;

namespace OWOVRC.Audio.UI
{
    public partial class AudioDemoForm : Form
    {
        private readonly AudioAnalyzer analyzer = new();
        private readonly System.Timers.Timer timer;

        private AnalyzedAudioFrame? lastFrame;

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
            AnalyzedAudioFrame? frame = analyzer.AnalyzeAudio();
            if (frame == null)
            {
                return;
            }

            lastFrame = frame;

            Invoke(UpdateTrackBars);
        }

        private void UpdateTrackBars()
        {
            if (lastFrame == null)
            {
                return;
            }

            subBassIndicator.Value    = lastFrame.SubBass;
            bassIndicator.Value       = lastFrame.Bass;
            lowMidIndicator.Value     = lastFrame.LowMid;
            midIndicator.Value        = lastFrame.Mid;
            highMidIndicator.Value    = lastFrame.HighMid;
            presenceIndicator.Value   = lastFrame.Presence;
            brillianceIndicator.Value = lastFrame.Brilliance;
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
