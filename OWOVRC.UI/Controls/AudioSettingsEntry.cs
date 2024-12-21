using OWOVRC.UI.Forms;

namespace OWOVRC.UI.Controls
{
    public partial class AudioSettingsEntry : UserControl
    {
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
        public int Min
        {
            get
            {
                return (int)minInput.Value;
            }
        }
        public int Max
        {
            get
            {
                return (int)maxInput.Value;
            }
        }

        public EventHandler<MouseEventArgs>? OnDragStart;
        public EventHandler<MouseEventArgs>? OnDragStop;
        public EventHandler? OnPriorityChanged;

        public readonly Dictionary<int, int> MuscleIntensities = [];

        public AudioSettingsEntry()
        {
            InitializeComponent();
            RegisterEvents();
        }

        public AudioSettingsEntry(string name, int priority = 0)
        {
            InitializeComponent();
            Name = name;

            priorityInput.Value = priority;
            nameLabel.Text = Name;

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
            using (MuscleIntensityForm intensityForm = new(MuscleIntensities, null, $"Muscles affected by {Name}"))
            {
                intensityForm.ShowDialog();
            }
        }
    }
}
