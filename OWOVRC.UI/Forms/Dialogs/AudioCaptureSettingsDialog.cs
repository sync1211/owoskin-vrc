using NAudio.CoreAudioApi;
using OWOVRC.Classes.Settings;

namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class AudioCaptureSettingsDialog : Form
    {
        private readonly MMDevice?[] devices;
        public MMDevice? SelectedDevice => devices[audioDeviceComboBox.SelectedIndex];
        public bool CaptureVRCProcess
        {
            get
            {
                return vrcOnlyCheckbox.Checked;
            }
        }
        public bool UseHammingWindow
        {
            get
            {
                return hammingWindowCheckbox.Checked;
            }
        }

        public AudioCaptureSettingsDialog(MMDevice?[] devices, AudioEffectSettings settings, int selectedIndex = 0)
        {
            InitializeComponent();

            this.devices = devices;

            audioDeviceComboBox.DataSource = devices;
            audioDeviceComboBox.SelectedIndex = selectedIndex;

            hammingWindowCheckbox.Checked = settings.UseHammingWindow;

            vrcOnlyCheckbox.Checked = settings.CaptureVRCOnly;
            audioDeviceComboBox.Enabled = !vrcOnlyCheckbox.Checked;
        }

        private void VrcOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            audioDeviceComboBox.Enabled = !vrcOnlyCheckbox.Checked;    
        }
    }
}
