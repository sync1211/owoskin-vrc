namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class NameCollisionDialog : Form
    {
        public string Value => newNameInput.Text.Trim();
        private bool IsCollision => string.IsNullOrEmpty(Value) || Value.Equals(origName, stringComparison);
        private readonly string origName;
        private readonly StringComparison stringComparison;

        public NameCollisionDialog(string name, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            InitializeComponent();
            descriptionLabel.Text = $"Please enter a new name for '{name}':";
            newNameInput.Text = name;
            origName = name;
            this.stringComparison = stringComparison;
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NameCollisionDialog_Shown(object sender, EventArgs e)
        {
            newNameInput.Focus();
            newNameInput.Select(newNameInput.Text.Length, newNameInput.Text.Length);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (IsCollision)
            {
                MessageBox.Show(
                    $"The name '{origName}' is already in use. Please enter a new name!",
                    "Name already in use",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            Close();
        }

        private void NewNameInput_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !IsCollision;
        }
    }
}
