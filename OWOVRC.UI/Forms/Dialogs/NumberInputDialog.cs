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

        private void NumberInputDialog_Shown(object sender, EventArgs e)
        {
            inputBox.Focus();
            inputBox.Select(0, inputBox.Text.Length);
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (e.KeyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
        }
    }
}
