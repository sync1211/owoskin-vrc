namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class SelectionDialog<T> : Form
    {
        private readonly T[] items;
        public T Value => items[comboBox1.SelectedIndex];

        public SelectionDialog(T[] items, string description = "Please select an option:", string title = "Selection", int selectedIndex = 0)
        {
            InitializeComponent();
            this.items = items;

            comboBox1.DataSource = items;

            descriptionLabel.Text = description;
            Text = title;

            comboBox1.SelectedIndex = selectedIndex;
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
