namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class TextInputDialog : Form
    {
        public string Value => textInput.Text.Trim();

        public TextInputDialog(string title, string description, string? defaultText = null)
        {
            InitializeComponent();

            Text = title;
            descriptionLabel.Text = description;
            textInput.Text = defaultText ?? String.Empty;
        }

        private void TextInputDialog_Shown(object sender, EventArgs e)
        {
            textInput.Focus();
            textInput.Select(textInput.Text.Length, textInput.Text.Length);
        }

        private void NewNameInput_TextChanged(object sender, EventArgs e)
        {
            okButton.Enabled = !String.IsNullOrEmpty(Value);
        }
    }
}
