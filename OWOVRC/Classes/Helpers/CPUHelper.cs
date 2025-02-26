using Serilog;
using System.Diagnostics;

namespace OWOVRC.Classes.Helpers
{
    public static class CPUHelper
    {
        private static readonly Process Process = Process.GetCurrentProcess();
        public static readonly long MaxAffinityValue = (2L << (Environment.ProcessorCount - 1)) - 1; // Assuming the CPU is not hot-swappable. (Yes, those exist!)

        public static void SetCpuAffinity(IntPtr cores)
        {
            if (MaxAffinityValue < cores.ToInt64())
            {
                Log.Error("Failed to set CPU affinity: Invalid CPU affinity value: {cores}", cores.ToString("X"));
                return;
            }

            Process.ProcessorAffinity = cores;
            Log.Information("CPU affinity set to {cores}!", cores.ToString("X"));
        }

        public static void SetProcessPriority(ProcessPriorityClass priority)
        {
            Process.PriorityClass = priority;
            Log.Information("Process priority set to {priority}!", priority);
        }

        public static IntPtr InvertAffinityValue(int affinity)
        {
            return new IntPtr(affinity ^ MaxAffinityValue);
        }
    }
}
