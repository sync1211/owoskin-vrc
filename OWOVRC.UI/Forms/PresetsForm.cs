using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Settings;
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
            dataGridView1.CellContentClick += GridViewCellClick;
        }

        public DataGridViewButtonColumn AddButtonColumn(string name, string text)
        {
            DataGridViewButtonColumn buttonColumn = new()
            {
                Text = text,
                Name = name
            };
            if (dataGridView1.Columns[name] == null)
            {
                dataGridView1.Columns.Add(buttonColumn);
            }

            return buttonColumn;
        }

        public void AddControlColumns()
        {
            // Remove button
            AddButtonColumn("Remove", "REMOVE");
        }

        public void GridViewCellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || presets.Count == 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name != "Remove")
            {
                return;
            }

            // Remove entry
            presets.RemoveAt(e.RowIndex);

            // Update table
            ForceDataRefresh();
        }

        private void ForceDataRefresh()
        {
            //NOTE: Idk why, but the gridView isn't removing the entry unless I do this terribleness
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            dataGridView1.DataSource = presets;
            AddControlColumns();
        }

        public void ShowDialog(OSCPresetsSettings settings)
        {
            this.settings = settings;
            presets = settings.Presets.Values.ToList();

            dataGridView1.DataSource = presets;

            AddControlColumns();
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

        private void ImportOWOSensation(string name, string sensationString)
        {
            try
            {
                OSCSensationPreset preset = new(true, name, 1, 100, sensationString);
                presets.Add(preset);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Error parsing sensation file!", "Import failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ForceDataRefresh();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (settings == null)
            {
                return;
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
    }
}
