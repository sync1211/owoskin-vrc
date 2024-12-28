using OWOGame;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Forms;

namespace OWOVRC.UI.Controls
{
    public partial class AudioSettingsEntry : UserControl
    {
        public bool IsEnabled
        {
            get
            {
                return enabledCheckbox.Checked;
            }
            set
            {
                enabledCheckbox.Checked = value;
            }
        }
        public int Priority
        {
            get
            {
                return (int)priorityInput.Value;
            }
            set
            {
                priorityInput.Value = value;
            }
        }
        public float Min
        {
            get
            {
                return (float)minInput.Value;
            }
            set
            {
                minInput.ValueChanged -= MinInput_ValueChanged;
                minInput.Value = (decimal)value;
                minInput.ValueChanged += MinInput_ValueChanged;
            }
        }
        public float Max
        {
            get
            {
                return (float)maxInput.Value;
            }
            set
            {
                maxInput.ValueChanged -= MaxInput_ValueChanged;
                maxInput.Value = (decimal)value;
                maxInput.ValueChanged += MaxInput_ValueChanged;
            }
        }

        public EventHandler<MouseEventArgs>? OnDragStart;
        public EventHandler<MouseEventArgs>? OnDragStop;
        public EventHandler? OnPriorityChanged;

        private readonly OWOHelper? owoHelper;

        public readonly AudioEffectSpectrumSettings? audioEffectSpectrumSettings;

        public readonly Dictionary<int, int> MuscleIntensities = [];

        public AudioSettingsEntry(OWOHelper? owoHelper = null)
        {
            this.owoHelper = owoHelper;
            InitializeComponent();
            RegisterEvents();
        }

        public AudioSettingsEntry(string name, int priority = 0, AudioEffectSpectrumSettings? settings = null, OWOHelper? owoHelper = null)
        {
            InitializeComponent();
            Name = name;

            this.owoHelper = owoHelper;

            // Force newline on zero-width space (U+200B)
            nameLabel.Text = Name.Replace("​", Environment.NewLine);

            priorityInput.Value = priority;

            audioEffectSpectrumSettings = settings;

            RegisterEvents();
        }

        private void RegisterEvents()
        {
            DragHandle1.MouseDown += DragHandle1_MouseDown;
            DragHandle1.MouseUp += DragHandle1_MouseUp;

            minInput.ValueChanged += MinInput_ValueChanged;
            maxInput.ValueChanged += MaxInput_ValueChanged;
        }

        private void DragHandle1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            OnDragStart?.Invoke(this, e);
        }

        private void DragHandle1_MouseUp(object? sender, MouseEventArgs e)
        {
            OnDragStop?.Invoke(this, e);
        }

        protected override void OnCreateControl()
        {
            base.CreateControl();
            MinimumSize = new(321, 57);
        }

        private void PriorityInput_Leave(object sender, EventArgs e)
        {
            OnPriorityChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ConfigureButton_Click(object sender, EventArgs e)
        {
            Sensation? testSensation = null;
            if (audioEffectSpectrumSettings != null)
            {
                testSensation = audioEffectSpectrumSettings.CreateSensation();
            }

            using (MuscleIntensityForm intensityForm = new(MuscleIntensities, testSensation, $"Muscles affected by {Name}", owoHelper))
            {
                intensityForm.ShowDialog();
            }
        }

        public void ApplyToSpectrumSettings()
        {
            if (audioEffectSpectrumSettings == null)
            {
                throw new ArgumentNullException("No spectrum settings to apply to.");
            }

            audioEffectSpectrumSettings.Enabled = IsEnabled;
            audioEffectSpectrumSettings.Priority = Priority;
            audioEffectSpectrumSettings.MinDB = Min;
            audioEffectSpectrumSettings.MaxDB = Max;

            foreach (KeyValuePair<int, int> intensity in MuscleIntensities)
            {
                audioEffectSpectrumSettings.Intensities[intensity.Key] = intensity.Value;
            }
        }

        public static AudioSettingsEntry FromSpectrumSettings(AudioEffectSpectrumSettings settings, OWOHelper? owoHelper = null)
        {
            AudioSettingsEntry entry = new(settings.Name, settings.Priority, settings, owoHelper)
            {
                IsEnabled = settings.Enabled,
                Min = settings.MinDB,
                Max = settings.MaxDB
            };

            foreach (KeyValuePair<int, int> intensity in settings.Intensities)
            {
                entry.MuscleIntensities[intensity.Key] = intensity.Value;
            }

            return entry;
        }

        private void MinInput_ValueChanged(object? sender, EventArgs e)
        {
            if (Min >= Max)
            {
                Min = 1;
            }

            MessageBox.Show("Min cannot be larger than Max!", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MaxInput_ValueChanged(object? sender, EventArgs e)
        {
            if (Min >= Max)
            {
                Max = Min + 1;
            }

            MessageBox.Show("Max cannot be small than Min!", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
