using System.Diagnostics;
using System.Runtime.InteropServices;
using Serilog.Events;

namespace OWOVRC.Classes.Commandline
{
    [StructLayout(LayoutKind.Auto)]
    public struct CommandlineArgs
    {
        public bool Autostart { get; internal set; }
        public long? CpuAffinity { get; internal set; }
        public ProcessPriorityClass? Priority { get; internal set; }
        public LogEventLevel? LogLevel { get; internal set; }
    }
}
