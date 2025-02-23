namespace OWOVRC.UI.Forms.Dialogs
{
    public class SelectionDialog<T> : SelectionDialogBase
    {
        /*
            /!\ The layout builder is broken for this class! /!\
                Use SelectionDialogBase for layout instead.
        */

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
