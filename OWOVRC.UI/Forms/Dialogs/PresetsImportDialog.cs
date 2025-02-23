using OWOVRC.Classes.Effects.OSCPresets;
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

            RefreshCollisionState();
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
                preset.Name = $"{prefix}/{preset.Name}";
            }

            dataGridView1.Refresh();
            RefreshCollisionState();
        }

        private void RefreshCollisionState()
        {
            importButton.Enabled = !CheckForCollisions();
            collisionIndicatorLabel.Visible = !importButton.Enabled;
        }

        private bool CheckForCollisions()
        {
            foreach (OSCSensationPreset preset in Presets)
            {
                if (existing.Any(p => p.Name.Equals(preset.Name, stringComparison)))
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
    }
}
