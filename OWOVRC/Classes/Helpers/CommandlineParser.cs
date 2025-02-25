using Serilog;
using System.Globalization;

namespace OWOVRC.Classes.Helpers
{
    public class CommandlineParser
    {
        private const string AUTOSTART_SWITCH = "--start";
        private const string CPU_AFFINITY_ARG = "--affinity=";

        public bool Autostart { get; private set; }
        public IntPtr? CpuAffinity { get; private set; }

        public CommandlineParser(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                // Autostart
                if (arg.Equals(AUTOSTART_SWITCH, StringComparison.CurrentCultureIgnoreCase))
                {
                    Autostart = true;
                }

                // CPU affinity (part 1)
                else if (arg.StartsWith(CPU_AFFINITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(CPU_AFFINITY_ARG.Length).TrimStart('0', 'x');
                    if (int.TryParse(argValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int affinity) && affinity > 0)
                    {
                        CpuAffinity = new IntPtr(affinity);
                    }
                    else
                    {
                        Log.Error("Invalid CPU affinity value: {arg}", arg);
                    }
                }
            }
        }
    }
}
