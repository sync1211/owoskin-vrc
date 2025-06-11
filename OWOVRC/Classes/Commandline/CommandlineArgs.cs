using System.Diagnostics;
using Serilog.Events;

namespace OWOVRC.Classes.Commandline
{
    public struct CommandlineArgs
    {
        public bool Autostart { get; internal set; }
        public long? CpuAffinity { get; internal set; }
        public ProcessPriorityClass? Priority { get; internal set; }
        public LogEventLevel? LogLevel { get; internal set; }
    }
}
