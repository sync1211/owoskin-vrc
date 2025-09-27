using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.UI.Forms.Monitors
{
    public partial class SpeedMonitorForm : Form
    {
        private readonly Velocity velocityEffect;

        private const int UPDATE_INVERVAL = 50;
        private readonly System.Timers.Timer refreshTimer;

        private bool oscActiveStatus;
        private float velocityThreshold = 0.1f;

        private readonly Font regularFont = new("Segoe UI", 9F, FontStyle.Regular);
        private readonly Font boldFont = new("Segoe UI", 9F, FontStyle.Bold);

        //TODO: add some visualization for direction and speed
        //TODO: display thresholds (and resulting sensation intensity?)
        //public float ThresholdX = 0;
        //public float ThresholdY = 0;
        //public float ThresholdZ = 0;

        public SpeedMonitorForm(Velocity velocityEffect)
        {
            InitializeComponent();
            this.velocityEffect = velocityEffect;

            refreshTimer = new System.Timers.Timer()
            {
                Interval = UPDATE_INVERVAL,
                AutoReset = true
            };

            refreshTimer.Elapsed += OnTimerElapsed;

            UpdateVelocities();
            refreshTimer.Start();
        }

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(UpdateVelocities);
                }
                catch (ObjectDisposedException)
                {
                    this.Close();
                }
            }
            else
            {
                UpdateVelocities();
            }
        }

        public void SetMaxVelocity(float maxVelocity)
        {
            topDirectionIndicator.MaxX = maxVelocity;
            topDirectionIndicator.MaxY = maxVelocity;

            sideDirectionIndicator.MaxX = maxVelocity;
            sideDirectionIndicator.MaxY = maxVelocity;
        }

        public void SetMinVelocity(float minVelocity)
        {
            velocityThreshold = minVelocity;

            topDirectionIndicator.ThresholdX = velocityThreshold;
            topDirectionIndicator.ThresholdY = velocityThreshold;

            sideDirectionIndicator.ThresholdX = velocityThreshold;
            sideDirectionIndicator.ThresholdY = velocityThreshold;
        }

        private void UpdateVelocities()
        {
            //NOTE: For reference:

            velXLabel.Text = velocityEffect.VelX.ToString("0.00");
            velYLabel.Text = velocityEffect.VelY.ToString("0.00");
            velZLabel.Text = velocityEffect.VelZ.ToString("0.00");

            // Mark values above the minVelocity threshold in bold
            velXLabel.Font = Math.Abs(velocityEffect.VelX) >= velocityThreshold ? boldFont : regularFont;
            velYLabel.Font = Math.Abs(velocityEffect.VelY) >= velocityThreshold ? boldFont : regularFont;
            velZLabel.Font = Math.Abs(velocityEffect.VelZ) >= velocityThreshold ? boldFont : regularFont;

            // Top view
            topDirectionIndicator.ValueX = velocityEffect.VelX;
            topDirectionIndicator.ValueY = velocityEffect.VelZ;

            // Side view
            sideDirectionIndicator.ValueX = velocityEffect.VelZ;
            sideDirectionIndicator.ValueY = velocityEffect.VelY;

            notRunningIndicator.Visible = !oscActiveStatus;
        }

        public void SetOSCStatus(bool isActive)
        {
            oscActiveStatus = isActive;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SpeedMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            refreshTimer.Dispose();
        }

        private void SpeedMonitorForm_Shown(object sender, EventArgs e)
        {
            // Force axis to be drawn without waiting for a value change
            sideDirectionIndicator.ForceUpdate();
            topDirectionIndicator.ForceUpdate();
        }
    }
}
