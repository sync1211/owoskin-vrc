using Avalonia.Controls;
using NAudio.CoreAudioApi;
using OwoAdvancedSensationBuilder.manager;
using OWOGame;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Forms;
using OWOVRC.UI.Forms.Monitors;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using VRC.OSCQuery;
using OWOVRC.AvaloniaUI.Classes.Logging;
using System.Windows.Forms;
using Avalonia.Media;
using Serilog.Events;
using OWOVRC.UI.Classes.Helpers;
using OWOVRC.UI.Classes.Proxies;
using OWOVRC.UI.Forms.Dialogs;
using Avalonia.Input;
using System.Linq;
using Avalonia.Interactivity;

namespace OWOVRC.AvaloniaUI.Forms
{
    public partial class MainWindow : Window
    {
        // Logging
        private readonly LoggingLevelSwitch logLevelSwitch;

        // Settings
        private ConnectionSettings connectionSettings = new();
        private VelocityEffectSettings velocitySettings = new();
        private InertiaEffectSettings inertiaSettings = new();
        private CollidersEffectSettings collidersSettings = new();
        private WorldIntegratorSettings owiSettings = new();
        private OSCPresetsSettings oscPresetsSettings = new();
        private AudioEffectSettings audioSettings = new();

        // OSC
        private OSCReceiver? receiver;

        // OWO
        private readonly OWOHelper owo = new();
        private const string UnnamedSensationName = "<Unnamed>";

        // Effects
        private OSCEffectBase[] oscEffects = [];
        private WorldIntegrator? owi;
        private AudioEffect? audioEffect;
        private VelocityEffect? velocityEffect;
        private InertiaEffect? inertiaEffect;
        private OSCPresetTrigger? presetsEffect;

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
        private SpeedHistoryForm? speedHistoryForm;

        // Token for cancelling the start/connection process
        private CancellationTokenSource cancellationTokenSource = new();

        // Brushes for text coloring
        private readonly IBrush RedColorBrush = Brushes.Red;
        private readonly IBrush BlueColorBrush = Brushes.Blue;
        private readonly IBrush GreenColorBrush = Brushes.Green;
        private readonly IBrush BlackColorBrush = Brushes.Black;

        // Cursors
        private readonly Avalonia.Input.Cursor WaitCursor = new Avalonia.Input.Cursor(StandardCursorType.Wait);
        private readonly Avalonia.Input.Cursor DefaultCursor = Avalonia.Input.Cursor.Default;

        public MainWindow(LoggingLevelSwitch? logLevelSwitch = null)
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

            activeSensationsListBox.ItemsSource = activeSensationList;
            owo.OnSensationChange += HandleSensationChange;
        }

        private void HandleSensationChange(object? sender, AdvancedSensationStreamInstance instance)
        {
            sensationsChanged = true;
        }

        private void HandleTimerElapsed(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                return;
            }

            // Update status panel
            try
            {
                this.InvokeIfRequired(UpdateConnectionStatus);
            }
            catch (ObjectDisposedException)
            {
                this.InvokeIfRequired(Close);
            }

