using Serilog;
using System.Diagnostics;
using System.Globalization;

namespace OWOVRC.Classes.Helpers
{
    public class CommandlineParser
    {
        private const string AUTOSTART_SWITCH = "--start";
        private const string CPU_AFFINITY_ARG = "--affinity=";
        private const string REVERSE_AFFINITY_ARG = "--vrc-affinity=";
        private const string PROCESS_PRIORITY_ARG = "--process-priority=";

        public readonly bool Autostart;
        public readonly IntPtr? CpuAffinity;
        public readonly ProcessPriorityClass? Priority;

        private readonly ProcessPriorityClass[] priorityClasses =
        {
            ProcessPriorityClass.Idle,        // -2
            ProcessPriorityClass.BelowNormal, // -1
            ProcessPriorityClass.Normal,      // 0
            ProcessPriorityClass.AboveNormal, // 1
            ProcessPriorityClass.High         // 2
         // ProcessPriorityClass.RealTime     // 3 (shouldn't be used)
        };

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

                // CPU affinity
                else if (arg.StartsWith(CPU_AFFINITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(CPU_AFFINITY_ARG.Length).TrimStart('0', 'x');
                    if (!int.TryParse(argValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int affinity) || affinity <= 0)
                    {
                        Log.Error("Invalid CPU affinity value: {arg}", argValue);
                        continue;
                    }

                    CpuAffinity = new IntPtr(affinity);
                }

                // CPU affinity (inverted)
                else if (arg.StartsWith(REVERSE_AFFINITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(REVERSE_AFFINITY_ARG.Length).TrimStart('0', 'x');
                    if (!int.TryParse(argValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int affinity) || affinity > CPUHelper.MaxAffinityValue)
                    {
                        Log.Error("Invalid inverse CPU affinity value: {arg}", argValue);
                        continue;
                    }

                    if (CpuAffinity != null)
                    {
                        Log.Warning("CPU affinity already set, ignoring other CPU affinity value of {arg}", CpuAffinity.Value.ToString("X"));
                    }

                    CpuAffinity = CPUHelper.InvertAffinityValue(affinity);
                }

                // Process priority
                else if (arg.StartsWith(PROCESS_PRIORITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(PROCESS_PRIORITY_ARG.Length);
                    if (!int.TryParse(argValue, out int priority))
                    {
                        Log.Error("Invalid process priority value: {arg}", argValue);
                        continue;
                    }

                    ProcessPriorityClass? priorityClass = IntToPriorityClass(priority);
                    if (priorityClass == null)
                    {
                        Log.Error("Invalid process priority value: {arg}", argValue);
                        continue;
                    }

                    if (CpuAffinity != null)
                    {
                        Log.Warning("CPU affinity already set, ignoring other CPU affinity value of {arg}", CpuAffinity.Value.ToString("X"));
                    }

                    Priority = priorityClass.Value;
                }
            }
        }

        private ProcessPriorityClass? IntToPriorityClass(int priorityId)
        {
            if (priorityId < -2 || priorityId > 2)
            {
                return null;
            }

            return priorityClasses[priorityId + 2];
        }
    }
}
