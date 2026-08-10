using OWOVRC.Classes.Effects.Sensations;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;

namespace OWOVRC.UI.Forms
{
    public partial class SpeedTestForm : Form
    {
        private bool testActive;
        private readonly OWOHelper owo;
        private readonly WindSensation windSensation;
        private readonly VelocityEffectSettings effectSettings;

        public SpeedTestForm(OWOHelper owo, VelocityEffectSettings effectSettings)
        {
            InitializeComponent();
            this.owo = owo;
            this.effectSettings = effectSettings;

            windSensation = new WindSensation(VelocityEffect.SENSATION_DURATION);
        }

        public void UpdateValues()
        {
            UpdateLimits();

            playerXBar.Value = SpeedToPercentage((float)xSpeedInput.Value);
            playerYBar.Value = SpeedToPercentage((float)ySpeedInput.Value);
            playerZBar.Value = SpeedToPercentage((float)zSpeedInput.Value);


            UpdateSensation();
        }

        private void UpdateButtonVisibility()
        {
            startTestButton.Visible = !testActive;
            stopTestButton.Visible = testActive;
        }

        private void StartTestButton_Click(object sender, EventArgs e)
        {
            testActive = true;
            UpdateButtonVisibility();

            // Start test sensation
            UpdateSensation();
        }

        private void StopTestButton_Click(object sender, EventArgs e)
        {
            testActive = false;
            UpdateButtonVisibility();

            // Stop test sensation
            owo.StopSensation(windSensation.Name);
        }

        private void UpdateSensation()
        {
            //float totalSpeed = (float)speedInput.Value;
            float totalSpeed = effectSettings.MaxSpeed;
            float speedX = totalSpeed * (playerXBar.Value / 100f);
            float speedY = totalSpeed * (playerYBar.Value / 100f);
            float speedZ = totalSpeed * (playerZBar.Value / 100f);

            // Top view
            topDirectionIndicator.ValueX = speedX; // Left / Right
            topDirectionIndicator.ValueY = speedZ; // Forward / Backward

            // Side view
            sideDirectionIndicator.ValueX = speedZ; // Forward / Backward
            sideDirectionIndicator.ValueY = speedY; // Up / Down

            // Update indicator
            UpdateLimits();

            windSensation.UpdateDirection(speedX, speedY, speedZ, effectSettings.Intensity);

            if (testActive)
            {
                //TODO: Does not seem to be looping :[
                windSensation.Play(owo, effectSettings.Priority);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (testActive)
            {
                StopTestButton_Click(sender, e);
            }

            Close();
        }

        private void PlayerZBar_Scroll(object sender, EventArgs e)
        {
            playerZBar2.Value = playerZBar.Value;
            zSpeedInput.Value = (decimal)SpeedFromPercentage(playerZBar.Value);

            UpdateSensation();
        }

        private void PlayerZBar2_Scroll(object sender, EventArgs e)
        {
            playerZBar.Value = playerZBar2.Value;
            zSpeedInput.Value = (decimal)SpeedFromPercentage(playerZBar.Value);

            UpdateSensation();
        }

        private void PlayerXBar_Scroll(object sender, EventArgs e)
        {
            xSpeedInput.Value = (decimal)SpeedFromPercentage(playerXBar.Value);
            UpdateSensation();
        }

        private void PlayerYBar_Scroll(object sender, EventArgs e)
        {
            ySpeedInput.Value = (decimal)SpeedFromPercentage(playerYBar.Value);
            UpdateSensation();
        }

        private void XSpeedInput_ValueChanged(object sender, EventArgs e)
        {
            playerXBar.Value = SpeedToPercentage((float)xSpeedInput.Value);
            UpdateSensation();
        }

        private void YSpeedInput_ValueChanged(object sender, EventArgs e)
        {
            playerYBar.Value = SpeedToPercentage((float)ySpeedInput.Value);
            UpdateSensation();
        }

        private void ZSpeedInput_ValueChanged(object sender, EventArgs e)
        {
            playerZBar.Value = SpeedToPercentage((float)zSpeedInput.Value);
            playerZBar2.Value = SpeedToPercentage((float)zSpeedInput.Value);
            UpdateSensation();
        }

        private void UpdateLimits()
        {
            // Max values
            topDirectionIndicator.MaxX = effectSettings.MaxSpeed;
            topDirectionIndicator.MaxY = effectSettings.MaxSpeed;
            sideDirectionIndicator.MaxX = effectSettings.MaxSpeed;
            sideDirectionIndicator.MaxY = effectSettings.MaxSpeed;

            xSpeedInput.Maximum = (decimal)effectSettings.MaxSpeed;
            ySpeedInput.Maximum = (decimal)effectSettings.MaxSpeed;
            zSpeedInput.Maximum = (decimal)effectSettings.MaxSpeed;

            // Min values
            xSpeedInput.Minimum = (decimal)-effectSettings.MaxSpeed;
            ySpeedInput.Minimum = (decimal)-effectSettings.MaxSpeed;
            zSpeedInput.Minimum = (decimal)-effectSettings.MaxSpeed;

            // Threshold
            topDirectionIndicator.ThresholdX = effectSettings.MinSpeed;
            topDirectionIndicator.ThresholdY = effectSettings.MinSpeed;
            sideDirectionIndicator.ThresholdX = effectSettings.MinSpeed;
            sideDirectionIndicator.ThresholdY = effectSettings.MinSpeed;

            // Force redraw
            sideDirectionIndicator.ForceUpdate();
            topDirectionIndicator.ForceUpdate();
        }

        private int SpeedToPercentage(float input)
        {
            return (int)((input / effectSettings.MaxSpeed) * 100);
        }

        private float SpeedFromPercentage(int input)
        {
            return input * (effectSettings.MaxSpeed / 100);
        }

        private void SpeedTestForm_Shown(object sender, EventArgs e)
        {
            UpdateLimits();
        }
    }
}
