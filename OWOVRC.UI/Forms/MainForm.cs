using Newtonsoft.Json;
using OWOGame;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

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
        private CollisionEffectSettings collisionSettings = new();

        // OWO
        private readonly OWOHelper owo = new();
        private OSCReceiver receiver = new();

        // Effects
        private OSCEffectBase[] effects = [];

        public MainForm()
        {
            InitializeComponent();
            logLevelSwitch = Logging.SetUpLogger(logBox);
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

            T? settings = JsonConvert.DeserializeObject<T>(settingsData);
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

            // Collision effect settings
            CollisionEffectSettings? collisionSettings = GetSettingsData<CollisionEffectSettings>(settingsDir, "collision.json", "collision effect");
            if (collisionSettings != null)
            {
                this.collisionSettings = collisionSettings;
            }

            // Velocity effect settings
            VelocityEffectSettings? velocitySettings = GetSettingsData<VelocityEffectSettings>(settingsDir, "velocity.json", "velocity effect");
            if (velocitySettings != null)
            {
                this.velocitySettings = velocitySettings;
            }
        }

        private void SaveSettings<T>(T settings, string fileName, string displayName)
        {
            string settingsData = JsonConvert.SerializeObject(settings);
            string settingsFilePath = Path.Combine(settingsDir, fileName);

            if (!File.Exists(settingsFilePath))
            {
                FileStream newFile = File.Create(settingsFilePath);
                newFile.Close();
            }

            File.WriteAllText(settingsFilePath, settingsData);

            Log.Information("{0} settings saved", displayName);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartOWO();
            startButton.Visible = false;
            stopButton.Visible = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopOWO();
            startButton.Visible = true;
            stopButton.Visible = false;
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            // OWO connection
            if (owo.IsConnected)
            {
                connectionStatusLabel.Text = "Connected";
                connectionStatusLabel.ForeColor = Color.Green;
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
        }

        private void StartOWO()
        {
            // Start OSC receiver
            receiver.Dispose(); // The receiver does not have a stop method, so we're re-creating it on launch
            receiver = new(connectionSettings.OSCPort);
            receiver.Start();

            // Register effects
            foreach (OSCEffectBase effect in effects)
            {
                receiver.OnMessageReceived += effect.OnOSCMessageReceived;
            }

            // Start OWO connection
            owo.Address = connectionSettings.OWOAddress;
            Task.Run(owo.Connect);
            Log.Information("Started OWOVRC");
        }

        private void StopOWO()
        {
            // Unregister effects
            foreach (OSCEffectBase effect in effects)
            {
                receiver.OnMessageReceived -= effect.OnOSCMessageReceived;
            }

            receiver.Dispose();

            owo.Disconnect();
            Log.Information("Stopped OWOVRC");
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (logLevelComboBox.SelectedItem is LogEventLevel level)
            {
                logLevelSwitch.MinimumLevel = level;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            logLevelComboBox.DataSource = Logging.Levels;
            logLevelSwitch.MinimumLevel = LogEventLevel.Information;
            logLevelComboBox.SelectedItem = logLevelSwitch.MinimumLevel;

            // Set up effects
            effects = [
                new Collision(owo, collisionSettings),
                new Velocity(owo, velocitySettings)
            ];
        }

        private void UpdateConnectionSettings()
        {
            owoIPInput.Text = connectionSettings.OWOAddress;
            oscPortInput.Text = connectionSettings.OSCPort.ToString();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            owoIPInput.ValidatingType = typeof(System.Net.IPAddress);
            UpdateConnectionSettings();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopOWO();
            owo.Dispose();
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

            SaveSettings<ConnectionSettings>(connectionSettings, "connection.json", "connection settings");
        }

        private void OscPortInput_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(oscPortInput.Text, out int port))
            {
                connectionSettings.OSCPort = port;
            }
            else
            {
                oscPortInput.Text = connectionSettings.OSCPort.ToString();
            }

            SaveSettings<ConnectionSettings>(connectionSettings, "connection.json", "connection");
        }

        private void ApplyCollisionSettingsButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement me!
            SaveSettings<CollisionEffectSettings>(collisionSettings, "collision.json", "collision effect");
        }

        private void ApplyVelocitySettingsButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement me!
            SaveSettings<VelocityEffectSettings>(velocitySettings, "velocity.json", "velocity effect");
        }
    }
}
