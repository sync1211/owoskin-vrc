﻿using OWOGame;

namespace OWOVRC.UI.Forms
{
    public partial class AppDiscoveryForm : Form
    {
        public string? SelectedApp { get; private set; }
        private readonly System.Timers.Timer timer;

        public AppDiscoveryForm()
        {
            InitializeComponent();
            timer = new() { Interval = 500, AutoReset = true };
            timer.Elapsed += TimerElapsed;
        }

        private void AppDiscoveryForm_Shown(object sender, EventArgs e)
        {
            OWO.StartScan();
            timer.Start();
        }

        private void TimerElapsed(object? sender, EventArgs args)
        {
            try
            {
                this.Invoke(RefreshItems);
            }
            catch (ObjectDisposedException)
            {
                timer.Stop();
            }
        }

        private void RefreshItems()
        {
            int selectedIndex = listBox1.SelectedIndex;

            listBox1.Items.Clear();
            listBox1.Items.AddRange(OWO.DiscoveredApps);

            if (selectedIndex >= 0 && selectedIndex < listBox1.Items.Count)
            {
                listBox1.SelectedIndex = selectedIndex;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectEntryButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is not string slectedApp)
            {
                return;
            }

            SelectedApp = slectedApp;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            selectEntryButton.Enabled = listBox1.SelectedItem is not null;
        }

        private void AppDiscoveryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Elapsed -= TimerElapsed;

            timer.Stop();
            timer.Dispose();
        }
    }
}
