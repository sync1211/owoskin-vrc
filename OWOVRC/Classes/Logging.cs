using Serilog.Core;
using Serilog.Events;
using Serilog;

namespace OWOVRC.Classes
{
    public static class Logging
    {
        public static LoggingLevelSwitch SetUpLogger()
        {
            LoggingLevelSwitch logLevelSwitch = new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
#if DEBUG
                .WriteTo.Debug()
#endif
                .CreateLogger();

            Log.Information("Logging started!");
            return logLevelSwitch;
        }
    }
}
