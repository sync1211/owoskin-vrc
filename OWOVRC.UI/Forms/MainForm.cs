using System.Text.Json;
using OWOGame;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Net;
using OWOVRC.UI.Forms;
using OWOVRC.Classes.Effects.OSCPresets;

namespace OWOVRC.UI
{
    public partial class MainForm : Form
    {
        // Logging
        private readonly LoggingLevelSwitch logLevelSwitch;

        // Settings
        private readonly string settingsDir = Environment.CurrentDirectory;
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

        private bool IsRunning;

        // Forms
        private readonly PresetsForm presetsForm = new();

        public MainForm()
        {
            InitializeComponent();

            logLevelSwitch = Logging.SetUpLogger(logBox);

            // Call UpdateConnectionStatus on every ui update
            Application.Idle += OnApplicationIdle;
            presetsForm.OnSave += OnPresetsFormSave;
        }

        private void OnApplicationIdle(object? sender, EventArgs e)
        {
            UpdateConnectionStatus();
        }

        private static T? GetSettingsData<T>(string settingsDir, string filename, string displayName)
        {
            string settingsFilePath = Path.Combine(settingsDir, filename);
            if (!File.Exists(settingsFilePath))
            {
                Log.Warning("Failed to load {0} settings: file does not exist", displayName);
                return default;
            }

            string settingsData = File.ReadAllText(settingsFilePath);
            if (string.IsNullOrWhiteSpace(settingsData))
            {
                Log.Error("Failed to load {0} settings: file is empty", displayName);
                return default;
            }

            T? settings = JsonSerializer.Deserialize<T>(settingsData);
            if (settings == null)
            {
                Log.Error("Failed to load {0} settings: failed to deserialize", displayName);
                return default;
            }

            return settings;
        }

        private void LoadSettings()
        {
            // Connection settings
            ConnectionSettings? connectionSettings = GetSettingsData<ConnectionSettings>(settingsDir, "connection.json", "connection");
            if (connectionSettings != null)
            {
                this.connectionSettings = connectionSettings;
            }

            // Colliders effect settings
            CollidersEffectSettings? collidersSettings = GetSettingsData<CollidersEffectSettings>(settingsDir, "colliders.json", "colliders effect");
            if (collidersSettings != null)
            {
                this.collidersSettings = collidersSettings;
            }

            // Velocity effect settings
            VelocityEffectSettings? velocitySettings = GetSettingsData<VelocityEffectSettings>(settingsDir, "velocity.json", "velocity effect");
            if (velocitySettings != null)
            {
                this.velocitySettings = velocitySettings;
            }

            // OWI settings
            WorldIntegratorSettings? owiSettings = GetSettingsData<WorldIntegratorSettings>(settingsDir, "owi.json", "OWI integration");
            if (owiSettings != null)
            {
                this.owiSettings = owiSettings;
            }

            // OSC Presets settings
            OSCPresetsSettings? oscPresetsSettings = GetSettingsData<OSCPresetsSettings>(settingsDir, "oscPresets.json", "OSC Presets");
            if (oscPresetsSettings != null)
            {
                this.oscPresetsSettings = oscPresetsSettings;
            }
        }

        private void SaveSettings<T>(T settings, string fileName, string displayName)
        {
            string settingsData = JsonSerializer.Serialize<T>(settings);
            string settingsFilePath = Path.Combine(settingsDir, fileName);

            if (!File.Exists(settingsFilePath))
            {
                FileStream newFile = File.Create(settingsFilePath);
                newFile.Close();
            }

            File.WriteAllText(settingsFilePath, settingsData);

            Log.Information("{0} settings saved", displayName);
        }

        private void UpdateControlAvailability()
        {
            startButton.Visible = !IsRunning;
            stopButton.Visible = IsRunning;

            owoIPInput.Enabled = !IsRunning;
            oscPortInput.Enabled = !IsRunning;

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
            collidersIntensityInput.Text = collidersSettings.BaseIntensity.ToString();
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
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Idle -= OnApplicationIdle;
            presetsForm.OnSave -= OnPresetsFormSave;

            // Stop OWO
            StopOWO();
            owo.Dispose();

            // Stop OSC receiver
            receiver.Dispose();

            // Stop logging
            Log.CloseAndFlush();

            // Close presets form
            presetsForm.Close();
            presetsForm.Dispose();
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

            SaveSettings<ConnectionSettings>(connectionSettings, "connection.json", "connection settings");
        }

        private static int ValidateIntSetting(TextBox input, int settingsValue, int minValue = 0, int maxValue = int.MaxValue)
        {
            if (!int.TryParse(input.Text, out int value))
            {
                input.Text = settingsValue.ToString();
                return settingsValue;
            }

            if (value < minValue)
            {
                input.Text = minValue.ToString();
                return minValue;
            }

            if (value > maxValue)
            {
                input.Text = maxValue.ToString();
                return maxValue;
            }

            return value;
        }

