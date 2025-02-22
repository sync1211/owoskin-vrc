namespace OWOVRC.UI.Forms.Dialogs
{
    public class SelectionDialog<T> : SelectionDialogBase
    {
        private readonly T[] items;
        public T Value => items[comboBox1.SelectedIndex];

        public SelectionDialog(T[] items, string description = "Please select an option:", string title = "Selection", int selectedIndex = 0): base(description, title)
        {
            this.items = items;

            comboBox1.DataSource = items;
            comboBox1.SelectedIndex = selectedIndex;
        }
    }
}
