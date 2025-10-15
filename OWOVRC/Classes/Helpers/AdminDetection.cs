using Microsoft.Win32;
using Serilog;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace OWOVRC.Classes.Helpers
{
    public static class AdminDetection
    {
        public static bool IsRunningAsAdmin()
        {
#if !TARGET_LINUX
            return IsRunningAsWindowsAdmin();
#else
            return IsRunningAsRoot();
#endif
        }

#if !TARGET_LINUX
        private static bool IsRunningAsWindowsAdmin()
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
#else
        [DllImport("libc")]
        private static extern uint getuid();

        private static bool IsRunningAsRoot()
        {
            return getuid() == 0;
        }
#endif
    }
}
