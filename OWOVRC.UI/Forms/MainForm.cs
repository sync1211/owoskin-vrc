using OwoAdvancedSensationBuilder.manager;
using OWOGame;
using OWOVRC.Classes;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using OWOVRC.UI.Forms;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Net;

namespace OWOVRC.UI
{
    public partial class MainForm : Form
    {
        // Logging
        private readonly LoggingLevelSwitch logLevelSwitch;

        // Settings
        private ConnectionSettings connectionSettings = new();
        private VelocityEffectSettings velocitySettings = new();
        private CollidersEffectSettings collidersSettings = new();
        private WorldIntegratorSettings owiSettings = new();
        private OSCPresetsSettings oscPresetsSettings = new();

        // OWO
        private readonly OWOHelper owo = new();
        private OSCReceiver receiver = new();

        // Effects
        private OSCEffectBase[] effects = [];
        private WorldIntegrator? owi;

        // Status
        private bool IsRunning;

        // Timer for UI updates
        private readonly System.Timers.Timer uiUpdateTimer;

        public MainForm()
        {
            InitializeComponent();

            logLevelSwitch = Logging.SetUpLogger(logBox);

            // Call UpdateASMStatus every 0.1 Seconds
            uiUpdateTimer = new();
            uiUpdateTimer.Interval = 100;
            uiUpdateTimer.Elapsed += HandleTimerTick;

            // Call UpdateConnectionStatus on every ui update
            Application.Idle += HandleApplicationIdle;
        }

        private void HandleApplicationIdle(object? sender, EventArgs e)
        {
            UpdateConnectionStatus();
        }

        private void HandleTimerTick(object? sender, EventArgs e)
        {
            Invoke(UpdateASMStatus);
        }

