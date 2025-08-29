using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace OWOVRC.Audio.Demo.Classes
{
    public static class Logging
    {
        public static LoggingLevelSwitch SetUpLogger()
        {
            LoggingLevelSwitch logLevelSwitch = new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
#if DEBUG
                .WriteTo.Debug()
#endif
                .CreateLogger();

            Log.Information("Logging started!");
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
