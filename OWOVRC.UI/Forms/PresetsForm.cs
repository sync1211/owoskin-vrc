﻿using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes;
using OWOVRC.UI.Forms.Dialogs;
using Serilog;
using System.ComponentModel;

namespace OWOVRC.UI.Forms
{
    public partial class PresetsForm : Form
    {
        private const string PATH_BASE = "OWO/SensationsTrigger";
        private readonly OSCPresetsSettings settings;
        private readonly BindingList<OSCSensationPreset> presets;
        private bool showCollisionDialog = true;
        private const StringComparison stringComparison = StringComparison.CurrentCulture;

        public PresetsForm(OSCPresetsSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            presets = new([.. settings.Presets.Values]);

            dataGridView1.DataSource = presets;
            presets.ListChanged += OnListChange;
        }

        private void OnListChange(object? sender, EventArgs args)
        {
            RefreshCollisionState();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ImportSensationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "OWO Sensation Files (*.owo)|*.owo|OWOVRC Preset settings (oscPresets.json)|oscPresets.json",
                Title = "Select a file to import",
                Multiselect = true
            };

            DialogResult result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            ImportSensationFileList(openFileDialog.FileNames);
        }

        private void ImportSensationFileList(string[] fileNames)
        {
            int sensationsCount = presets.Count;
            foreach (string filePath in fileNames)
            {
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
                preset.Path = preset.Path;
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

        private string? ResolvePathCollisions(string path)
        {
            if (!presets.Any((preset) => preset.Path.Equals(path)))
            {
                return path;
            }

            if (showCollisionDialog)
            {
                using (NameCollisionDialog dialog = new(path, stringComparison))
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

            // Rename by addin a "(<i>)" suffix
            int i = 1;
            while (presets.Any((preset) => preset.Path.Equals($"{path} ({i})")))
            {
                i++;
            }

            return $"{path} ({i})";
        }

        private bool ImportOWOSensation(string name, string sensationString)
        {
            name = $"{PATH_BASE}/{name}";
            try
            {
                Log.Verbose("Importing sensation {name}: {value}", name, sensationString);

                // Fix potential name collisions
                string? newName = ResolvePathCollisions(name);
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
            List<string> names = [];
            foreach (OSCSensationPreset preset in presets)
            {
                if (names.Contains(preset.Path))
                {
                    MessageBox.Show(
                        $"The preset {preset.Path} is listed more than once!{Environment.NewLine}Preset names must be unique!",
                        "Duplicate preset names found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                names.Add(preset.Path);
            }

            settings.Presets.Clear();
            foreach (OSCSensationPreset preset in presets)
            {
                settings.Presets.Add(preset.Path, preset);
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
        }

        private void RefreshCollisionState()
        {
            saveButton.Enabled = !CheckForCollisions();
        }

        private bool CheckForCollisions()
        {
            DataGridViewColumn? column = dataGridView1.Columns[nameof(OSCSensationPreset.Path)];
            if (column == null)
            {
                Log.Warning("[Validation] Column not found: {Name}", nameof(OSCSensationPreset.Path));
                return false;
            }

            bool result = false;
            for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                DataGridViewCell cell = row.Cells[column.Index];

                if (cell == null)
                {
                    Log.Warning("[Validation] Cell not found: {Name}", nameof(OSCSensationPreset.Path));
                    return false;
                }

                cell.ErrorText = String.Empty;

                string data = cell.Value?.ToString() ?? "";
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

                    if (presets[i].Path.Equals(data, stringComparison))
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
    }
}
