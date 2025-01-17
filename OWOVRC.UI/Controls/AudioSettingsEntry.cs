using OWOGame;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Forms;
using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class AudioSettingsEntry : UserControl
    {
        [Localizable(true)]
        [Description("If the elements should support reordering drag&drop"), Category("Data")]
        public bool AllowDrag
        {
            get
            {
                return DragHandle1.Enabled;
            }
            set
            {
                DragHandle1.SetEnabled(value);
            }
        }

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
                minInput_SkipValueChanged = true;
                minInput.Value = (decimal)value;
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
                maxInput_SkipValueChanged = true;
                maxInput.Value = (decimal)value;
            }
        }

        public EventHandler<MouseEventArgs>? OnDragStart;
        public EventHandler<MouseEventArgs>? OnDragStop;
        public EventHandler? OnPriorityChanged;

        private readonly OWOHelper? owoHelper;

        public readonly AudioEffectSpectrumSettings? audioEffectSpectrumSettings;

        public readonly Dictionary<int, int> MuscleIntensities = [];

        // Skip next change event (we only want to fire ValueChanged on user interactions)
        //NOTE: Set these to true when setting the value programmatically
        private bool maxInput_SkipValueChanged;
        private bool minInput_SkipValueChanged;

        public AudioSettingsEntry(OWOHelper? owoHelper = null)
        {
            this.owoHelper = owoHelper;
            InitializeComponent();
            RegisterEvents();
        }

        private void Ctl_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e is not HandledMouseEventArgs handledEvent)
            {
                return;
            }

            handledEvent.Handled = true;
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

            minInput.MouseWheel += Ctl_MouseWheel;
            maxInput.MouseWheel += Ctl_MouseWheel;
            priorityInput.MouseWheel += Ctl_MouseWheel;
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
            // Ignore programmatic changes
            if (minInput_SkipValueChanged)
            {
                minInput_SkipValueChanged = false;
                return;
            }

            if (Min < Max)
            {
                return;
            }

            Min = 1;
            MessageBox.Show("Min cannot be larger than Max!", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MaxInput_ValueChanged(object? sender, EventArgs e)
        {
            // Ignore programmatic changes
            if (maxInput_SkipValueChanged)
            {
                maxInput_SkipValueChanged = false;
                return;
            }

            if (Min < Max)
            {
                return;
            }

            Max = Min + 1;
            MessageBox.Show("Max cannot be small than Min!", "Invalid input!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
