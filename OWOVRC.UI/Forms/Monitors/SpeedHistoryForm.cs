using OWOVRC.Classes.Effects;


namespace OWOVRC.UI.Forms.Monitors
{
    public partial class SpeedHistoryForm : Form
    {
        private readonly InertiaEffect inertiaEffect;
        private bool oscActiveStatus;
        private bool graphActive = true;

        public SpeedHistoryForm(InertiaEffect inertiaEffect)
        {
            InitializeComponent();

            this.inertiaEffect = inertiaEffect;

            inertiaEffect.OnInertiaUpdate += OnInertiaUpdate;
            RefreshGraphButtons();
        }

        private void OnInertiaUpdate(object? sender, float value)
        {
            if (!oscActiveStatus || !graphActive)
            {
                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(AddSpeedItem, [value]);
                }
                catch (ObjectDisposedException)
                {
                    this.Close();
                }
            }
            else
            {
                AddSpeedItem(value);
            }
        }

        public void SetMaxVelocity(float maxVelocity)
        {
            speedHistoryGraph.MaxY = maxVelocity;
            //TODO: Auto scaling, like we do in AudioMonitorForm?
        }

        public void SetMinDelta(float minDelta)
        {
            speedHistoryGraph.DeltaThreshold = minDelta;
        }

        private void AddSpeedItem(float value)
        {
            speedHistoryGraph.AddValue(value);
        }

        public void SetOSCStatus(bool isActive)
        {
            notRunningIndicator.Visible = !isActive;
            oscActiveStatus = isActive;
        }

        private void SpeedHistoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            inertiaEffect.OnInertiaUpdate -= OnInertiaUpdate;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResumeGraphButton_Click(object sender, EventArgs e)
        {
            graphActive = true;
            RefreshGraphButtons();
        }

        private void RefreshGraphButtons()
        {
            resumeGraphButton.Visible = !graphActive;
            pauseGraphButton.Visible = graphActive;
        }

        private void PauseGraphButton_Click(object sender, EventArgs e)
        {
            graphActive = false;
            RefreshGraphButtons();
        }
    }
}
