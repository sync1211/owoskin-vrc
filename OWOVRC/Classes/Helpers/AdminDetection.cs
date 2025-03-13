using Microsoft.Win32;
using Serilog;
using System.Security.Principal;

namespace OWOVRC.Classes.Helpers
{
    public static class AdminDetection
    {
        public static bool IsRunningAsAdmin()
        {
            if (IsRunningViaWine())
            {
                Log.Information("OWOVRC is running via WINE. Nice!");
                return false; // Using wine will cause the program to run as admin in the context of wine
            }
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool IsRunningViaWine()
        {
            return Registry.LocalMachine.OpenSubKey("Software\\Wine", false) != null;
        }
    }
}
