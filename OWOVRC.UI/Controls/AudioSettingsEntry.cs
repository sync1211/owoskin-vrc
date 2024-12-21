using OWOGame;
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
                return (float) minInput.Value;
            }
            set
            {
                minInput.Value = (decimal) value;
            }
        }
        public float Max
        {
            get
            {
                return (float) maxInput.Value;
            }
            set
            {
                maxInput.Value = (decimal) value;
            }
        }

        public EventHandler<MouseEventArgs>? OnDragStart;
        public EventHandler<MouseEventArgs>? OnDragStop;
        public EventHandler? OnPriorityChanged;

        public readonly AudioEffectSpectrumSettings? audioEffectSpectrumSettings;

        public readonly Dictionary<int, int> MuscleIntensities = [];

        public AudioSettingsEntry()
        {
            InitializeComponent();
            RegisterEvents();
        }

        public AudioSettingsEntry(string name, int priority = 0, AudioEffectSpectrumSettings? settings = null)
        {
            InitializeComponent();
            Name = name;

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

            using (MuscleIntensityForm intensityForm = new(MuscleIntensities, testSensation, $"Muscles affected by {Name}"))
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

        public static AudioSettingsEntry FromSpectrumSettings(AudioEffectSpectrumSettings settings)
        {
            AudioSettingsEntry entry = new(settings.Name, settings.Priority, settings)
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
    }
}
