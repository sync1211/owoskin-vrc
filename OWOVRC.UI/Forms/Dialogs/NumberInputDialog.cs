namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class NumberInputDialog : Form
    {
        public int Value => (int)inputBox.Value;

        public NumberInputDialog(string description = "Please enter a number:", string title = "Number Input", int min = 0, int max = 100, int initial = 0)
        {
            InitializeComponent();

            inputBox.Minimum = min;
            inputBox.Maximum = max;
            inputBox.Value = initial;

            descriptionLabel.Text = description;
            Text = title;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
