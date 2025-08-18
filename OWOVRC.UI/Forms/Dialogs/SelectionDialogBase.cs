namespace OWOVRC.UI.Forms.Dialogs
{
    public partial class SelectionDialogBase: Form
    {
        public SelectionDialogBase(string description = "Please select an option:", string title = "Selection")
        {
            InitializeComponent();

            descriptionLabel.Text = description;
            Text = title;
        }
    }
}
