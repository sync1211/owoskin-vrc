using OWOVRC.Classes.Helpers;
using Serilog;
using Serilog.Core;

namespace OWOVRC.Classes
{
    public static class CommandlineSettings
    {
        public static CommandlineParser ProcessCommandlineArgs(LoggingLevelSwitch logLevelSwitch)
        {
            {
                CommandlineParser args = new(Environment.GetCommandLineArgs());

                // CPU affinity
                if (args.CpuAffinity != null)
                {
                    CPUHelper.SetCpuAffinity(args.CpuAffinity.Value);
                }

                // Process priority
                if (args.Priority != null)
                {
                    CPUHelper.SetProcessPriority(args.Priority.Value);
                }

                // Log level
                if (args.LogLevel != null)
                {
                    logLevelSwitch.MinimumLevel = args.LogLevel.Value;
                    Log.Information("Log level changed to: {newLevel}", logLevelSwitch.MinimumLevel);
                }

                return args;
            }
        }
    }
}
