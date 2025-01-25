using OWOVRC.Classes;

namespace OWOVRC.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            if (AdminDetection.IsRunningAsAdmin())
            {
                MessageBox.Show(
                    $"This application is not intended to be run as administrator!{Environment.NewLine}Please avoid running applications as administrator unless ABSOLUTELY NECCESSARY!{Environment.NewLine}{Environment.NewLine}If you encounter permission errors, please file an issue on GitHub instead!",
                    "Application is running as admin!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Parse commandline switches
            string[] args = Environment.GetCommandLineArgs();
            bool autostart = args.Contains("--start");

            // Create form
            using (MainForm mainForm = new())
            {
                // Autostart (--start): Immediately start connecting to OWO
                if (autostart)
                {
                    mainForm.Shown += (_, _) => mainForm.StartConnection();
                }

                // Show form
                Application.Run(mainForm);
            }
        }
    }
}