using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.UI.Classes.Extensions;
using System.ComponentModel;

namespace OWOVRC.UI.Controls
{
    public partial class AudioSettingsPriorityPanel : UserControl
    {
        [Localizable(true)]
        [Description("The spacing between entries"), Category("Data")]
        public int itemSpacing = 2;
        [Localizable(true)]
        [Description("The spacing between entries and scrollbar"), Category("Data")]
        public int scrollbarSpacing = 17;
        [Localizable(true)]
        [Description("If the elements should support reordering drag&drop"), Category("Data")]
        public bool DragReordering = true;

        private readonly List<AudioSettingsEntry> items = [];
        public AudioSettingsEntry[] Items
        {
            get {
                return [.. items];
            }
        }

        private AudioSettingsEntry? pickedUpEntry;
        private int pickedUpEntryIndex;
        private int mouseStartY;

        public AudioSettingsPriorityPanel()
        {
            InitializeComponent();
            CreateControlList();
        }

        private void HandlePriorityChanged(object? sender, EventArgs e)
        {
            this.InvokeIfRequired(UpdateItems);
        }

        private void UpdateItems()
        {
            SortItems();
            UpdateItemOffsets();
        }

        public void ClearItems()
        {
            items.Clear();
            CreateControlList();
        }

        public void ImportSettings(AudioEffectSpectrumSettings[] spectrumSettings, OWOHelper owo)
        {
            foreach (AudioEffectSpectrumSettings settings in spectrumSettings)
            {
                AudioSettingsEntry entry = AudioSettingsEntry.FromSpectrumSettings(settings, owo);
                items.Add(entry);
            }

            CreateControlList();
        }

        private void CreateControlList()
        {
            SuspendLayout();
            Controls.Clear();

            SortItems();

            for (int i = 0; i < items.Count; i++) {
                AudioSettingsEntry item = items[i];

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

            UpdateItemOffsets();
        }

        private void UpdateItemOffsets()
        {
            int lastY = itemSpacing;

            for (int i = 0 ; i < items.Count; i++)
            {
                AudioSettingsEntry item = items[i];
                item.AllowDrag = DragReordering;

                if (!item.IsPickedUp)
                {
                    item.Top = lastY;
                }

                item.Left = itemSpacing;
                item.Width = Width - ((itemSpacing * 2) + scrollbarSpacing);

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
                UpdateItemOffsets();
                return;
            }

            pickedUpEntry = entry;
            pickedUpEntry.BringToFront();

            mouseStartY = e.Y;
            pickedUpEntryIndex = items.IndexOf(pickedUpEntry);
        }

        public void HandleItemDragStop(object? sender, MouseEventArgs e)
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            UpdatePriorities();
            pickedUpEntry = null;

            UpdateItemOffsets();

            Refresh();
        }

        private void UpdatePriorities()
        {
            // Update priorities starting with the lowest value
            int lastPriority = items.Min(item => item.Priority);
            AudioSettingsEntry? lastItem = items.LastOrDefault();
            if (lastItem != null)
            {
                lastItem.Priority = lastPriority;
            }

            for (int i = (items.Count - 2); i >= 0; i--)
            {
                AudioSettingsEntry item = items[i];

                // Ensure entry has a higher priority than the last one
                if (item.Priority <= lastPriority)
                {
                    lastPriority++;
                    item.Priority = lastPriority;
                }
                else
                {
                    lastPriority = item.Priority;
                }
            }
        }

        private void SortItems()
        {
            AudioSettingsEntry[] newItems = items.OrderByDescending((entry) => entry.Priority).ToArray();

            for (int i = 0; i < newItems.Length; i++)
            {
                AudioSettingsEntry item = newItems[i];
                item.AllowDrag = DragReordering;
                items[i] = item;
            }
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

            //pickedUpEntry.Refresh();
            // Move item to its new position
            MoveItemToIndex();

            // Update position for all other items
            UpdateItemOffsets();

            // Reactivate MouseMove events
            pickedUpEntry.DragHandle1.MouseMove += AudioSettingsPriorityPanel_MouseMove;
        }

        private int GetPickedUpEntryIndex()
        {
            if (pickedUpEntry == null)
            {
                return -1;
            }

            int itemHeight = pickedUpEntry.Height + (itemSpacing * 2);

            float index = (float) pickedUpEntry.Top / (float) itemHeight;
            return Math.Min((int)Math.Round(index), items.Count);
        }

        private void MoveItemToIndex()
        {
            if (pickedUpEntry == null)
            {
                return;
            }

            int index = GetPickedUpEntryIndex();
            if (index == -1 || index == pickedUpEntryIndex)
            {
                return;
            }

            // Account for pickedUpEntry being removed from the list
            if (index > pickedUpEntryIndex)
            {
                index--;
            }

            items.RemoveAt(pickedUpEntryIndex);
            items.Insert(index, pickedUpEntry);

            pickedUpEntryIndex = index;
        }
    }
}
