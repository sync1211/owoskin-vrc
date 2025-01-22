using System.Security.Principal;

namespace OWOVRC.Classes.Helpers
{
    public static class AdminDetection
    {
        public static bool IsRunningAsAdmin()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
