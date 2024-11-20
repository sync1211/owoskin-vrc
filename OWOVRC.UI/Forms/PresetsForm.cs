using OWOVRC.Classes.Settings;
using System;
using System.Linq;

namespace OWOVRC.UI.Forms
{
    public partial class PresetsForm : Form
    {
        private OSCPresetsSettings? settings;
        public PresetsForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(OSCPresetsSettings settings)
        {
            this.settings = settings;
            this.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void importSensationButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement me!
            MessageBox.Show("Not yet implemented!");
        }
    }
}
