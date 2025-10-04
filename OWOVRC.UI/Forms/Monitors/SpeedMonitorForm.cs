using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.UI.Forms.Monitors
{
    public partial class SpeedMonitorForm : Form
    {
        private readonly VelocityEffect velocityEffect;

        private const int UPDATE_INVERVAL = 50;
        private readonly System.Timers.Timer refreshTimer;

        private bool oscActiveStatus;
        private float velocityThreshold = 0.1f;

        private readonly Font regularFont = new("Segoe UI", 9F, FontStyle.Regular);
        private readonly Font boldFont = new("Segoe UI", 9F, FontStyle.Bold);

        public SpeedMonitorForm(VelocityEffect velocityEffect)
        {
            InitializeComponent();
            this.velocityEffect = velocityEffect;

            refreshTimer = new System.Timers.Timer()
            {
                Interval = UPDATE_INVERVAL,
                AutoReset = true
            };

            refreshTimer.Elapsed += OnTimerElapsed;

            UpdateVelocityDisplay();
            refreshTimer.Start();
        }

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(UpdateVelocityDisplay);
                }
                catch (ObjectDisposedException)
                {
                    this.Close();
                }
            }
            else
            {
                UpdateVelocityDisplay();
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

        private void UpdateVelocityDisplay()
        {
            float velocityX = velocityEffect.VelX;
            float velocityY = velocityEffect.VelY;
            float velocityZ = velocityEffect.VelZ;
            double speed = velocityEffect.Speed;

            velXLabel.Text = velocityX.ToString("0.00");
            velYLabel.Text = velocityY.ToString("0.00");
            velZLabel.Text = velocityZ.ToString("0.00");
            speedLabel.Text = speed.ToString("0.00");

            // Mark values above the minVelocity threshold in bold
            velXLabel.Font = Math.Abs(velocityX) >= velocityThreshold ? boldFont : regularFont;
            velYLabel.Font = Math.Abs(velocityY) >= velocityThreshold ? boldFont : regularFont;
            velZLabel.Font = Math.Abs(velocityZ) >= velocityThreshold ? boldFont : regularFont;
            speedLabel.Font = Math.Abs(speed) >= velocityThreshold ? boldFont : regularFont;

            // Top view
            topDirectionIndicator.ValueX = velocityX; // Left / Right
            topDirectionIndicator.ValueY = velocityZ; // Forward / Backward

            // Side view
            sideDirectionIndicator.ValueX = velocityZ; // Forward / Backward
            sideDirectionIndicator.ValueY = velocityY; // Up / Down

            notRunningIndicator.Visible = !oscActiveStatus;

            // Grounded / Seated indicators
            groundedIndicator.Checked = velocityEffect.IsGrounded;
            seatedIndicator.Checked = velocityEffect.IsSeated;
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
