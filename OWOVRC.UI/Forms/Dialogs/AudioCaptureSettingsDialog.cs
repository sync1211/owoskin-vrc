using NAudio.CoreAudioApi;

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

        public AudioCaptureSettingsDialog(MMDevice?[] devices, int selectedIndex = 0, bool captureVRCOnly = false)
        {
            InitializeComponent();

            this.devices = devices;

            audioDeviceComboBox.DataSource = devices;
            audioDeviceComboBox.SelectedIndex = selectedIndex;

            vrcOnlyCheckbox.Checked = captureVRCOnly;
            audioDeviceComboBox.Enabled = !vrcOnlyCheckbox.Checked;
        }

        private void VrcOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            audioDeviceComboBox.Enabled = !vrcOnlyCheckbox.Checked;    
        }
    }
}
