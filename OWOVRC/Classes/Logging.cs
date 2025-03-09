using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace OWOVRC.Classes
{
    public abstract class Logging
    {
        public static LoggingLevelSwitch SetUpLogger()
        {
            LoggingLevelSwitch logLevelSwitch = new();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            Log.Debug("Logging started!");

            return logLevelSwitch;
        }

        public static readonly LogEventLevel[] Levels =
        [
            LogEventLevel.Verbose,
            LogEventLevel.Debug,
            LogEventLevel.Information,
            LogEventLevel.Warning,
            LogEventLevel.Error,
            LogEventLevel.Fatal
        ];
    }
}
