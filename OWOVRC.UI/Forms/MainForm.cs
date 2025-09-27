using OwoAdvancedSensationBuilder.manager;
using NAudio.CoreAudioApi;
using OWOGame;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using OWOVRC.UI.Controls;
using OWOVRC.UI.Forms;
using OWOVRC.UI.Forms.Dialogs;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Net;
using System.ComponentModel;
using OWOVRC.Classes.Helpers;
using OWOVRC.UI.Forms.Monitors;

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
        private AudioEffectSettings audioSettings = new();

        // OWO
        private OSCReceiver? receiver;
        private readonly OWOHelper owo = new();
        private const string UnnamedSensationName = "<Unnamed>";

        // Effects
        private OSCEffectBase[] oscEffects = [];
        private WorldIntegrator? owi;
        private AudioEffect? audioEffect;
        private Velocity? velocityEffect;

        // Status
        private bool IsRunning;

        // Timer for UI updates
        private readonly System.Timers.Timer uiUpdateTimer;

        // Skip next change event (we only want to fire ValueChanged on user interactions)
        //NOTE: Set these to true when setting the value programmatically
        private bool oscPortInput_SkipValueChanged;

        // Indicates that the sensations display needs to be updated on the next UI update
        private bool sensationsChanged;

        // DataSource for activeSensationListBox
        private readonly BindingList<string> activeSensationList = [];

        // Monitor forms (single-instance, not shown as dialogs)
        private AudioMonitorForm? audioMonitorForm;
        private SpeedMonitorForm? speedMonitorForm;

        public MainForm(LoggingLevelSwitch? logLevelSwitch = null)
        {
            InitializeComponent();
            ClearSensationDetails();

            // Logger (replaces pre-UI logger)
            this.logLevelSwitch = Logging.SetUpWithTextBox(logBox, logLevelSwitch);

            // Update UI every 0.1 Seconds
            uiUpdateTimer = new()
            {
                Interval = 100
            };
            uiUpdateTimer.Elapsed += HandleTimerElapsed;

            activeSensationsListBox.DataSource = activeSensationList;
            owo.OnSensationChange += HandleSensationChange;
        }

        private void HandleSensationChange(object? sender, AdvancedSensationStreamInstance instance)
        {
            sensationsChanged = true;
        }

        private void HandleTimerElapsed(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                return;
            }

            // Update status panel
            if (InvokeRequired)
            {
                try
                {
                    this.Invoke(UpdateConnectionStatus);
                }
                catch (ObjectDisposedException)
                {
                    Close();
                }
            }
            else
            {
                UpdateConnectionStatus();
            }

            // Update sensations panel
            if (sensationsChanged)
            {
                if (InvokeRequired)
                {
                    try
                    {
                        this.Invoke(UpdateASMStatus);
                    }
                    catch (ObjectDisposedException)
                    {
                        Close();
                    }
                }
                else
                {
                    UpdateASMStatus();
                }

                sensationsChanged = false;
            }
        }

        private void LoadSettings()
        {
            // Connection settings
            ConnectionSettings? loadedConnectionSettings = SettingsHelper
                .LoadSettingsFromFile("connection.json", "connection", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings);
            if (loadedConnectionSettings != null)
            {
                this.connectionSettings = loadedConnectionSettings;
            }

            // Colliders effect settings
            CollidersEffectSettings? loadedCollidersSettings = SettingsHelper
                .LoadSettingsFromFile("colliders.json", "colliders effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings);
            if (loadedCollidersSettings != null)
            {
                this.collidersSettings = loadedCollidersSettings;
            }

            // Velocity effect settings
            VelocityEffectSettings? loadedVelocitySettings = SettingsHelper
                .LoadSettingsFromFile("velocity.json", "velocity effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings);
            if (loadedVelocitySettings != null)
            {
                this.velocitySettings = loadedVelocitySettings;
            }

            // OWI settings
            WorldIntegratorSettings? loadedOwiSettings = SettingsHelper
                .LoadSettingsFromFile("owi.json", "OWI integration", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings);
            if (loadedOwiSettings != null)
            {
                this.owiSettings = loadedOwiSettings;
            }

            // OSC Presets settings
            OSCPresetsSettings? loadedOscPresetsSettings = SettingsHelper
                .LoadSettingsFromFile("oscPresets.json", "OSC presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
            if (loadedOscPresetsSettings != null)
            {
                this.oscPresetsSettings = loadedOscPresetsSettings;
            }

            // Audio settings
            AudioEffectSettings? loadedAudioSettings = SettingsHelper
                .LoadSettingsFromFile("audio.json", "Audio", SettingsHelper.AudioEffectSettingsContext.Default.AudioEffectSettings);
            if (loadedAudioSettings != null)
            {
                this.audioSettings = loadedAudioSettings;
            }
        }

        private void UpdateControlAvailability()
        {
            startButton.Visible = !IsRunning;
            stopButton.Visible = IsRunning;

            owoIPInput.Enabled = !IsRunning;
            oscPortInput.Enabled = !IsRunning;

            openDiscoveryButton.Enabled = !IsRunning;

            audioDeviceSelectButton.Enabled = !IsRunning;

            stopSensationsButton.Enabled = IsRunning;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartConnection();
            UpdateASMStatus();
        }

        public void StartConnection()
        {
            StartOWO();
            UpdateControlAvailability();
            uiUpdateTimer.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopOWO();
            UpdateControlAvailability();

            uiUpdateTimer.Stop();

            UpdateConnectionStatus();
            UpdateASMStatus();
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
            if (receiver?.IsRunning ?? false)
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

            // Audio effect
            if (audioEffect == null)
            {
                audioStatusLabel.Text = "Not initialized";
                audioStatusLabel.ForeColor = Color.Black;
            }
            else if (audioEffect.CaptureState == CaptureState.Starting)
            {
                audioStatusLabel.Text = "Starting";
                audioStatusLabel.ForeColor = Color.Yellow;
            }
            else if (audioEffect.CaptureState == CaptureState.Capturing)
            {
                audioStatusLabel.Text = "Capturing";
                audioStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                audioStatusLabel.Text = "Stopped";
                audioStatusLabel.ForeColor = Color.Red;
            }
        }

        private void UpdateSensationsList(Dictionary<string, AdvancedSensationStreamInstance> activeSensations)
        {
            string[] sensationKeys = [.. activeSensations.Keys];

            // Disable updates until the list is fully updated
            activeSensationList.RaiseListChangedEvents = false;
            activeSensationList.Clear();

            // Import running sensations from SensationManager
            for (int i = 0; i < sensationKeys.Length; i++)
            {
                activeSensationList.Add(sensationKeys[i]);
            }

            // Trigger list update event
            activeSensationList.RaiseListChangedEvents = true;
            activeSensationList.ResetBindings();
        }

        private void UpdateASMStatus()
        {
            int selectedItemIndex = activeSensationsListBox.SelectedIndex;

            // Update entries
            Dictionary<string, AdvancedSensationStreamInstance> runningSensations = owo.GetRunningSensations();
            activeSensationsListBox.BeginUpdate();
            UpdateSensationsList(runningSensations);
            activeSensationsListBox.EndUpdate();

            // Select previously selected item
            if (selectedItemIndex >= 0 && selectedItemIndex < activeSensationsListBox.Items.Count)
            {
                activeSensationsListBox.SelectedIndex = selectedItemIndex;
            }

            UpdateSensationControls();

            // Clear details if no item is selected
            if (activeSensationsListBox.SelectedItem == null)
            {
                ClearSensationDetails();
            }
            else
            {
                AdvancedSensationStreamInstance? selectedSensation = runningSensations.Values.ElementAt(activeSensationsListBox.SelectedIndex);
                UpdateSensationDetails(selectedSensation);
            }
        }

        private void UpdateSensationControls()
        {
            // Update buttons
            bool itemSelected = activeSensationsListBox.SelectedItem != null;
            stopSelectedSensationNowButton.Enabled = itemSelected;
            stopSelectedSensationLoopButton.Enabled = itemSelected;
        }

        private void ClearOSCReceiver()
        {
            receiver?.Dispose();
            receiver = null;
        }

        private void StartOWO()
        {
            // Create OSC receiver
            ClearOSCReceiver(); // The receiver does not have a stop method, so we're re-creating it on launch

            try
            {
                receiver = new(connectionSettings.OSCPort);
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show(
                    "The selected OSC port is already in use by another process!",
                    "Failed to bind to OSC port!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Register effects
            foreach (OSCEffectBase effect in oscEffects)
            {
                receiver.OnMessageReceived += effect.OnOSCMessageReceived;
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

            // Start Audio effects
            if (audioEffect == null)
            {
                Log.Warning("Audio effect has not been initialized!");
                SetUpAudio();
            }

            if (audioSettings.Enabled && !audioEffect!.IsRunning)
            {
                audioEffect!.Start();
            }

            // Start OWO connection
            owo.Address = connectionSettings.OWOAddress;
            _ = Task.Run(StartOWOHelper);
            Log.Information("Started OWOVRC");

            IsRunning = true;
            speedMonitorForm?.SetOSCStatus(receiver.IsRunning);
        }

        private async Task StartOWOHelper()
        {
            await owo.Connect()
                .ConfigureAwait(false);
        }

        private void StopOWO()
        {
            // Unregister effects
            if (receiver != null)
            {
                foreach (OSCEffectBase effect in oscEffects)
                {
                    receiver.OnMessageReceived -= effect.OnOSCMessageReceived;
                    effect.Stop();
                }
            }

            // Stop OWI
            owi?.Stop();

            // Stop audio effect
            if (audioMonitorForm == null)
            {
                audioEffect?.Stop();
            }

            // Stop osc receiver
            ClearOSCReceiver();

            owo.Disconnect();
            Log.Information("Stopped OWOVRC");

            IsRunning = false;
            speedMonitorForm?.SetOSCStatus(false);
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (logLevelComboBox.SelectedItem is LogEventLevel level)
            {
                logLevelSwitch.MinimumLevel = level;
                Log.Information("Log level changed to {Level}", level);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            logLevelComboBox.DataSource = Logging.Levels;
            logLevelComboBox.SelectedItem = logLevelSwitch.MinimumLevel;
            logLevelComboBox.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            LoadSettings();

            velocityEffect = new(owo, velocitySettings);

            // Set up effects
            oscEffects = [
                velocityEffect,
                new Colliders(owo, collidersSettings),
                new OSCPresetTrigger(owo, oscPresetsSettings),
            ];

            // Set up OWI
            SetUpOWI();

            // Set up audio effect
            SetUpAudio();
        }

        private void SetUpOWI()
        {
            owi = new(owiSettings, owo);
        }

        private void SetUpAudio()
        {
            audioEffect = new(owo, audioSettings);
        }

        private void UpdateConnectionSettings()
        {
            oscPortInput_SkipValueChanged = true;
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
            collidersFrequencyInput.Text = collidersSettings.Frequency.ToString();
            collidersSpeedDecayCheckbox.Checked = collidersSettings.DecayEnabled;
            collidersSpeedDecayInput.Value = collidersSettings.DecayTime;

            collidersSpeedDecayInput.Enabled = collidersSpeedDecayCheckbox.Checked;
            velocityBasedGroupBox.Enabled = collidersUseVelocityCheckbox.Checked;
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
        }

        private void UpdateOSCPrestsSettings()
        {
            oscPresetsEnabledCheckbox.Checked = oscPresetsSettings.Enabled;
            oscPresetsPriorityInput.Text = oscPresetsSettings.Priority.ToString();
        }

        private void UpdateAudioSettings()
        {
            audioEnabledCheckbox.Checked = audioSettings.Enabled;
            AddAudioSettingsEntries();
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
            UpdateAudioSettings();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            uiUpdateTimer.Stop();

            // Stop logging
            Log.Information("Form closing, disposing logger..");
            Log.CloseAndFlush();

            // Remove events
            uiUpdateTimer.Elapsed -= HandleTimerElapsed;
            owo.OnSensationChange -= HandleSensationChange;

            // Stop OWO
            StopOWO();

            // Clean up OWO
            owo.Dispose();

            // Stop OSC receiver
            ClearOSCReceiver();

            // Unregister UI events
            logLevelComboBox.SelectedIndexChanged -= ComboBox1_SelectedIndexChanged;

            // Close forms
            audioMonitorForm?.Close();
            speedMonitorForm?.Close();
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

            connectionSettings.SaveToFile();
        }

        private void OscPortInput_ValueChanged(object sender, EventArgs e)
        {
            // Programmatic change -> Skip event
            if (oscPortInput_SkipValueChanged)
            {
                oscPortInput_SkipValueChanged = false;
                return;
            }

            connectionSettings.OSCPort = (int)oscPortInput.Value;

            connectionSettings.SaveToFile();
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

            // Frequency
            collidersSettings.Frequency = (int)collidersFrequencyInput.Value;

            // Decay Factor
            collidersSettings.DecayEnabled = collidersSpeedDecayCheckbox.Checked;
            collidersSettings.DecayTime = (int)collidersSpeedDecayInput.Value;

            collidersSettings.SaveToFile();
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

            velocitySettings.SaveToFile();

            speedMonitorButton.Enabled = velocitySettings.Enabled;

            speedMonitorForm?.SetMaxVelocity(velocitySettings.SpeedCap);
            speedMonitorForm?.SetMinVelocity(velocitySettings.Threshold);
        }

        private void StopSensationsButton_Click(object sender, EventArgs e)
        {
            owo.StopAllSensations();

            // Reset all effects
            foreach (OSCEffectBase effect in oscEffects)
            {
                effect.Stop();
            }

            Log.Information("Stopped all running sensations!");
            //UpdateASMStatus();
        }

        private void ApplyOwiSettingsButton_Click(object sender, EventArgs e)
        {
            owiSettings.Enabled = owiEnabledCheckbox.Checked;

            // Priority
            owiSettings.Priority = (int)owiPriorityInput.Value;

            // Log update interval
            owiSettings.UpdateInterval = (int)owiUpdateIntervalInput.Value;

            owiSettings.SaveToFile();
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
            oscPresetsSettings.SaveToFile();
        }

        private void OpenOscPresetsFormButton_Click(object sender, EventArgs e)
        {
            using (PresetsForm presetsForm = new(oscPresetsSettings, owo))
            {
                DialogResult result = presetsForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    oscPresetsSettings.SaveToFile();
                }
            }
        }

        private void ApplyAudioSettingsButton_Click(object sender, EventArgs e)
        {
            audioSettings.Enabled = audioEnabledCheckbox.Checked;

            foreach (AudioSettingsEntry entry in audioSettingsPriorityPanel1.Items)
            {
                entry.ApplyToSpectrumSettings();
            }

            audioSettings.SaveToFile();
            EnableOrDisableAudio();
            UpdateAudioMonitorThresholds();
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
            using (MuscleIntensityForm intensityForm = new(collidersSettings.MuscleIntensities, collidersSettings.GetSensation(), title: null, owoHelper: owo))
            {
                intensityForm.ShowDialog();
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
                connectionSettings.SaveToFile();
            }
        }

        private void StopSelectedSensationNowButton_Click(object sender, EventArgs e)
        {
            if (activeSensationsListBox.SelectedItem is not string sensationName)
            {
                return;
            }

            if (sensationName.Equals(UnnamedSensationName))
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

            if (sensationName.Equals(UnnamedSensationName))
            {
                sensationName = String.Empty;
            }

            owo.StopSensation(sensationName, interrupt: false);
        }

        private void StopSensationInstance(AdvancedSensationStreamInstance instance)
        {
            owo.StopSensation(instance.name, interrupt: true);

            Log.Information("Stopped sensation {0}", instance.name);
            UpdateASMStatus();
        }

        private void ActiveSensationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSensationControls();

            if (activeSensationsListBox.SelectedItem is not string sensationName)
            {
                return;
            }

            if (sensationName.Equals(UnnamedSensationName))
            {
                sensationName = String.Empty;
            }

            Dictionary<string, AdvancedSensationStreamInstance> sensations = owo.GetRunningSensations();

            AdvancedSensationStreamInstance? selectedSensation = sensations.GetValueOrDefault(sensationName);
            if (selectedSensation == null)
            {
                Log.Warning("Selected sensation {0} could not be found!", sensationName);
                //UpdateASMStatus();
                return;
            }

            // Update sensation details
            UpdateSensationDetails(selectedSensation);
        }

        public void ClearSensationDetails()
        {
            sensationNameLabel.Text = String.Empty;
            sensationLoopLabel.Text = String.Empty;
            sensationPriorityLabel.Text = String.Empty;
            sensationDurationLabel.Text = String.Empty;
            sensationBlockLowerPrioLabel.Text = String.Empty;
        }

        public void UpdateSensationDetails(AdvancedSensationStreamInstance instance)
        {
            sensationNameLabel.Text = instance.name;
            sensationLoopLabel.Text = instance.loop ? "Yes" : "No";
            sensationPriorityLabel.Text = instance.sensation.Priority.ToString();
            sensationDurationLabel.Text = instance.sensation.Duration.ToString("0.00s");
            sensationBlockLowerPrioLabel.Text = instance.blockLowerPrio ? "Yes" : "No";
        }

        private void AudioMonitorForm_Closed(object? sender, EventArgs e)
        {
            if (audioMonitorForm == null)
            {
                return;
            }

            audioMonitorForm.FormClosed -= AudioMonitorForm_Closed;
            audioMonitorForm = null;

            if (!IsRunning)
            {
                audioEffect?.Stop();
            }
        }

        private void UpdateAudioMonitorThresholds()
        {
            if (audioMonitorForm == null)
            {
                return;
            }

            audioMonitorForm.ImportThresholdsFromSettings();
        }

        private void AudioMonitorButton_Click(object sender, EventArgs e)
        {
            if (audioEffect == null)
            {
                Log.Warning("Audio effect is not initialized!");
                return;
            }

            if (!IsRunning)
            {
                audioEffect.Start();
            }

            if (audioMonitorForm == null)
            {
                audioMonitorForm = new AudioMonitorForm(audioEffect);
                audioMonitorForm.FormClosed += AudioMonitorForm_Closed;
                audioMonitorForm.Show();
            }

            audioMonitorForm.Activate();
        }

        /// <summary>
        /// Enables or disables OWI based on settings while the program is running.
        /// This is done to preserve resources, as VRC can be very CPU intensive.
        /// </summary>
        private void EnableOrDisableAudio()
        {
            if (!IsRunning || audioEffect == null)
            {
                return;
            }

            if (audioEffect.IsRunning && !audioSettings.Enabled)
            {
                audioEffect.Stop();
            }

            if (!audioEffect.IsRunning && audioSettings.Enabled)
            {
                audioEffect.Start();
            }
        }

        private void AudioDeviceSelectButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            using (MMDeviceEnumerator enumerator = new())
            {
                Log.Information("Loading audio devices...");
                MMDeviceCollection devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                MMDevice[] deviceArray = [.. devices];

                // Get index of default device
                int selectedIndex;
                Log.Debug("Getting default audio device...");
                using (MMDevice defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia))
                {
                    selectedIndex = Array.FindIndex(deviceArray, d => d.ID.Equals(defaultDevice.ID));
                }

                using (SelectionDialog<MMDevice> dialog = new(deviceArray, "Select an audio device:", "Select audio device", selectedIndex))
                {
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        audioEffect = new(owo, audioSettings, dialog.Value);
                        Log.Information("Audio device changed to {Device}", dialog.Value);
                    }
                }
            }

            Cursor = Cursors.Default;
        }

        private void AddAudioSettingsEntries()
        {
            if (audioSettings == null)
            {
                return;
            }

            audioSettingsPriorityPanel1.ClearItems();
            audioSettingsPriorityPanel1.ImportSettings(audioSettings.SpectrumSettings, owo);
        }

        private void OwiConfigureSensationsButton_Click(object sender, EventArgs e)
        {
            using (OWIIntensityListForm form = new(owiSettings.EnabledSensations))
            {
                form.ShowDialog();
            }
        }

        private void CollidersSpeedDecayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            collidersSpeedDecayInput.Enabled = collidersSpeedDecayCheckbox.Checked;
        }

        private void CollidersUseVelocityCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            velocityBasedGroupBox.Enabled = collidersUseVelocityCheckbox.Checked;
        }

        private void VelocityImpactEnabledCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            velocityImpactGroup.Enabled = velocityImpactEnabledCheckbox.Checked;
        }

        private void OwiConfigureIntensitiesButton_Click(object sender, EventArgs e)
        {
            using (MuscleIntensityForm intensityForm = new(owiSettings.MuscleIntensities, Sensation.ShotBleeding, title: null, owoHelper: owo))
            {
                intensityForm.ShowDialog();
            }
        }

        private void SpeedMonitorForm_Closed(object? sender, EventArgs e)
        {
            if (speedMonitorForm == null)
            {
                return;
            }

            speedMonitorForm.FormClosed -= SpeedMonitorForm_Closed;
            speedMonitorForm = null;
        }

        private void SpeedMonitorButton_Click(object sender, EventArgs e)
        {
            if (velocityEffect == null)
            {
                Log.Error("Velocity effect is not initialized!");
                return;
            }

            if (speedMonitorForm == null)
            {
                speedMonitorForm = new SpeedMonitorForm(velocityEffect);
                speedMonitorForm.FormClosed += SpeedMonitorForm_Closed;
                speedMonitorForm.Show();
            }

            speedMonitorForm.SetMaxVelocity(velocitySettings.SpeedCap);
            speedMonitorForm.SetMinVelocity(velocitySettings.Threshold);
            speedMonitorForm.SetOSCStatus(receiver?.IsRunning ?? false);

            speedMonitorForm.Activate();
        }
    }
}
