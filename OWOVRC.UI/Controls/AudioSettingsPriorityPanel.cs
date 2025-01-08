using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class AudioSettingsPriorityPanel : UserControl
    {
        [Localizable(true)]
        [Description("The spacing between entries"), Category("Data")]
        public int itemSpacing = 2;
        [Localizable(true)]
        [Description("If the elements should support reordering drag&drop"), Category("Data")]
        public bool DragReordering;

        public readonly BindingList<AudioSettingsEntry> Items = [];

        private AudioSettingsEntry? pickedUpEntry;
        private int pickedUpEntryIndex;
        private int mouseStartY;

        //TODO: Scrolling support
        public AudioSettingsPriorityPanel()
        {
            InitializeComponent();
            Items.ListChanged += HandleListChanged;
            AddItems();
        }

        private void HandleListChanged(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(AddItems);
            }
            else
            {
                AddItems();
            }
        }

        private void HandlePriorityChanged(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(UpdateItemOrder);
            }
            else
            {
                UpdateItemOrder();
            }
        }

        public void AddItems()
        {
            SuspendLayout();
            Controls.Clear();

            foreach (AudioSettingsEntry item in Items.OrderByDescending((entry) => entry.Priority))
            {
                item.OnDragStart -= HandleItemDragStart;
                item.OnDragStop -= HandleItemDragStop;
                item.OnPriorityChanged -= HandlePriorityChanged;

                item.OnDragStart += HandleItemDragStart;
                item.OnDragStop += HandleItemDragStop;
                item.OnPriorityChanged += HandlePriorityChanged;

                Controls.Add(item);
            }

            AddMouseMoveHandler(this);
            ResumeLayout(true);

            UpdateItemOrder();
        }

        public void UpdateItemOrder()
        {
            int lastY = itemSpacing;

            foreach (AudioSettingsEntry item in Items.OrderByDescending((entry) => entry.Priority))
            {
                item.AllowDrag = DragReordering;

                item.Top = lastY;
                item.Left = itemSpacing;
                item.Width = Width - (itemSpacing * 2);

                lastY += item.Height + itemSpacing;
            }

            Refresh();
        }

        public void AddMouseMoveHandler(Control control)
        {
            if (control is DragHandle)
            {
                control.MouseMove -= AudioSettingsPriorityPanel_MouseMove;
                control.MouseMove += AudioSettingsPriorityPanel_MouseMove;
            }

            foreach (Control childControl in control.Controls)
            {
                AddMouseMoveHandler(childControl);
            }
        }

        public void HandleItemDragStart(object? sender, MouseEventArgs e)
        {
            if (sender is not AudioSettingsEntry entry || !DragReordering)
            {
                return;
            }

            if (pickedUpEntry != null)
            {
                pickedUpEntry = null;
                UpdateItemOrder();
                return;
            }

            pickedUpEntry = entry;
            pickedUpEntry.BringToFront();

            mouseStartY = e.Y;
            pickedUpEntryIndex = Items.IndexOf(pickedUpEntry);
            UpdateEntrySpacingForDragged();
        }

        public void HandleItemDragStop(object? sender, MouseEventArgs e)
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            if (pickedUpEntryIndex < Items.Count)
            {
                AudioSettingsEntry entry = Items[pickedUpEntryIndex];
                pickedUpEntry.Priority = entry.Priority;
                entry.Priority--;

                // Update the priority of all items below the replaced entry
                int maxPriority = entry.Priority;
                for (int i = 0; i < Items.Count; i++)
                {
                    AudioSettingsEntry currentEntry = Items[i];
                    if (currentEntry == pickedUpEntry || currentEntry == entry)
                    {
                        continue;
                    }

                    if (currentEntry.Priority < pickedUpEntry.Priority && currentEntry.Priority >= maxPriority)
                    {
                        maxPriority--;
                        currentEntry.Priority = maxPriority;
                    }
                }
            }
            else
            {
                int minPriority = Items.Min((entry) => entry.Priority);

                if (minPriority == 0)
                {
                    AudioSettingsEntry[] priorityArr = [.. Items.OrderBy((entry) => entry.Priority)];

                    minPriority++;
                    foreach (AudioSettingsEntry item in priorityArr)
                    {
                        if (item.Priority > minPriority)
                        {
                            continue;
                        }

                        item.Priority = minPriority;
                        minPriority++;
                    }

                    pickedUpEntry.Priority = 0;
                }
                else
                {
                    pickedUpEntry.Priority = minPriority - 1;
                }
            }

            UpdatePriorities();
            pickedUpEntry = null;

            SortItems();
        }

        private void SortItems()
        {
            Items.ListChanged -= HandleListChanged;

            AudioSettingsEntry[] newItems = Items.OrderByDescending((entry) => entry.Priority).ToArray();

            Items.Clear();
            foreach (AudioSettingsEntry item in newItems)
            {
                item.AllowDrag = DragReordering;
                Items.Add(item);
            }

            Items.ListChanged += HandleListChanged;

            HandleListChanged(this, EventArgs.Empty);
        }

        private void AudioSettingsPriorityPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            // Suspend MouseMove events to prevent jitter
            pickedUpEntry.DragHandle1.MouseMove -= AudioSettingsPriorityPanel_MouseMove;

            int offset = mouseStartY - e.Y;

            pickedUpEntry.Top = Math.Max(itemSpacing, pickedUpEntry.Top - offset);

            pickedUpEntry.Refresh();

            UpdatePickedUpEntryIndex();
            UpdateEntrySpacingForDragged();

            // Reactivate MouseMove events
            pickedUpEntry.DragHandle1.MouseMove += AudioSettingsPriorityPanel_MouseMove;
        }

        private void UpdatePickedUpEntryIndex()
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            int entryHeight = pickedUpEntry.Height + itemSpacing;
            int pickedUpEntryPos = pickedUpEntry.Top;

            pickedUpEntryIndex = Math.Max(0, (pickedUpEntryPos + (entryHeight / 2)) / entryHeight);
        }

        private void UpdateEntrySpacingForDragged()
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            AudioSettingsEntry[] priorityArr = [.. Items.OrderByDescending((entry) => entry.Priority)];

            int lastY = itemSpacing;
            for (int i = 0; i < priorityArr.Length; i++)
            {
                AudioSettingsEntry item = priorityArr[i];
                if (item == pickedUpEntry)
                {
                    if (pickedUpEntryIndex == 0)
                    {
                        lastY += pickedUpEntry.Height + itemSpacing;
                    }
                    continue;
                }

                // Insert placeholder for picked up entry
                if (i == pickedUpEntryIndex)
                {
                    lastY += pickedUpEntry.Height + itemSpacing;
                }

                item.Top = lastY;
                item.Left = itemSpacing;

                item.Refresh();

                lastY += item.Height + itemSpacing;
            }
        }

        private void UpdatePriorities()
        {
            AudioSettingsEntry[] priorityArr = [.. Items.OrderBy((entry) => entry.Priority)];

            int minPriority = priorityArr[0].Priority;

            foreach (AudioSettingsEntry item in priorityArr)
            {
                item.Priority = minPriority;
                minPriority++;
            }
        }
    }
}
