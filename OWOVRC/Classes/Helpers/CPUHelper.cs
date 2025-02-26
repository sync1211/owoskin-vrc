using Serilog;
using System.Diagnostics;

namespace OWOVRC.Classes.Helpers
{
    public static class CPUHelper
    {
        public static readonly long MaxAffinityValue = (2L << (Environment.ProcessorCount - 1)) - 1; // Assuming the CPU is not hot-swappable. (Yes, those exist!)

        public static void SetCpuAffinity(IntPtr cores)
        {
            if (MaxAffinityValue < cores.ToInt64())
            {
                Log.Error("Failed to set CPU affinity: Invalid CPU affinity value: {cores}", cores.ToString("X"));
                return;
            }

            using (Process Process = Process.GetCurrentProcess())
            {
                Process.ProcessorAffinity = cores;
            }
            Log.Information("CPU affinity set to {cores}!", cores.ToString("X"));
        }

        public static void SetProcessPriority(ProcessPriorityClass priority)
        {
            using (Process Process = Process.GetCurrentProcess())
            {
                Process.PriorityClass = priority;
            }
            Log.Information("Process priority set to {priority}!", priority);
        }

        public static IntPtr InvertAffinityValue(int affinity)
        {
            return new IntPtr(affinity ^ MaxAffinityValue);
        }
    }
}