            // Update sensations panel
            if (sensationsChanged)
            {
                try
                {
                    this.InvokeIfRequired(UpdateASMStatus);
                }
                catch (ObjectDisposedException)
                {
                    this.InvokeIfRequired(Close);
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

            // Velocity effect settings
            InertiaEffectSettings? loadedInertiaSettings = SettingsHelper
                .LoadSettingsFromFile("inertia.json", "inertia effect", SettingsHelper.InertiaEffectSettingsContext.Default.InertiaEffectSettings);
            if (loadedInertiaSettings != null)
            {
                this.inertiaSettings = loadedInertiaSettings;
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

            // Update buttons
            UpdateVelocityMonitorButtonState();
            UpdateInertiaMonitorButtonState();
        }

        private void UpdateControlAvailability()
        {
            startButton.IsVisible = !IsRunning;
            stopButton.IsVisible = IsRunning;

            owoIPInput.IsEnabled = !IsRunning;
            oscPortInput.IsEnabled = !IsRunning || !(useOSCQueryCheckbox.IsChecked ?? false);
            useOSCQueryCheckbox.IsEnabled = !IsRunning;

            openDiscoveryButton.IsEnabled = !IsRunning;

            audioDeviceSelectButton.IsEnabled = !IsRunning;

            stopSensationsButton.IsEnabled = IsRunning;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartConnection();
            UpdateASMStatus();

        }

        public async void StartConnection()
        {
            if (!cancellationTokenSource.TryReset())
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new();
            }

            try
            {
                if (!await StartOWO(cancellationTokenSource.Token))
                {
                    StopOWO(); // Clean up anything that's not fully started
                    return;
                }

                UpdateControlAvailability();
                uiUpdateTimer.Start();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while starting the connection!");
                MessageBox.Show(
                    $"An unexpected error occurred while starting the connection.{Environment.NewLine}See log output for more info.",
                    "Failed to start connection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                StopOWO();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
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
                connectionStatusLabel.Content = "Connected";
                connectionStatusLabel.Foreground = GreenColorBrush;
            }
            else if (OWO.ConnectionState == ConnectionState.Connecting)
            {
                connectionStatusLabel.Content = "Connecting...";
                connectionStatusLabel.Foreground = BlueColorBrush;
            }
            else
            {
                connectionStatusLabel.Content = "Disconnected";
                connectionStatusLabel.Foreground = RedColorBrush;
            }

            // OSC receiver
            if (receiver?.IsRunning ?? false)
            {
                oscStatusLabel.Content = "Running";
                oscStatusLabel.Foreground = GreenColorBrush;
            }
            else
            {
                oscStatusLabel.Content = "Stopped";
                oscStatusLabel.Foreground = GreenColorBrush;
            }

            // OWO World Integration support
            if (owi == null)
            {
                owiStatusLabel.Content = "Not initialized";
                owiStatusLabel.Foreground = BlackColorBrush;
            }
            else if (owi.IsRunning)
            {
                owiStatusLabel.Content = "Running";
                owiStatusLabel.Foreground = GreenColorBrush;
            }
            else
            {
                owiStatusLabel.Content = "Stopped";
                owiStatusLabel.Foreground = RedColorBrush;
            }

            // Audio effect
            if (audioEffect == null)
            {
                audioStatusLabel.Content = "Not initialized";
                audioStatusLabel.Foreground = BlackColorBrush;
            }
            else if (audioEffect.CaptureState == CaptureState.Starting)
            {
                audioStatusLabel.Content = "Starting";
                audioStatusLabel.Foreground = BlueColorBrush;
            }
            else if (audioEffect.CaptureState == CaptureState.Capturing)
            {
                audioStatusLabel.Content = "Capturing";
                audioStatusLabel.Foreground = GreenColorBrush;
            }
            else
            {
                audioStatusLabel.Content = "Stopped";
                audioStatusLabel.Foreground = RedColorBrush;
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
            //activeSensationsListBox.BeginUpdate();
            UpdateSensationsList(runningSensations);
            //activeSensationsListBox.EndUpdate();

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
            stopSelectedSensationNowButton.IsEnabled = itemSelected;
            stopSelectedSensationLoopButton.IsEnabled = itemSelected;
        }

        private void ClearOSCReceiver()
        {
            receiver?.Dispose();
            receiver = null;
        }

        private async Task<bool> StartOWO(CancellationToken cancellationToken = default)
        {
            // Create OSC receiver
            ClearOSCReceiver(); // The receiver does not have a stop method, so we're re-creating it on launch

            int oscPort = connectionSettings.OSCPort;

            // Create OSCQuery helper
            if (connectionSettings.UseOSCQuery)
            {
                oscPort = Extensions.GetAvailableUdpPort();
            }

            try
            {
                receiver = new(oscPort, connectionSettings.UseOSCQuery, "OWOVRC-UI");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show(
                    "The selected OSC port is already in use by another process!",
                    "Failed to bind to OSC port!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            // Register effects
            foreach (OSCEffectBase effect in oscEffects)
            {
                effect.RegisterCallbacks(receiver);
            }

            // Start OSC receiver
            receiver.Start();

            // Wait for VRChat client connection
            IsRunning = true;
            UpdateControlAvailability();

            bool result = await ConnectToVRChat(cancellationToken);
            if (!result)
            {
                StopOWO();
                return false;
            }

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

            _ = Task.Run(StartOWOHelper, CancellationToken.None);
            Log.Information("Started OWOVRC");

            speedMonitorForm?.SetOSCStatus(receiver.IsRunning);
            speedHistoryForm?.SetOSCStatus(receiver.IsRunning);

            IsRunning = true;

            return true;
        }

        private async Task<bool> ConnectToVRChat(CancellationToken cancellationToken = default)
        {
            if (receiver == null)
            {
                return false;
            }

            // Wait for VRChat to be detected
            bool result = await receiver.WaitForVRChatClientConnected(connectionSettings.OSCQuery_MaxWait, connectionSettings.OSCQuery_RefreshInterval, cancellationToken);

            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            if (!result)
            {
                MessageBox.Show(
                    $"No VRChat clients found!{Environment.NewLine}Please launch VRChat before starting OWOVRC!",
                    "VRChat not found!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return false;
            }

            return true;
        }

        private async Task StartOWOHelper()
        {
            bool result = await owo.Connect()
                .ConfigureAwait(false);

            if (result)
            {
                return;
            }

            // Connection error
            try
            {
                this.InvokeIfRequired(ShowOWOConnectionError);
            }
            catch (ObjectDisposedException)
            {
                this.InvokeIfRequired(Close);
            }
        }

        private void ShowOWOConnectionError()
        {
            MessageBox.Show(
                $"An unexpected error occurred while trying to connect to the MyOWO app.{Environment.NewLine}See log output for more info.",
                "Failed to connect",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        private void StopOWO()
        {
            cancellationTokenSource.Cancel();

            // Unregister effects
            if (receiver != null)
            {
                foreach (OSCEffectBase effect in oscEffects)
                {
                    effect.UnregisterCallbacks(receiver);
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
            speedHistoryForm?.SetOSCStatus(false);
        }

        private void ComboBox1_SelectedIndexChanged(object? sender, RoutedEventArgs e)
        {
            if (logLevelComboBox.SelectedItem is LogEventLevel level)
            {
                logLevelSwitch.MinimumLevel = level;
                Log.Information("Log level changed to {Level}", level);
            }
        }

        private void MainForm_Load(object sender, RoutedEventArgs e)
        {
            logLevelComboBox.ItemsSource = Logging.Levels;
            logLevelComboBox.SelectedItem = logLevelSwitch.MinimumLevel;
            logLevelComboBox.SelectionChanged += ComboBox1_SelectedIndexChanged;

            LoadSettings();

            inertiaEffect = new(owo, inertiaSettings);
            velocityEffect = new(owo, velocitySettings);
            presetsEffect = new OSCPresetTrigger(owo, oscPresetsSettings);

            // Set up effects
            oscEffects = [
                inertiaEffect,
                velocityEffect,
                presetsEffect,
                new Colliders(owo, collidersSettings),
            ];

            // Set up OWI
            SetUpOWI();

            // Set up audio effect
            SetUpAudio();

            UpdateConnectionStatus();
            UpdateControlAvailability();
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
            oscPortInput_SkipValueChanged = true;
            owoIPInput.Text = connectionSettings.OWOAddress;
            oscPortInput.Value = connectionSettings.OSCPort;
            useOSCQueryCheckbox.IsChecked = connectionSettings.UseOSCQuery;
        }

        private void UpdateCollidersEffectSettings()
        {
            collidersEnabledCheckbox.IsChecked = collidersSettings.Enabled;
            collidersPriorityInput.Value = collidersSettings.Priority;

            collidersUseVelocityCheckbox.IsChecked = collidersSettings.UseVelocity;
            collidersMinIntensityInput.Value = collidersSettings.MinIntensity;
            collidersSpeedMultiplierInput.Value = (decimal)collidersSettings.SpeedMultiplier;

            collidersDecayInput.Value = collidersSettings.DecayTime;
            collidersDecayOnChangeCheckbox.IsChecked = collidersSettings.DecayOnChanges;
            collidersDecayOnExitCheckbox.IsChecked = collidersSettings.DecayOnExit;

            velocityBasedGroupBox.IsEnabled = (collidersUseVelocityCheckbox.IsChecked ?? false);
        }

        private void UpdateVelocityEffectSettings()
        {
            velocityEnabledCheckbox.IsChecked = velocitySettings.Enabled;

            velocityPriorityInput.Value = velocitySettings.Priority;
            velocityThresholdInput.Value = (decimal)velocitySettings.MinSpeed;
            velocitySpeedCapInput.Value = (decimal)velocitySettings.MaxSpeed;
            velocityIntensityInput.Value = velocitySettings.Intensity;

            velocityIgnoreWhenGroundedCheckbox.IsChecked = velocitySettings.IgnoreWhenGrounded;
            velocityIgnoreWhenSeatedCheckbox.IsChecked = velocitySettings.IgnoreWhenSeated;
        }

        private void UpdateInertiaEffectSettings()
        {
            inertiaEnabledCheckbox.IsChecked = inertiaSettings.Enabled;
            inertiaPriorityInput.Value = inertiaSettings.Priority;

            inertiaMinDeltaInput.Value = (decimal)inertiaSettings.MinDelta;
            inertiaMaxDeltaInput.Value = (decimal)inertiaSettings.MaxDelta;
            inertiaIntensityInput.Value = inertiaSettings.Intensity;

            inertiaIgnoreWhenGroundedCheckbox.IsChecked = inertiaSettings.IgnoreWhenGrounded;
            inertiaIgnoreWhenSeatedCheckbox.IsChecked = inertiaSettings.IgnoreWhenSeated;

            inertiaAccelCheckbox.IsChecked = inertiaSettings.AccelEnabled;
            inertiaDecelCheckbox.IsChecked = inertiaSettings.DecelEnabled;
        }

        private bool owiEnabled { get; set; }
        private void UpdateOWISettings()
        {
            owiEnabledCheckbox.IsChecked = owiSettings.Enabled;
            owiPriorityInput.Value = owiSettings.Priority;
            owiUpdateIntervalInput.Value = owiSettings.UpdateInterval;
        }

        private void UpdateOSCPrestsSettings()
        {
            oscPresetsEnabledCheckbox.IsChecked = oscPresetsSettings.Enabled;
            oscPresetsPriorityInput.Value = oscPresetsSettings.Priority;
        }

        private void UpdateAudioSettings()
        {
            audioEnabledCheckbox.IsChecked = audioSettings.Enabled;
            AddAudioSettingsEntries();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateConnectionSettings();
            UpdateCollidersEffectSettings();
            UpdateVelocityEffectSettings();
            UpdateInertiaEffectSettings();
            UpdateOWISettings();
            UpdateOSCPrestsSettings();

            uiUpdateTimer.Start();
            UpdateAudioSettings();
        }

        private void MainForm_FormClosing(object sender, WindowClosingEventArgs e)
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
            logLevelComboBox.SelectionChanged -= ComboBox1_SelectedIndexChanged;

            // Close forms
            audioMonitorForm?.Close();
            speedMonitorForm?.Close();
            speedHistoryForm?.Close();

            // Dispose effects
            for (int i = 0; i < oscEffects.Length; i++)
            {
                oscEffects[i].Dispose();
            }
        }

        private void OwoIPInput_Exit(object sender, RoutedEventArgs e)
        {
            if (IPAddress.TryParse(owoIPInput.Text, out IPAddress? ipAddress) && ipAddress != null)
            {
                if (ipAddress.AddressFamily != AddressFamily.InterNetwork)
                {
                    MessageBox.Show(
                        "The format of the entered IP-Address is not supported. OWOVRC currently only supports IPv4 addresses.",
                        "Unsupported address format",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    owoIPInput.Text = connectionSettings.OWOAddress;
                    return;
                }
                connectionSettings.OWOAddress = ipAddress.ToString();
            }
            else
            {
                owoIPInput.Text = connectionSettings.OWOAddress;
            }

            connectionSettings.SaveToFile();
        }

        private void OscPortInput_ValueChanged(object sender, NumericUpDownValueChangedEventArgs e)
        {
            // Programmatic change -> Skip event
            if (oscPortInput_SkipValueChanged)
            {
                oscPortInput_SkipValueChanged = false;
                return;
            }

            connectionSettings.OSCPort = (int)(oscPortInput.Value ?? 0);

            connectionSettings.SaveToFile();
        }

        private void ApplyCollidersSettingsButton_Click(object sender, EventArgs e)
        {
            collidersSettings.Enabled = collidersEnabledCheckbox.IsChecked ?? false;
            collidersSettings.UseVelocity = collidersUseVelocityCheckbox.IsChecked ?? false;

            // Priority
            collidersSettings.Priority = (int)(collidersPriorityInput.Value ?? 0);

            // MinIintensity
            collidersSettings.MinIntensity = (int)(collidersMinIntensityInput.Value ?? 0);

            // Speed multiplier
            collidersSettings.SpeedMultiplier = (float)(collidersSpeedMultiplierInput.Value ?? 0);

            // Decay Factor
            collidersSettings.DecayTime = (int)(collidersDecayInput.Value ?? 0);

            // Decay conditions
            collidersSettings.DecayOnExit = collidersDecayOnExitCheckbox.IsChecked ?? false;
            collidersSettings.DecayOnChanges = collidersDecayOnChangeCheckbox.IsChecked ?? false;

            collidersSettings.SaveToFile();
        }

        private void ApplyVelocitySettingsButton_Click(object sender, EventArgs e)
        {
            // Enabled
            bool oldState = velocitySettings.Enabled;
            velocitySettings.Enabled = (velocityEnabledCheckbox.IsChecked ?? false);

            // Ignore conditions
            velocitySettings.IgnoreWhenGrounded = (velocityIgnoreWhenGroundedCheckbox.IsChecked ?? false);
            velocitySettings.IgnoreWhenSeated = (velocityIgnoreWhenSeatedCheckbox.IsChecked ?? false);

            // Priority
            velocitySettings.Priority = (int)(velocityPriorityInput.Value ?? 0);

            // Threshold
            velocitySettings.MinSpeed = (float)(velocityThresholdInput.Value ?? 0);

            // Speed cap
            velocitySettings.MaxSpeed = (float)(velocitySpeedCapInput.Value ?? 0);

            // Intensity
            velocitySettings.Intensity = (int)(velocityIntensityInput.Value ?? 0);

            velocitySettings.SaveToFile();

            UpdateVelocityMonitorButtonState();

            speedMonitorForm?.SetMaxVelocity(velocitySettings.MaxSpeed);
            speedMonitorForm?.SetMinVelocity(velocitySettings.MinSpeed);

            if (!velocitySettings.Enabled)
            {
                speedMonitorForm?.Close();
            }

            // (Optimization) Unregister callbacks when disabled
            if (!IsRunning || velocityEffect == null || receiver == null || oldState == velocitySettings.Enabled)
            {
                return;
            }

            if (velocitySettings.Enabled)
            {
                velocityEffect.RegisterCallbacks(receiver);
            }
            else
            {
                velocityEffect.UnregisterCallbacks(receiver);
            }
        }

        private void UpdateVelocityMonitorButtonState()
        {
            velocityMonitorButton.IsEnabled = velocitySettings.Enabled;
        }

        private void StopSensationsButton_Click(object sender, RoutedEventArgs e)
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

        private void ApplyOwiSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            owiSettings.Enabled = (owiEnabledCheckbox.IsChecked ?? false);

            // Priority
            owiSettings.Priority = (int)(owiPriorityInput.Value ?? 0);

            // Log update interval
            owiSettings.UpdateInterval = (int)(owiUpdateIntervalInput.Value ?? 0);

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

        private void ApplyOscPresetsSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            oscPresetsSettings.Enabled = (oscPresetsEnabledCheckbox.IsChecked ?? false);

            // Priority
            oscPresetsSettings.Priority = (int)(oscPresetsPriorityInput.Value ?? 0);
            oscPresetsSettings.SaveToFile();
        }

        private void OpenOscPresetsFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (presetsEffect == null)
            {
                Log.Warning("Cannot open presets form: Presets effect not initialized!");
                return;
            }

            using (PresetsForm presetsForm = new(oscPresetsSettings, presetsEffect, receiver, owo))
            {
                DialogResult result = presetsForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    oscPresetsSettings.SaveToFile();
                }
            }
        }

        private void ApplyAudioSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            audioSettings.Enabled = (audioEnabledCheckbox.IsChecked ?? false);

            //TODO: AudioSettingsPriorityPanel
            //foreach (AudioSettingsEntry entry in audioSettingsPriorityPanel1.Items)
            //{
            //    entry.ApplyToSpectrumSettings();
            //}

            audioSettings.SaveToFile();
            EnableOrDisableAudio();
            UpdateAudioMonitorThresholds();
        }

        private void ApplyInertiaSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            bool oldState = inertiaSettings.Enabled;

            // Invalid state: Both acceleration and deceleration disabled => Enable both and disable the effect
            bool inertiaAccelEnabled = inertiaAccelCheckbox.IsChecked ?? false;
            bool inertiaDecelEnabled = inertiaDecelCheckbox.IsChecked ?? false;
            bool inertiaEnabled = inertiaEnabledCheckbox.IsChecked ?? false;

            bool isStupidState = !inertiaAccelEnabled && !inertiaDecelEnabled && inertiaEnabled;

            if (isStupidState)
            {
                // Show a message to inform the user that their current settings are stupid, then fix it for them
                Log.Warning("Both acceleration and deceleration are disabled for the inertia effect! If only we had added a way to disable effects...");
                inertiaAccelCheckbox.IsChecked = true;
                inertiaDecelCheckbox.IsChecked = true;
                inertiaEnabledCheckbox.IsChecked = false;
            }

            // Enabled
            inertiaSettings.Enabled = (inertiaEnabledCheckbox.IsChecked ?? false);

            // Priority
            inertiaSettings.Priority = (int)(inertiaPriorityInput.Value ?? 0);

            // Delta bounds
            inertiaSettings.MinDelta = (float)(inertiaMinDeltaInput.Value ?? 0);
            inertiaSettings.MaxDelta = (float)(inertiaMaxDeltaInput.Value ?? 0);

            // Intensity scale
            inertiaSettings.Intensity = (int)(inertiaIntensityInput.Value ?? 0);

            // Ignore conditions
            inertiaSettings.IgnoreWhenGrounded = (inertiaIgnoreWhenGroundedCheckbox.IsChecked ?? false);
            inertiaSettings.IgnoreWhenSeated = (inertiaIgnoreWhenSeatedCheckbox.IsChecked ?? false);

            // Activation conditions
            inertiaSettings.AccelEnabled = (inertiaAccelCheckbox.IsChecked ?? false);
            inertiaSettings.DecelEnabled = (inertiaDecelCheckbox.IsChecked ?? false);

            inertiaSettings.SaveToFile();

            UpdateInertiaMonitorButtonState();

            speedHistoryForm?.SetMinDelta(inertiaSettings.MinDelta);

            if (!inertiaSettings.Enabled)
            {
                speedHistoryForm?.Close();
            }

            // (Optimization) Unregister callbacks when disabled
            if (!IsRunning || receiver == null || inertiaEffect == null || oldState == inertiaSettings.Enabled)
            {
                return;
            }

            if (inertiaSettings.Enabled)
            {
                inertiaEffect.RegisterCallbacks(receiver);
            }
            else
            {
                inertiaEffect.UnregisterCallbacks(receiver);
            }
        }

        private void UpdateInertiaMonitorButtonState()
        {
            inertiaMonitorButton.IsEnabled = inertiaSettings.Enabled;
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
            using (AppDiscoveryForm discoveryForm = new(connectionSettings.ResolveHostnames))
            {
                DialogResult result = discoveryForm.ShowDialog();
                HostEntry? selectedApp = discoveryForm.SelectedApp;
                if (result != DialogResult.OK || selectedApp == null)
                {
                    if (connectionSettings.ResolveHostnames != discoveryForm.ResolveHostNames)
                    {
                        connectionSettings.ResolveHostnames = discoveryForm.ResolveHostNames;
                        connectionSettings.SaveToFile();
                    }
                    return;
                }

                owoIPInput.Text = selectedApp.IP;
                Log.Information("OWO app found at {HostInfo}", selectedApp);

                connectionSettings.OWOAddress = owoIPInput.Text;
                connectionSettings.ResolveHostnames = discoveryForm.ResolveHostNames;
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
            sensationNameLabel.Content = String.Empty;
            sensationLoopLabel.Content = String.Empty;
            sensationPriorityLabel.Content = String.Empty;
            sensationDurationLabel.Content = String.Empty;
            sensationBlockLowerPrioLabel.Content = String.Empty;
        }

        public void UpdateSensationDetails(AdvancedSensationStreamInstance instance)
        {
            sensationNameLabel.Content = instance.name;
            sensationLoopLabel.Content = instance.loop ? "Yes" : "No";
            sensationPriorityLabel.Content = instance.sensation.Priority.ToString();
            sensationDurationLabel.Content = instance.sensation.Duration.ToString("0.00s");
            sensationBlockLowerPrioLabel.Content = instance.blockLowerPrio ? "Yes" : "No";
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

            audioMonitorForm.WindowState = FormWindowState.Normal; // Show if minimized
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

        private void AudioDeviceSelectButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = WaitCursor;
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

            Cursor = DefaultCursor;
        }

        private void AddAudioSettingsEntries()
        {
            if (audioSettings == null)
            {
                return;
            }

            //TODO: AudioSettingsPriorityPanel
            //audioSettingsPriorityPanel1.ClearItems();
            //audioSettingsPriorityPanel1.ImportSettings(audioSettings.SpectrumSettings, owo);
        }

        private void OwiConfigureSensationsButton_Click(object sender, RoutedEventArgs e)
        {
            using (OWIIntensityListForm form = new(owiSettings.EnabledSensations))
            {
                form.ShowDialog();
            }
        }

        private void CollidersUseVelocityCheckbox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            velocityBasedGroupBox.IsEnabled = (collidersUseVelocityCheckbox.IsChecked ?? false);
        }

        private void OwiConfigureIntensitiesButton_Click(object sender, RoutedEventArgs e)
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

        private void VelocityMonitorButton_Click(object sender, RoutedEventArgs e)
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

            speedMonitorForm.SetMaxVelocity(velocitySettings.MaxSpeed);
            speedMonitorForm.SetMinVelocity(velocitySettings.MinSpeed);
            speedMonitorForm.SetOSCStatus(receiver?.IsRunning ?? false);

            speedMonitorForm.WindowState = FormWindowState.Normal; // Show if minimized
            speedMonitorForm.Activate();
        }

        private void InertiaMonitorButton_Click(object sender, RoutedEventArgs e)
        {
            if (inertiaEffect == null)
            {
                Log.Error("Inertia effect is not initialized!");
                return;
            }

            if (speedHistoryForm == null)
            {
                speedHistoryForm = new SpeedHistoryForm(inertiaEffect);
                speedHistoryForm.FormClosed += SpeedHistoryForm_Closed;
                speedHistoryForm.Show();
            }

            //speedHistoryForm.SetMaxVelocity(100f); // Idk what to use as max value, so I'm using the velocity max for now
            speedHistoryForm.SetMinDelta(inertiaSettings.MinDelta);
            speedHistoryForm.SetOSCStatus(receiver?.IsRunning ?? false);

            speedHistoryForm.WindowState = FormWindowState.Normal; // Show if minimized
            speedHistoryForm.Activate();

            if (!velocitySettings.Enabled)
            {
                speedMonitorForm?.Close();
            }
        }

        private void SpeedHistoryForm_Closed(object? sender, FormClosedEventArgs e)
        {
            if (speedHistoryForm == null)
            {
                return;
            }
            speedHistoryForm.FormClosed -= SpeedHistoryForm_Closed;
            speedHistoryForm = null;
        }

        private void UseOSCQueryCheckbox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            oscPortInput.IsEnabled = !(useOSCQueryCheckbox.IsChecked ?? false);
            connectionSettings.UseOSCQuery = useOSCQueryCheckbox.IsChecked ?? false;
            connectionSettings.SaveToFile();
        }


        // Compatibility helpers until I figure things out
        public void InvokeIfRequired(Action del, params object?[]? args)
        {
            del.DynamicInvoke(args);
        }
    }
}