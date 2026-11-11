using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Effects.Sensations;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes.Extensions;
using OWOVRC.UI.Controls;

namespace OWOVRC.UI.Forms.Monitors
{
    public partial class SpeedMonitorForm : Form
    {
        private readonly VelocityEffect velocityEffect;

        // Monitor
        private const int UPDATE_INVERVAL = 50;
        private readonly System.Timers.Timer refreshTimer;

        private bool oscActiveStatus;
        private float velocityThreshold = 0.1f;

        private readonly Font regularFont = new("Segoe UI", 9F, FontStyle.Regular);
        private readonly Font boldFont = new("Segoe UI", 9F, FontStyle.Bold);

        // Preview
        private bool previewMode;
        private bool previewActive;
        private readonly WindSensation windSensation;
        private readonly VelocityEffectSettings effectSettings;
        private readonly OWOHelper owo;

        public SpeedMonitorForm(VelocityEffect velocityEffect, VelocityEffectSettings effectSettings, OWOHelper owo)
        {
            InitializeComponent();
            this.velocityEffect = velocityEffect;
            this.effectSettings = effectSettings;
            this.owo = owo;

            windSensation = new WindSensation(VelocityEffect.SENSATION_DURATION);


            refreshTimer = new System.Timers.Timer()
            {
                Interval = UPDATE_INVERVAL,
                AutoReset = true
            };

            topDirectionIndicator.ValueClicked += HandleTopValueClicked;
            topDirectionIndicator.ValueDrag += HandleTopValueClicked;

            sideDirectionIndicator.ValueClicked += HandleSideValueClicked;
            sideDirectionIndicator.ValueDrag += HandleSideValueClicked;


            refreshTimer.Elapsed += OnTimerElapsed;

            UpdateVelocityDisplay();
            UpdateControlVisibility();
            refreshTimer.Start();
        }

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            try
            {
                this.InvokeIfRequired(UpdateVelocityDisplay);
            }
            catch (ObjectDisposedException) { }
        }

        private decimal SpeedFromPercentage(float input)
        {
            return (decimal) (input * (effectSettings.MaxSpeed / 100));
        }

        public void HandleTopValueClicked(object? sender, IndicatorMouseEventValues values)
        {
            if (!previewMode)
            {
                return;
            }

            xSpeedInput.Value = SpeedFromPercentage(values.ValueX); // Speed X (L-R)
            zSpeedInput.Value = SpeedFromPercentage(values.ValueY); // Speed Z (Fwd-Back)

            UpdatePreview();
        }

        public void HandleSideValueClicked(object? sender, IndicatorMouseEventValues values)
        {
            if (!previewMode)
            {
                return;
            }

            zSpeedInput.Value = SpeedFromPercentage(values.ValueX); // Speed Z (Fwd-Back)
            ySpeedInput.Value = SpeedFromPercentage(values.ValueY); // Speed Y (Up-Down)

            UpdatePreview();
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

            // Grounded / Seated indicators
            groundedIndicator.Checked = velocityEffect.IsGrounded;
            seatedIndicator.Checked = velocityEffect.IsSeated;
        }

        public void SetOSCStatus(bool isActive)
        {
            oscActiveStatus = isActive;
            UpdateControlVisibility();
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


        // Preview

        private void UpdateControlVisibility()
        {
            startTestButton.Visible = !previewActive && previewMode;
            stopTestButton.Visible = previewActive && previewMode;

            playerStateGroup.Visible = !previewMode;

            previewButton.Enabled = !previewMode;
            monitorButton.Enabled = previewMode;

            speedTitle.Text = previewMode
                ? "Intensity"
                : "Speed";


            if (previewMode) {
                notRunningIndicator.Visible = !OWOHelper.IsConnected;
                notRunningIndicator.Text = $"Please connect to OWO to preview the sensation.";

                sideDirectionIndicator.Cursor = Cursors.Cross;
                topDirectionIndicator.Cursor = Cursors.Cross;
            }
            else
            {
                notRunningIndicator.Visible = !oscActiveStatus;
                notRunningIndicator.Text = $"Indicators will not be updated while OWOVRC is stopped.";

                sideDirectionIndicator.Cursor = Cursors.Default;
                topDirectionIndicator.Cursor = Cursors.Default;
            }
        }

        private int SpeedToPercentage(float input)
        {
            return (int)((input / effectSettings.MaxSpeed) * 100);
        }

        private void UpdatePreview()
        {
            //float totalSpeed = (float)speedInput.Value;
            float speedX = SpeedToPercentage((float) xSpeedInput.Value);
            float speedY = SpeedToPercentage((float) ySpeedInput.Value);
            float speedZ = SpeedToPercentage((float) zSpeedInput.Value);

            // Top view
            topDirectionIndicator.ValueX = speedX; // Left / Right
            topDirectionIndicator.ValueY = speedZ; // Forward / Backward

            // Side view
            sideDirectionIndicator.ValueX = speedZ; // Forward / Backward
            sideDirectionIndicator.ValueY = speedY; // Up / Down

            if (!previewMode)
            {
                return;
            }

            speedLabel.Text = $"{effectSettings.Intensity}%";
            windSensation.UpdateDirection(speedX, speedY, speedZ, effectSettings.Intensity);
            
            if (previewActive)
            {
                StartSensation();
            }
            else
            {
                StopSensation();
            }
        }

        private void StartSensation()
        {
            windSensation.Play(owo, effectSettings.Priority);
        }

        private void StopSensation()
        {
            owo.StopSensation(windSensation.Name);
        }

        private void StartTestButton_Click(object sender, EventArgs e)
        {
            previewActive = true;
            UpdateControlVisibility();

            stopTestButton.Focus();

            // Start test sensation
            UpdatePreview();
        }

        private void StopTestButton_Click(object sender, EventArgs e)
        {
            previewActive = false;
            UpdateControlVisibility();

            startTestButton.Focus();

            UpdatePreview();
        }

        private void MonitorButton_Click(object sender, EventArgs e)
        {
            previewMode = false;
            refreshTimer.Start();

            UpdateVelocityDisplay();
            UpdateControlVisibility();
            UpdatePreview();
        }

        private void PreviewButton_Click(object sender, EventArgs e)
        {
            previewMode = true;
            refreshTimer.Stop();

            UpdateControlVisibility();
            UpdatePreview();
        }

        private void SpeedInput_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }
    }
}
