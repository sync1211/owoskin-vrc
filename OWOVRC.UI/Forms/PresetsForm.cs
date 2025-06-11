using OWOGame;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using OWOVRC.UI.Forms.Dialogs;
using Serilog;
using System.ComponentModel;

namespace OWOVRC.UI.Forms
{
    public partial class PresetsForm : Form
    {
        private readonly OSCPresetsSettings settings;
        private readonly BindingList<OSCSensationPreset> presets;
        private bool showCollisionDialog = true;
        private const StringComparison stringComparison = StringComparison.CurrentCulture;

        private const string PLAY_BUTTON_COLUMN_NAME = "PlaySensationButtonColumn";
        private readonly OWOHelper? owo;

        public PresetsForm(OSCPresetsSettings settings, OWOHelper? owo = null)
        {
            InitializeComponent();
            this.owo = owo;
            this.settings = settings;
            presets = new([.. settings.Presets.Values]);

            dataGridView1.DataSource = presets;
            presets.ListChanged += OnListChange;
            AddTestButtonColumn();
        }

        private void AddTestButtonColumn()
        {
            if (owo == null)
            {
                Log.Information("PresetsForm has been created without OWOHelper. Senastion play buttons will not be added!");
                return;
            }

            string buttonText = OWOHelper.IsConnected ? "Preview" : "unavailable";

            DataGridViewButtonColumn buttonColumn = new()
            {
                HeaderText = "Preview",
                Text = buttonText,
                UseColumnTextForButtonValue = true,
                Name = PLAY_BUTTON_COLUMN_NAME,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Resizable = DataGridViewTriState.False,
            };

            dataGridView1.Columns.Add(buttonColumn);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > dataGridView1.Columns.Count || e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }

            DataGridViewColumn column = dataGridView1.Columns[e.ColumnIndex];

            if (!column.Name.Equals(PLAY_BUTTON_COLUMN_NAME))
            {
                return;
            }

            TestSensation(presets[e.RowIndex]);
        }

        private void TestSensation(OSCSensationPreset preset)
        {
            if (owo == null)
            {
                Log.Warning("Cannot test sensation: OWOHelper is not initialized! (This should not be possible, please file an issue on GitHub!)");
                return;
            }

            if (!OWOHelper.IsConnected)
            {
                Log.Warning("Cannot test sensation: OWO is not connected!");
                MessageBox.Show(
                    "Please connect to OWO to preview presets.",
                    "OWO not connected!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Apply intensity
            Muscle[] muscles = OSCPresetTrigger.GetMusclesFromSensation(preset.SensationObject);
            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i] = muscles[i].WithIntensity(preset.Intensity);
            }

            owo.AddSensation(preset.Name, preset.SensationObject, muscles);
        }