        private void LoadSettings()
        {
            // Connection settings
            ConnectionSettings? connectionSettings = SettingsHelper
                .LoadSettingsFromFile("connection.json", "connection", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
            if (connectionSettings != null)
            {
                this.connectionSettings = connectionSettings;
            }

            // Colliders effect settings
            CollidersEffectSettings? collidersSettings = SettingsHelper
                .LoadSettingsFromFile("colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
            if (collidersSettings != null)
            {
                this.collidersSettings = collidersSettings;
            }

            // Velocity effect settings
            VelocityEffectSettings? velocitySettings = SettingsHelper
                .LoadSettingsFromFile("velocity.json", "velocity effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings);
            if (velocitySettings != null)
            {
                this.velocitySettings = velocitySettings;
            }

            // OWI settings
            WorldIntegratorSettings? owiSettings = SettingsHelper
                .LoadSettingsFromFile("owi.json", "OWI integration", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
            if (owiSettings != null)
            {
                this.owiSettings = owiSettings;
            }

            // OSC Presets settings
            OSCPresetsSettings? oscPresetsSettings = SettingsHelper
                .LoadSettingsFromFile("oscPresets.json", "OSC presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
            if (oscPresetsSettings != null)
            {
                this.oscPresetsSettings = oscPresetsSettings;
            }
        }

        private void UpdateControlAvailability()
        {
            startButton.Visible = !IsRunning;
            stopButton.Visible = IsRunning;

            owoIPInput.Enabled = !IsRunning;
            oscPortInput.Enabled = !IsRunning;

            openDiscoveryButton.Enabled = !IsRunning;

            openOscPresetsFormButton.Enabled = !IsRunning;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartOWO();
            UpdateControlAvailability();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopOWO();
            UpdateControlAvailability();
        }

        private void UpdateConnectionStatus()
        {
            // OWO connection
            if (OWOHelper.IsConnected)
            {
                connectionStatusLabel.Text = "Connected";
                connectionStatusLabel.ForeColor = Color.Green;
            }
            else if (OWO.ConnectionState == ConnectionState.Connecting)
            {
                connectionStatusLabel.Text = "Connecting...";
                connectionStatusLabel.ForeColor = Color.Blue;
            }
            else
            {
                connectionStatusLabel.Text = "Disconnected";
                connectionStatusLabel.ForeColor = Color.Red;
            }

            // OSC receiver
            if (receiver.IsRunning)
            {
                oscStatusLabel.Text = "Running";
                oscStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                oscStatusLabel.Text = "Stopped";
                oscStatusLabel.ForeColor = Color.Red;
            }

            // OWO World Integration support
            if (owi == null)
            {
                owiStatusLabel.Text = "Not initialized";
                owiStatusLabel.ForeColor = Color.Black;
            }
            else if (owi.IsRunning)
            {
                owiStatusLabel.Text = "Running";
                owiStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                owiStatusLabel.Text = "Stopped";
                owiStatusLabel.ForeColor = Color.Red;
            }
        }

        private void UpdateASMStatus()
        {
            if (statusTabControl.SelectedTab != sensationsPage)
            {
                return;
            }

            string[] activeSensations = owo.GetRunningSensations().Keys.ToArray();
            for (int i = 0; i < activeSensations.Length; i++)
            {
                string sensationName = activeSensations[i];
                if (string.IsNullOrEmpty(sensationName))
                {
                    activeSensations[i] = "<Unnamed>";
                }
            }

            int selectedItemIndex = activeSensationsListBox.SelectedIndex;
            activeSensationsListBox.DataSource = activeSensations;

            if (selectedItemIndex >= 0 && selectedItemIndex < activeSensations.Length)
            {
                activeSensationsListBox.SelectedIndex = selectedItemIndex;
            }
        }

        private void StartOWO()
        {
            // Create OSC receiver
            receiver.Dispose(); // The receiver does not have a stop method, so we're re-creating it on launch
            receiver = new(connectionSettings.OSCPort);

            // Register effects
            owo.ClearBakedSensations();
            foreach (OSCEffectBase effect in effects)
            {
                receiver.OnMessageReceived += effect.OnOSCMessageReceived;
                effect.RegisterSensations();
            }

            // Start OSC receiver
            receiver.Start();

            // Start OWI
            if (owi == null)
            {
                Log.Warning("OWO World Integration has not been initialized!");
                SetUpOWI();
            }

            if (owiSettings.Enabled)
            {
                try
                {
                    owi!.Start();
                }
                catch (FileNotFoundException e)
                {
                    Log.Warning("Failed to start OWO World Integration: {0}", e.Message);
                }
            }

            // Start OWO connection
            owo.Address = connectionSettings.OWOAddress;
            Task.Run(StartOWOHelper);
            Log.Information("Started OWOVRC");

            IsRunning = true;
        }

        private async Task StartOWOHelper()
        {
            await owo.Connect();
        }

        private void StopOWO()
        {
            // Unregister effects
            foreach (OSCEffectBase effect in effects)
            {
                receiver.OnMessageReceived -= effect.OnOSCMessageReceived;
            }

            // Stop OWI
            owi?.Stop();

            // Stop osc receiver
            receiver.Dispose();

            owo.StopAllSensations();
            owo.Disconnect();
            Log.Information("Stopped OWOVRC");

            IsRunning = false;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (logLevelComboBox.SelectedItem is LogEventLevel level)
            {
                logLevelSwitch.MinimumLevel = level;
                Log.Information("Log level changed to {level}", level);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            logLevelComboBox.DataSource = Logging.Levels;
            logLevelSwitch.MinimumLevel = LogEventLevel.Information;
            logLevelComboBox.SelectedItem = logLevelSwitch.MinimumLevel;

            LoadSettings();

            // Set up effects
            effects = [
                new Colliders(owo, collidersSettings),
                new Velocity(owo, velocitySettings),
                new OSCPresetTrigger(owo, oscPresetsSettings)
            ];

            // Set up OWI
            SetUpOWI();
        }

        private void SetUpOWI()
        {
            owi = new(owiSettings, owo);
        }

        private void UpdateConnectionSettings()
        {
            owoIPInput.Text = connectionSettings.OWOAddress;
            oscPortInput.Text = connectionSettings.OSCPort.ToString();
        }

        private void UpdateCollidersEffectSettings()
        {
            collidersEnabledCheckbox.Checked = collidersSettings.Enabled;
            collidersPriorityInput.Text = collidersSettings.Priority.ToString();
            collidersUseVelocityCheckbox.Checked = collidersSettings.UseVelocity;
            collidersAllowContinuousCheckbox.Checked = collidersSettings.AllowContinuous;
            collidersMinIntensityInput.Text = collidersSettings.MinIntensity.ToString();
            collidersSpeedMultiplierInput.Text = collidersSettings.SpeedMultiplier.ToString();
        }

        private void UpdateVelocityEffectSettings()
        {
            velocityEnabledCheckbox.Checked = velocitySettings.Enabled;
            velocityPriorityInput.Text = velocitySettings.Priority.ToString();
            velocityThresholdInput.Text = velocitySettings.Threshold.ToString();
            velocityImpactEnabledCheckbox.Checked = velocitySettings.ImpactEnabled;
            velocityMinImpactInput.Text = velocitySettings.StopVelocityThreshold.ToString();
            velocitySpeedCapInput.Text = velocitySettings.SpeedCap.ToString();
            velocityIgnoreWhenGroundedCheckbox.Checked = velocitySettings.IgnoreWhenGrounded;
            velocityIgnoreWhenSeatedCheckbox.Checked = velocitySettings.IgnoreWhenSeated;
        }

        private void UpdateOWISettings()
        {
            owiEnabledCheckbox.Checked = owiSettings.Enabled;
            owiPriorityInput.Text = owiSettings.Priority.ToString();
            owiUpdateIntervalInput.Text = owiSettings.UpdateInterval.ToString();
            owiIntensityInput.Text = owiSettings.Intensity.ToString();
        }

        private void UpdateOSCPrestsSettings()
        {
            oscPresetsEnabledCheckbox.Checked = oscPresetsSettings.Enabled;
            oscPresetsPriorityInput.Text = oscPresetsSettings.Priority.ToString();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            owoIPInput.ValidatingType = typeof(System.Net.IPAddress);

            UpdateConnectionSettings();
            UpdateCollidersEffectSettings();
            UpdateVelocityEffectSettings();
            UpdateOWISettings();
            UpdateOSCPrestsSettings();

            uiUpdateTimer.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            uiUpdateTimer.Stop();

            // Stop logging
            Log.CloseAndFlush();

            // Remove events
            Application.Idle -= HandleApplicationIdle;
            uiUpdateTimer.Elapsed -= HandleTimerTick;

            // Stop OWO
            StopOWO();

            // Clean up OWO
            owo.Dispose();

            // Stop OSC receiver
            receiver.Dispose();
        }

        private void OwoIPInput_Exit(object sender, EventArgs e)
        {
            if (IPAddress.TryParse(owoIPInput.Text, out IPAddress? ipAddress) && ipAddress != null)
            {
                connectionSettings.OWOAddress = ipAddress.ToString();
            }
            else
            {
                owoIPInput.Text = connectionSettings.OWOAddress;
            }

            SettingsHelper.SaveSettingsToFile(connectionSettings, "connection.json", "connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
        }

        private void OscPortInput_Exit(object sender, EventArgs e)
        {
            connectionSettings.OSCPort = (int)oscPortInput.Value;

            SettingsHelper.SaveSettingsToFile(connectionSettings, "connection.json", "connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
        }

        private void ApplyCollidersSettingsButton_Click(object sender, EventArgs e)
        {
            collidersSettings.Enabled = collidersEnabledCheckbox.Checked;
            collidersSettings.UseVelocity = collidersUseVelocityCheckbox.Checked;
            collidersSettings.AllowContinuous = collidersAllowContinuousCheckbox.Checked;

            // Priority
            collidersSettings.Priority = (int)collidersPriorityInput.Value;

            // MinIintensity
            collidersSettings.MinIntensity = (int)collidersMinIntensityInput.Value;

            // Speed multiplier
            collidersSettings.SpeedMultiplier = (float)collidersSpeedMultiplierInput.Value;

            UpdateCollidersEffectSettings();
            SettingsHelper.SaveSettingsToFile(collidersSettings, "colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
        }

        private void ApplyVelocitySettingsButton_Click(object sender, EventArgs e)
        {
            velocitySettings.Enabled = velocityEnabledCheckbox.Checked;
            velocitySettings.ImpactEnabled = velocityImpactEnabledCheckbox.Checked;
            velocitySettings.IgnoreWhenGrounded = velocityIgnoreWhenGroundedCheckbox.Checked;
            velocitySettings.IgnoreWhenSeated = velocityIgnoreWhenSeatedCheckbox.Checked;

            // Priority
            velocitySettings.Priority = (int)velocityPriorityInput.Value;

            // Threshold
            velocitySettings.Threshold = (float)velocityThresholdInput.Value;

            // Min impact
            velocitySettings.StopVelocityThreshold = (float)velocityMinImpactInput.Value;

            // Speed cap
            velocitySettings.SpeedCap = (float)velocitySpeedCapInput.Value;

            UpdateVelocityEffectSettings();
            SettingsHelper.SaveSettingsToFile(velocitySettings, "velocity.json", "velocity effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings);
        }

        private void StopSensationsButton_Click(object sender, EventArgs e)
        {
            owo.StopAllSensations();

            // Reset all effects
            foreach (OSCEffectBase effect in effects)
            {
                effect.Reset();
            }

            Log.Information("Stopped all running sensations!");
        }

        private void ApplyOwiSettingsButton_Click(object sender, EventArgs e)
        {
            owiSettings.Enabled = owiEnabledCheckbox.Checked;

            // Priority
            owiSettings.Priority = (int)owiPriorityInput.Value;

            // Log update interval
            owiSettings.UpdateInterval = (int)owiUpdateIntervalInput.Value;

            // Intensity
            owiSettings.Intensity = (int)owiIntensityInput.Value; //TODO: Replace with dialog

            UpdateOWISettings();
            SettingsHelper.SaveSettingsToFile(owiSettings, "owi.json", "OWO World Integrator", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
            EnableOrDisableOWI();
        }

        /// <summary>
        /// Enables or disables OWI based on settings while the program is running.
        /// This is done to preserve resources, as VRC can be very CPU intensive.
        /// </summary>
        private void EnableOrDisableOWI()
        {
            if (!IsRunning || owi == null)
            {
                return;
            }

            if (owi.IsRunning && !owiSettings.Enabled)
            {
                owi.Stop();
            }

            if (!owi.IsRunning && owiSettings.Enabled)
            {
                owi.Start();
            }
        }

        private void ApplyOscPresetsSettingsButton_Click(object sender, EventArgs e)
        {
            oscPresetsSettings.Enabled = oscPresetsEnabledCheckbox.Checked;

            // Priority
            oscPresetsSettings.Priority = (int)oscPresetsPriorityInput.Value;
            SettingsHelper.SaveSettingsToFile(oscPresetsSettings, "oscPresets.json", "OSC Presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
        }

        private void OpenOscPresetsFormButton_Click(object sender, EventArgs e)
        {
            using (PresetsForm presetsForm = new(oscPresetsSettings))
            {
                DialogResult result = presetsForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    SettingsHelper.SaveSettingsToFile(oscPresetsSettings, "oscPresets.json", "OSC Presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
                }
            }
        }

        private void OwiLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WikiHelper.OpenURL(WorldIntegrator.OWI_GITHUB_URL);
        }

        private void CollidersHelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WikiHelper.OpenURL(WikiHelper.COLLIDERS_WIKI_URL);
        }

        private void PresetsHelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WikiHelper.OpenURL(WikiHelper.OSC_PRESETS_WIKI_URL);
        }

        private void ConfigureCollidersIntensityButton_Click(object sender, EventArgs e)
        {
            Sensation testSensation = SensationsFactory.Create(collidersSettings.Frequency, collidersSettings.SensationSeconds, 100, 0, 0, 0);
            using (MuscleIntensityForm intensityForm = new(collidersSettings.MuscleIntensities, testSensation, owo))
            {
                intensityForm.ShowDialog();
                SettingsHelper.SaveSettingsToFile(collidersSettings, "colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
            }
        }

        private void OpenDiscoveryButtom_Click(object sender, EventArgs e)
        {
            using (AppDiscoveryForm discoveryForm = new())
            {
                DialogResult result = discoveryForm.ShowDialog();

                if (result != DialogResult.OK || discoveryForm.SelectedApp == null)
                {
                    return;
                }

                owoIPInput.Text = discoveryForm.SelectedApp;
                connectionSettings.OWOAddress = discoveryForm.SelectedApp;
                SettingsHelper.SaveSettingsToFile(connectionSettings, "connection.json", "connection settings", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
            }
        }

        private void StopSelectedSensationNowButton_Click(object sender, EventArgs e)
        {
            if (activeSensationsListBox.SelectedItem is not string sensationName)
            {
                return;
            }

            if (sensationName.Equals("<Unnamed>"))
            {
                sensationName = String.Empty;
            }

            Dictionary<string, AdvancedSensationStreamInstance> sensations = owo.GetRunningSensations();

            AdvancedSensationStreamInstance? selectedSensation = sensations.GetValueOrDefault(sensationName);
            if (selectedSensation == null)
            {
                Log.Warning("The sensation {0} could not be found!", sensationName);
                return;
            }

            StopSensationInstance(selectedSensation);
        }

        private void StopSelectedSensationLoopButton_Click(object sender, EventArgs e)
        {
            if (activeSensationsListBox.SelectedItem is not string sensationName)
            {
                return;
            }

            if (sensationName.Equals("<Unnamed>"))
            {
                sensationName = String.Empty;
            }

            Dictionary<string, AdvancedSensationStreamInstance> sensations = owo.GetRunningSensations();

            AdvancedSensationStreamInstance? selectedSensation = sensations.GetValueOrDefault(sensationName);
            if (selectedSensation == null)
            {
                Log.Warning("The sensation {0} could not be found!", sensationName);
                return;
            }

            selectedSensation.LastCalculationOfCycle += StopSensationInstance;

            Log.Information("Marked sensation {0} to stop on the next loop", sensationName);
        }

        private void StopSensationInstance(AdvancedSensationStreamInstance instance)
        {
            owo.StopLoopedSensation(instance.name);

            Log.Information("Stopped sensation {0}", instance.name);

            UpdateASMStatus();
        }

        private void ActiveSensationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool itemSelected = activeSensationsListBox.SelectedItem != null;
            stopSelectedSensationNowButton.Enabled = itemSelected;
            stopSelectedSensationLoopButton.Enabled = itemSelected;
        }
    }
}
