using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.UI.Forms.Dialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OWOVRC.UI.Forms
{
    public partial class AdvancedPresetsForm : Form
    {
        public List<OSCAdvancedSensationPreset> Presets
        {
            get
            {
                return [.. presets];
            }
        }
        private BindingList<OSCAdvancedSensationPreset> presets;

        public AdvancedPresetsForm(OSCAdvancedSensationPreset[] presets)
        {
            InitializeComponent();
            this.presets = [.. presets];
            listBox1.DataSource = this.presets;

            RefreshControls();
        }

        private void RefreshControls()
        {
            RefreshInputAvailability();
            RefreshInputData();
        }

        private void RefreshInputAvailability()
        {
            bool entrySelected = listBox1.SelectedItem != null;
            deleteEntryButton.Enabled = entrySelected;
            copyEntryButton.Enabled = entrySelected;
            addEntryButton.Enabled = true;

            presetGroupBox.Enabled = entrySelected;
        }

        private void RefreshInputData()
        {
            if (listBox1.SelectedItem is OSCAdvancedSensationPreset preset)
            {
                nameInput.Text = preset.Name;
                priorityInput.Value = preset.Priority;
                intensityInput.Value = preset.Intensity;
                loopCheckBox.Checked = preset.Loop;
                interruptableCheckBox.Checked = preset.Interruptable;
                minValueInput.Value = (decimal) preset.MinValue;
                maxValueInput.Value = (decimal) preset.MaxValue;
            }
            else
            {
                nameInput.Text = string.Empty;
                priorityInput.Value = 0;
                intensityInput.Value = 0;
                loopCheckBox.Checked = false;
                interruptableCheckBox.Checked = false;
                minValueInput.Value = 0;
                maxValueInput.Value = 0;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool checkForConflicts()
        {
            //TODO: Implement me!
            return false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (checkForConflicts())
            {
                MessageBox.Show("There are conflicts in the presets. Please resolve them before saving.", "Conflicts Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddEntryButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new()
            {
                Filter = "OWO Sensation Files (*.owo)|*.owo|OWOVRC Preset settings (oscPresets.json)|oscPresets.json",
                Title = "Select a file to import",
                Multiselect = true
            })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                //TODO: Importing of OWOVRC Preset settings (oscPresets.json)
                foreach (string file in openFileDialog.FileNames)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    string path = $"{OSCPresetTrigger.OSC_ADDRESS_PREFIX}{name}";

                    presets.Add(new OSCAdvancedSensationPreset(true, name, 0, 0, false, false, path, file, 0, 0));
                }
            }
        }

        private void CopyEntryButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not OSCAdvancedSensationPreset preset)
            {
                return;
            }

            OSCAdvancedSensationPreset newPreset = new(
                preset.Enabled,
                $"{preset.Name} - Copy",
                preset.Priority,
                preset.Intensity,
                preset.Loop,
                preset.Interruptable,
                preset.SensationString,
                $"{preset.Path} - Copy",
                preset.MinValue,
                preset.MaxValue
            );
            presets.Add(newPreset);
        }

        private void DeleteEntryButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            presets.RemoveAt(listBox1.SelectedIndex);
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshControls();
        }
    }
}
