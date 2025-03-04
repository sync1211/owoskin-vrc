using OWOVRC.Classes.Effects.OSCPresets;
using Serilog;
using System.ComponentModel;

namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class PresetsImportDialog : Form
    {
        public readonly BindingList<OSCSensationPreset> Presets;
        private readonly IEnumerable<OSCSensationPreset> existing;
        private readonly string presetName;
        private readonly StringComparison stringComparison;

        public PresetsImportDialog(OSCSensationPreset[] presets, IEnumerable<OSCSensationPreset> existingPresets, string presetName = "Import", StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            InitializeComponent();
            Presets = [.. presets];
            dataGridView1.DataSource = presets;
            this.presetName = presetName;
            existing = existingPresets;
            this.stringComparison = stringComparison;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }

        private void AutoRenameButton_Click(object sender, EventArgs e)
        {
            string prefix = presetName;
            using (TextInputDialog dialog = new("Import presets", "Please enter a prefix for the imported presets:", prefix))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                prefix = dialog.Value;
            }

            foreach (OSCSensationPreset preset in Presets)
            {
                preset.Path = $"{prefix}/{preset.Path}";
            }

            dataGridView1.Refresh();
            RefreshCollisionState();
        }

        private void RefreshCollisionState()
        {
            importButton.Enabled = !CheckForCollisions();
            collisionIndicatorLabel.Visible = !importButton.Enabled;
            ValidateAllCells();
        }

        private bool CheckForCollisions()
        {
            foreach (OSCSensationPreset preset in Presets)
            {
                if (existing.Any(p => p.Path.Equals(preset.Path, stringComparison)))
                {
                    return true;
                }
            }

            return false;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RefreshCollisionState();
        }

        private void ValidateAllCells()
        {
            for (int rowIndex = 0; rowIndex < dataGridView1.RowCount; rowIndex++)
            {
                ValidateCell(rowIndex);
            }
        }

        private void ValidateCell(int rowIndex)
        {
            DataGridViewColumn? column = dataGridView1.Columns[nameof(OSCSensationPreset.Path)];
            if (column == null)
            {
                Log.Warning("[Validation] Column not found: {Name}", nameof(OSCSensationPreset.Path));
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            DataGridViewCell cell = row.Cells[column.Index];

            if (cell == null)
            {
                Log.Warning("[Validation] Cell not found: {Name}", nameof(OSCSensationPreset.Path));
                return;
            }

            string? data = cell.Value?.ToString();

            // Empty name
            if (data == null)
            {
                cell.ErrorText = "Name must not be empty";
                return;
            }

            // Name already taken by existing preset
            bool hasCollision = existing.Any((p) => p.Path.Equals(data, stringComparison));
            if (hasCollision)
            {
                cell.ErrorText = "A preset with this name already exists!";
                return;
            }

            // Name already taken by imported preset
            for (int i = 0; i < Presets.Count; i++)
            {
                if (i == rowIndex)
                {
                    continue;
                }

                if (Presets[i].Path.Equals(data, stringComparison))
                {
                    cell.ErrorText = "A preset with this name already exists within this import!";
                    return;
                }
            }

            cell.ErrorText = String.Empty;
        }

        private void PresetsImportDialog_Shown(object sender, EventArgs e)
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
