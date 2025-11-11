using NAudio.SoundFont;
using OWOGame;
using OWOVRC.UI.Classes.Proxies;
using Serilog;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace OWOVRC.UI.Forms
{
    public partial class AppDiscoveryForm : Form
    {
        public HostEntry? SelectedApp { get; private set; }
        private readonly bool ResolveHostnames = true;

        private const int TIMER_INTERVAL = 500;
        private readonly System.Timers.Timer timer;

        private readonly BindingList<HostEntry> discoveredApps;

        public AppDiscoveryForm(bool resolveHostnames = true)
        {
            InitializeComponent();
            discoveredApps = [];
            appListBox.DataSource = discoveredApps;

            timer = new()
            {
                Interval = TIMER_INTERVAL,
                AutoReset = true
            };
            timer.Elapsed += TimerElapsed;
            ResolveHostnames = resolveHostnames;
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
                if (InvokeRequired)
                {
                    this.Invoke(RefreshItems);
                }
                else
                {
                    RefreshItems();
                }
            }
            catch (ObjectDisposedException)
            {
                timer.Stop();
            }
        }

        private static string? GetHostName(string ip)
        {
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                return hostEntry.HostName;
            }
            catch (SocketException ex)
            {
                Log.Debug("Failed to resolve hostname for IP {IP}: {Message}", ip, ex.Message);
                return null;
            }
        }

        private void RefreshItems()
        {
            string? selectedIP= null;
            int selectedIndex = -1;
            if (appListBox.SelectedItem is HostEntry entry)
            {
                selectedIP = entry.IP;
            }

            // Save old enteries to avoid creating new objects
            Dictionary<string, HostEntry> oldEntries = discoveredApps.ToDictionary(host => host.IP, host => host);

            discoveredApps.RaiseListChangedEvents = false;
            discoveredApps.Clear();

            string[] newDiscoveredApps = OWO.DiscoveredApps;
            for (int i = 0; i < newDiscoveredApps.Length; i++)
            {
                string ip = newDiscoveredApps[i];
                string? hostName = null;
                if (ResolveHostnames)
                {
                    hostName = GetHostName(ip);
                }

                HostEntry hostEntry =
                    oldEntries.GetValueOrDefault(ip)
                    ?? new(ip, hostName);

                discoveredApps.Add(hostEntry);

                // Restore selection
                if (hostEntry.IP == selectedIP)
                {
                    selectedIndex = i;
                }
            }

            discoveredApps.RaiseListChangedEvents = true;
            discoveredApps.ResetBindings();

            if (selectedIndex != -1)
            {
                appListBox.SelectedIndex = selectedIndex;
            }
        }

        private void SelectEntryButton_Click(object sender, EventArgs e)
        {
            if (appListBox.SelectedIndex < 0)
            {
                return;
            }

            SelectedApp = discoveredApps[appListBox.SelectedIndex];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            selectEntryButton.Enabled = appListBox.SelectedItem is not null;
        }

        private void AppDiscoveryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Elapsed -= TimerElapsed;

            timer.Stop();
            timer.Dispose();
        }
    }
}
