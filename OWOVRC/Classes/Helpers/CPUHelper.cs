using Serilog;
using System.Diagnostics;

namespace OWOVRC.Classes.Helpers
{
    public static class CPUHelper
    {
        public static readonly long MaxAffinityValue = GetMaxAffinityForProcessorCount(Environment.ProcessorCount); // Assuming the CPU is not hot-swappable. (Yes, those exist!)

        public static long GetMaxAffinityForProcessorCount(int processorCount)
        {
            if (processorCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(processorCount), "Processor count must be greater than zero.");
            }
            return (2L << (processorCount - 1)) - 1;
        }

        public static void SetCpuAffinity(long cores)
        {
            if (MaxAffinityValue < cores || cores <= 0)
            {
                Log.Error("Failed to set CPU affinity: Invalid CPU affinity value: {Cores:X}", cores);
                return;
            }

            using (Process Process = Process.GetCurrentProcess())
            {
                Process.ProcessorAffinity = (nint)cores;
            }
            Log.Information("CPU affinity set to {Cores:X}!", cores);
        }

        public static void SetProcessPriority(ProcessPriorityClass priority)
        {
            using (Process Process = Process.GetCurrentProcess())
            {
                Process.PriorityClass = priority;
            }
            Log.Information("Process priority set to {Priority}!", priority);
        }

        public static long InvertAffinityValue(long affinity, long? maxAffinity = null)
        {
            maxAffinity ??= MaxAffinityValue;
            return affinity ^ maxAffinity.Value;
        }
    }
}
