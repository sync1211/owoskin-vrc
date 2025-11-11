using OWOVRC.UI.Classes.Proxies;

namespace OWOVRC.UI.Forms
{
    public partial class OWIIntensityListForm : Form
    {
        private readonly OWISensationListEntry[] Entries;

        public OWIIntensityListForm(Dictionary<string, int> items)
        {
            InitializeComponent();
            Entries = DictionaryToList(items);

            dataGridView1.DataSource = Entries;

            dataGridView1.Columns[0].HeaderText = "Sensation";
            dataGridView1.Columns[1].HeaderText = "Intensity %";
        }

        private static OWISensationListEntry[] DictionaryToList(Dictionary<string, int> items)
        {
            OWISensationListEntry[] entries = new OWISensationListEntry[items.Count];

            for (int i = 0; i < items.Count; i++)
            {
                KeyValuePair<string, int> item = items.ElementAt(i);
                entries[i] = new OWISensationListEntry(items, item.Key, item.Value);
            }

            return entries;
        }
    }
}
