using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class SelectableMuscle: PictureBox
    {
        [Localizable(true)]
        [Description("The Muscle id of this element"), Category("Data")]
        public MusclesEnum Muscle { get; set; }

        public int MuscleID => (int)Muscle;
        public bool IsActive
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                UpdateImage();
            }
        }
        // Aliases for accessibility
        public bool Checked => IsActive;
        public bool Value => IsActive;

        [Localizable(true)]
        [Description("The image shown when the element is inactive"), Category("Appearance")]
        public Image? InactiveImage
        {
            get
            {
                return inactiveImage;
            }
            set
            {
                if (!active)
                {
                    BackgroundImage = value;
                }
                inactiveImage = value;
            }
        }
        [Localizable(true)]
        [Description("The image shown when the element is active"), Category("Appearance")]
        public Image? ActiveImage { get; set; }

        private bool active;
        private Image? inactiveImage;

        public SelectableMuscle()
        {
            MouseClick += HandleClicked;
            BackgroundImage = InactiveImage;
            BackgroundImageLayout = ImageLayout.Zoom;
            AccessibleRole = AccessibleRole.CheckButton;
        }

        private void HandleClicked(object? sender, EventArgs e)
        {
            IsActive = true;
        }

        public void UpdateImage()
        {
            BackgroundImage = active ? ActiveImage : InactiveImage;
            AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
        }
    }
}
