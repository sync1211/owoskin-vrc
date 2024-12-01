using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using Serilog;

namespace OWOVRC.UI.Forms
{
    public partial class PresetsForm : Form
    {
        private OSCPresetsSettings? settings;
        private List<OSCSensationPreset> presets = [];
        public EventHandler? OnSave;

        public PresetsForm()
        {
            InitializeComponent();
        }

        private void ForceDataRefresh()
        {
            //NOTE: Idk why, but the gridView isn't removing the entry unless I do this terribleness
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = presets;
        }

        public void ShowDialog(OSCPresetsSettings settings)
        {
            this.settings = settings;
            presets = settings.Presets.Values.ToList();

            dataGridView1.DataSource = presets;

            this.ShowDialog();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            presets.Clear();
            this.Close();
        }

        private void ImportSensationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "OWO Sensation Files (*.owo)|*.owo",
                Title = "Select a .owo file to import",
                Multiselect = true
            };

            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            foreach (string filePath in openFileDialog.FileNames)
            {
                ImportOWOSensationFromFile(filePath);
            }

            ForceDataRefresh();
        }

        private void ImportOWOSensationFromFile(string path)
        {
            Log.Debug("Importing sensation from file: {path}", path);

            if (!path.EndsWith(".owo"))
            {
                Log.Warning("Not importing file {path}: unsupported extension", path);
                MessageBox.Show($"Unsupported file type:{Environment.NewLine}{path}{Environment.NewLine}{Environment.NewLine}Only valid .owo files can be imported as presets!", "Unsupported file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sensationString = File.ReadAllText(path);

            string fileName = Path.GetFileNameWithoutExtension(path);

            ImportOWOSensation(fileName, sensationString);
        }

        private string ResolveNameCollisions(string name)
        {
            if (presets.Any((preset) => preset.Name.Equals(name)))
            {
                int i = 0;
                while (presets.Any((preset) => preset.Name.Equals($"{name} ({i})")))
                {
                    i++;
                }

                return $"{name} ({i})";
            }

            return name;
        }

        private void ImportOWOSensation(string name, string sensationString)
        {
            // Check if sensation is baked
            if (!sensationString.Contains('~'))
            {
                Log.Warning("Not importing sensation {name}: only baked sensations are supported!", name);
                MessageBox.Show(
                    $"The sensation {name} is baked and cannot be imported as a preset!",
                    "Non-Baked sensation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                Log.Verbose("Importing sensation {name}: {value}", name, sensationString);

                // Fix potential name collisions
                name = ResolveNameCollisions(name);

                OSCSensationPreset preset = new(true, name, 1, 100, sensationString);
                presets.Add(preset);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Error parsing sensation file!", "Import failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (settings == null)
            {
                return;
            }

            // Check for collisions
            List<string> names = [];
            foreach (OSCSensationPreset preset in presets)
            {
                if (names.Contains(preset.Name))
                {
                    MessageBox.Show(
                        $"The preset {preset.Name} is listed more than once!{Environment.NewLine}Preset names must be unique!",
                        "Duplicate preset names found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                names.Add(preset.Name);
            }

            settings.Presets.Clear();
            foreach (OSCSensationPreset preset in presets)
            {
                settings.Presets.Add(preset.Name, preset);
            }

            this.Close();
            presets.Clear();
            OnSave?.Invoke(this, EventArgs.Empty);
        }

        private void DataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            dropIndicatorLabel.Visible = false;

            if (e.Data == null)
            {
                return;
            }

            Log.Debug("Received drop event");
            var data = e.Data.GetData(DataFormats.FileDrop, false);

            if (data is not string[] files)
            {
                Log.Debug("No files dropped");
                return;
            }

            // Import
            foreach (string file in files)
            {
                Log.Debug("Importing sensation via Drag&Drop: {file}", file);
                ImportOWOSensationFromFile(file);
            }

            ForceDataRefresh();
        }

        private void DataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            dropIndicatorLabel.Visible = true;
            if (e.Data == null)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void DataGridView1_DragLeave(object sender, EventArgs e)
        {
            dropIndicatorLabel.Visible = false;
        }

        private void RemovePresetButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                return;
            }

            // Get preset objects as the index will change when removing
            List<OSCSensationPreset> presetsToDelete = [];
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                presetsToDelete.Add(presets[cell.RowIndex]);
            }

            // Delete selected presets
            foreach (OSCSensationPreset preset in presetsToDelete)
            {
                presets.Remove(preset);
            }

            ForceDataRefresh();
        }

        private void PresetsHelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WikiHelper.OpenURL(WikiHelper.OSC_PRESETS_WIKI_URL);
        }
    }
}