        private static float ValidateFloatSetting(TextBox input, float settingsValue, float minValue = 0, float maxValue = float.MaxValue)
        {
            if (!float.TryParse(input.Text, out float value))
            {
                input.Text = settingsValue.ToString();
                return settingsValue;
            }

            if (value < minValue)
            {
                input.Text = minValue.ToString();
                return minValue;
            }

            if (value > maxValue)
            {
                input.Text = maxValue.ToString();
                return maxValue;
            }

            return value;
        }

        private void OscPortInput_Exit(object sender, EventArgs e)
        {
            connectionSettings.OSCPort = ValidateIntSetting(oscPortInput, connectionSettings.OSCPort);

            SaveSettings<ConnectionSettings>(connectionSettings, "connection.json", "connection");
        }

        private void ApplyCollidersSettingsButton_Click(object sender, EventArgs e)
        {
            collidersSettings.Enabled = collidersEnabledCheckbox.Checked;
            collidersSettings.UseVelocity = collidersUseVelocityCheckbox.Checked;
            collidersSettings.AllowContinuous = collidersAllowContinuousCheckbox.Checked;

            // Priority
            collidersSettings.Priority = ValidateIntSetting(collidersPriorityInput, collidersSettings.Priority);

            // Colliders min intensity
            collidersSettings.BaseIntensity = ValidateIntSetting(collidersIntensityInput, collidersSettings.BaseIntensity, 0, 100);

            // MinIintensity
            collidersSettings.MinIntensity = ValidateIntSetting(collidersMinIntensityInput, collidersSettings.MinIntensity, 0, 100);

            // Speed multiplier
            collidersSettings.SpeedMultiplier = ValidateFloatSetting(collidersSpeedMultiplierInput, collidersSettings.SpeedMultiplier, 0, 100);

            UpdateCollidersEffectSettings();
            SaveSettings<CollidersEffectSettings>(collidersSettings, "colliders.json", "colliders effect");
        }

        private void ApplyVelocitySettingsButton_Click(object sender, EventArgs e)
        {
            velocitySettings.Enabled = velocityEnabledCheckbox.Checked;
            velocitySettings.ImpactEnabled = velocityImpactEnabledCheckbox.Checked;
            velocitySettings.IgnoreWhenGrounded = velocityIgnoreWhenGroundedCheckbox.Checked;
            velocitySettings.IgnoreWhenSeated = velocityIgnoreWhenSeatedCheckbox.Checked;

            // Priority
            velocitySettings.Priority = ValidateIntSetting(velocityPriorityInput, velocitySettings.Priority);

            // Threshold
            velocitySettings.Threshold = ValidateFloatSetting(velocityThresholdInput, velocitySettings.Threshold);

            // Min impact
            velocitySettings.StopVelocityThreshold = ValidateFloatSetting(velocityMinImpactInput, velocitySettings.StopVelocityThreshold);

            // Speed cap
            velocitySettings.SpeedCap = ValidateFloatSetting(velocitySpeedCapInput, velocitySettings.SpeedCap);

            UpdateVelocityEffectSettings();
            SaveSettings<VelocityEffectSettings>(velocitySettings, "velocity.json", "velocity effect");
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

        private void OwiLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(WorldIntegrator.OWI_GITHUB_URL);
        }

        private void ApplyOwiSettingsButton_Click(object sender, EventArgs e)
        {
            owiSettings.Enabled = owiEnabledCheckbox.Checked;

            // Priority
            owiSettings.Priority = ValidateIntSetting(owiPriorityInput, owiSettings.Priority);

            // Log update interval
            owiSettings.UpdateInterval = ValidateIntSetting(owiUpdateIntervalInput, owiSettings.UpdateInterval, 10, 10000);

            // Intensity
            owiSettings.Intensity = ValidateIntSetting(owiIntensityInput, owiSettings.Intensity, 0, 100);

            UpdateOWISettings();
            SaveSettings<WorldIntegratorSettings>(owiSettings, "owi.json", "OWO World Integrator");
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
            oscPresetsSettings.Priority = ValidateIntSetting(oscPresetsPriorityInput, oscPresetsSettings.Priority);

            UpdateOSCPrestsSettings();
            SaveSettings<OSCPresetsSettings>(oscPresetsSettings, "oscPresets.json", "OSC Presets");
        }

        private void OnPresetsFormSave(object? sender, EventArgs args)
        {
            SaveSettings<OSCPresetsSettings>(oscPresetsSettings, "oscPresets.json", "OSC Presets");
        }

        private void OpenOscPresetsFormButton_Click(object sender, EventArgs e)
        {
            presetsForm.ShowDialog(oscPresetsSettings);
        }
    }
}
