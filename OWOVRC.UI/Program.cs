using OWOVRC.Classes.Helpers;
using OWOVRC.UI.Classes;
using Serilog.Events;
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
            const LogEventLevel defaultLogLevel =
#if DEBUG
                Serilog.Events.LogEventLevel.Debug;
#else
                Serilog.Events.LogEventLevel.Information;
#endif
            Logging.SetUpLogger(defaultLogLevel);

            // Parse commandline switches
            CommandlineParser args = new(Environment.GetCommandLineArgs());

            // CPU affinity
            if (args.CpuAffinity != null)
            {
                CPUHelper.SetCpuAffinity(args.CpuAffinity.Value);
            }

            // Process priority
            if (args.Priority != null)
            {
                CPUHelper.SetProcessPriority(args.Priority.Value);
            }

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
            using (MainForm mainForm = new())
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