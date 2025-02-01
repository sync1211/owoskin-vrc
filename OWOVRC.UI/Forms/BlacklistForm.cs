using System.ComponentModel;
using System.Windows.Forms;

namespace OWOVRC.UI.Forms
{
    public partial class BlacklistForm : Form
    {
        public readonly Dictionary<string, bool> Items;
        private readonly string[] keyLookupStorage; // Allows matching the index in checkedListBox to the dictionary keys

        public BlacklistForm(string title, Dictionary<string, bool> items)
        {
            InitializeComponent();
            Text = title;
            Items = items;

            keyLookupStorage = new string[Items.Count];
            Items.Keys.CopyTo(keyLookupStorage, 0);

            PopulateList();
        }

        private void PopulateList()
        {
            checkedListBox1.BeginUpdate();

            checkedListBox1.Items.Clear();
            foreach (KeyValuePair<string, bool> item in Items)
            {
                checkedListBox1.Items.Add(item.Key, item.Value);
            }

            checkedListBox1.EndUpdate();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;
            string name = keyLookupStorage[index];

            Items[name] = checkedListBox1.GetItemChecked(index);
        }
    }
}