        private void OnListChange(object? sender, EventArgs args)
        {
            //NOTE: This method may be called after trying to import sensations from a file
            //      REGARDLESS of whether there were any changes made to the list!
            RefreshCollisionState();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ImportSensationButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new()
            {
                Filter = "OWO Sensation Files (*.owo)|*.owo|OWOVRC Preset settings (oscPresets.json)|oscPresets.json",
                Title = "Select a file to import",
                Multiselect = true
            })
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                ImportSensationFileList(openFileDialog.FileNames);
            }
        }

        private void ImportSensationFileList(string[] fileNames)
        {
            // Disable list change events to prevent unnecessary updates when importing multiple sensations
            presets.RaiseListChangedEvents = false;

            try
            {
                int sensationsCount = presets.Count;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    string filePath = fileNames[i];
                    Log.Debug("Importing sensation from file: {file}", filePath);

                    bool success = false;
                    if (filePath.EndsWith(".owo"))
                    {
                        success = ImportOWOSensationFromFile(filePath);
                    }
                    else if (filePath.EndsWith(".json"))
                    {
                        success = ImportSensationsFromSettingsFile(filePath);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Unsupported file extension:{Environment.NewLine}{filePath}",
                            "Unsupported file extension",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                    }

                    // Abort on error (or user abort)
                    if (!success)
                    {
                        return;
                    }
                }

                // Nothing happened -> inform user to prevent confusion
                if (presets.Count == sensationsCount)
                {
                    MessageBox.Show(
                        "The provided files did not contain any sensations to import!",
                        "No sensations imported",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            finally
            {
                // Re-enable list change events and dispatch change event
                //NOTE: This will also trigger the change event even if no changed were made!
                presets.RaiseListChangedEvents = true;
                presets.ResetBindings();

                OnListChange(this, EventArgs.Empty);
            }
        }

        private bool ImportSensationsFromSettingsFile(string path)
        {
            OSCPresetsSettings? importedSettings = SettingsHelper.LoadSettingsFromFile<OSCPresetsSettings>(path, "imported preset", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings);
            if (importedSettings == null || importedSettings.Presets == null)
            {
                MessageBox.Show(
                    $"Failed to import prests from settings file:{Environment.NewLine}{path}",
                    "Preset import failed!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            // Show a dialog to confirm import and rename presets if necessary
            using (PresetsImportDialog dialog = new([.. importedSettings.Presets.Values], presets, Path.GetFileNameWithoutExtension(path), stringComparison))
            {
                DialogResult result = dialog.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return false;
                }
            }

            foreach (OSCSensationPreset preset in importedSettings.Presets.Values)
            {
                presets.Add(preset);
            }

            return true;
        }

        private bool ImportOWOSensationFromFile(string path)
        {
            Log.Debug("Importing sensation from file: {path}", path);

            if (!path.EndsWith(".owo"))
            {
                Log.Warning("Not importing file {path}: unsupported extension", path);
                MessageBox.Show($"Unsupported file type:{Environment.NewLine}{path}{Environment.NewLine}{Environment.NewLine}Only valid .owo files can be imported as presets!", "Unsupported file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string sensationString = File.ReadAllText(path);

            string fileName = Path.GetFileNameWithoutExtension(path);

            return ImportOWOSensation(fileName, sensationString);
        }

        private string? ResolveNameCollisions(string name)
        {
            if (!presets.Any((preset) => preset.Name.Equals(name)))
            {
                return name;
            }

            if (showCollisionDialog)
            {
                using (NameCollisionDialog dialog = new(name, stringComparison))
                {
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        return dialog.Name;
                    }
                    else if (result == DialogResult.Continue)
                    {
                        showCollisionDialog = false;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            // Rename by adding a "(<i>)" suffix
            return GetNonCollidingName(name, presets, stringComparison);
        }

        public static string GetNonCollidingName(string name, IEnumerable<OSCSensationPreset> presets, StringComparison stringComparison)
        {
            // Rename by adding a "(<i>)" suffix
            int i = 1;
            string newName;
            do
            {
                newName = $"{name} ({i})";
                i++;
            } while (presets.Any((preset) => preset.Name.Equals(newName, stringComparison)));

            return newName;
        }

        private bool ImportOWOSensation(string name, string sensationString)
        {
            try
            {
                Log.Verbose("Importing sensation {name}: {value}", name, sensationString);

                // Fix potential name collisions
                string? newName = ResolveNameCollisions(name);
                if (newName == null)
                {
                    return false; // Indicate the request to cancel
                }

                OSCSensationPreset preset = new(true, newName, 10, 100, false, false, sensationString);
                presets.Add(preset);
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Error parsing sensation file!", "Import failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (settings == null)
            {
                return;
            }

            // Check for collisions
            HashSet<string> names = [];
            for (int i = 0; i < presets.Count; i++)
            {
                OSCSensationPreset preset = presets[i];

                if (!names.Add(preset.Name))
                {
                    MessageBox.Show(
                        $"The preset {preset.Name} is listed more than once!{Environment.NewLine}Preset names must be unique!",
                        "Duplicate preset names found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
            }

            settings.Presets.Clear();
            for (int i = 0; i < presets.Count; i++)
            {
                OSCSensationPreset preset = presets[i];
                settings.Presets.Add(preset.Name, preset);
            }

            DialogResult = DialogResult.OK;
            this.Close();
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
            ImportSensationFileList(files);
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
            OSCSensationPreset[] presetsToDelete = new OSCSensationPreset[dataGridView1.SelectedCells.Count];
            for (int i = 0; i < presetsToDelete.Length; i++)
            {
                DataGridViewCell cell = dataGridView1.SelectedCells[i];
                presetsToDelete[i] = presets[cell.RowIndex];
            }

            // Delete selected presets
            for (int i = 0; i < presetsToDelete.Length; i++)
            {
                presets.Remove(presetsToDelete[i]);
            }
        }

        private void RefreshCollisionState()
        {
            saveButton.Enabled = !CheckForCollisions();
        }

        private bool CheckForCollisions()
        {
            DataGridViewColumn? column = dataGridView1.Columns[nameof(OSCSensationPreset.Name)];
            if (column == null)
            {
                Log.Warning("[Validation] Column not found: {Name}", nameof(OSCSensationPreset.Name));
                return false;
            }

            bool result = false;
            for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                DataGridViewCell cell = row.Cells[column.Index];

                if (cell == null)
                {
                    Log.Warning("[Validation] Cell not found: {Name}", nameof(OSCSensationPreset.Name));
                    return false;
                }

                cell.ErrorText = String.Empty;

                string? data = cell.Value?.ToString();
                if (String.IsNullOrEmpty(data))
                {
                    cell.ErrorText = "Name must not be empty!";
                    return true;
                }

                // Name already taken by preset
                for (int i = 0; i < presets.Count; i++)
                {
                    if (i == rowIndex)
                    {
                        continue;
                    }

                    if (presets[i].Name.Equals(data, stringComparison))
                    {
                        cell.ErrorText = "A preset with this name already exists!";
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private void PresetsHelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WikiHelper.OpenURL(WikiHelper.OSC_PRESETS_WIKI_URL);
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshCollisionState();
        }

        private void PresetsForm_Shown(object sender, EventArgs e)
        {
            RefreshCollisionState();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageBox.Show("Data error!", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(e.Exception.Message, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            dataGridView1.CancelEdit();
        }
    }
}
