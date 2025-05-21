using OWOVRC.Classes.Commandline;
using OWOVRC.Classes.Helpers;
using Serilog.Core;
using System.Runtime.InteropServices;

namespace OWOVRC.UI
{
    internal static partial class Program
    {
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool AttachConsole(int dwProcessId);

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Allow logging to console
            AttachConsole(-1);

            ApplicationConfiguration.Initialize();

            // Logger
            LoggingLevelSwitch logLevelSwitch = Classes.Logging.SetUpLogger();

            // Parse commandline switches
            CommandlineArgs args = CommandlineSettings.ParseAndApply(logLevelSwitch);

            // Admin detection
            if (AdminDetection.IsRunningAsAdmin())
            {
                MessageBox.Show(
                    $"This application is not intended to be run as administrator!{Environment.NewLine}Please avoid running applications as administrator unless ABSOLUTELY NECCESSARY!{Environment.NewLine}{Environment.NewLine}If you encounter permission errors, please file an issue on GitHub instead!",
                    "Application is running as admin!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Create form
            using (MainForm mainForm = new(logLevelSwitch))
            {
                // Autostart (--start): Immediately start connecting to OWO
                if (args.Autostart)
                {
                    mainForm.Shown += (_, _) => mainForm.StartConnection();
                }

                // Show form
                Application.Run(mainForm);
            }
        }
    }
}