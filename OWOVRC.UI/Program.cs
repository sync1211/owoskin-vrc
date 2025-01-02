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
            if (AdminDetection.IsRunningAsAdmin())
            {
                MessageBox.Show(
                    $"This application is not intended to be run as administrator!{Environment.NewLine}Please avoid running applications as administrator unless ABSOLUTELY NECCESSARY!{Environment.NewLine}{Environment.NewLine}If you encounter permission errors, please file an issue on GitHub instead!",
                    "Application is running as admin!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}