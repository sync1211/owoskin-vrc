using Serilog;
using System.Diagnostics;

namespace OWOVRC.Classes.Helpers
{
    public static class CPUHelper
    {
        public static void SetCpuAffinity(IntPtr cores)
        {
            int coreCount = Environment.ProcessorCount;
            long maxAffinityValue = 2L << coreCount;

            if (maxAffinityValue < cores.ToInt64())
            {
                Log.Error("Failed to set CPU affinity: Invalid CPU affinity value: {cores}", cores);
                return;
            }

            Process process = Process.GetCurrentProcess();
            process.ProcessorAffinity = cores;
            Log.Information("CPU affinity set to {cores}!", cores.ToString("X"));
        }

        public static void SetProcessPriority(ProcessPriorityClass priority)
        {
            Process process = Process.GetCurrentProcess();
            process.PriorityClass = priority;
            Log.Information("Process priority set to {priority}!", priority);
        }
    }
}
