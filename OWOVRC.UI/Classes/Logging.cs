using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace OWOVRC.UI.Classes
{
    public static class Logging
    {
        public static LoggingLevelSwitch SetUpLogger(LogEventLevel? logLevel = null, RichTextBox ? textBox = null)
        {
            LoggingLevelSwitch logLevelSwitch = new();

            LoggerConfiguration loggerConfig = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
                .WriteTo.Debug();

            if (textBox != null)
            {
                loggerConfig = loggerConfig.WriteTo.RichTextBox(textBox);
            }

            Log.Logger = loggerConfig.CreateLogger();

            // Set log level
            if (logLevel != null)
            {
                logLevelSwitch.MinimumLevel = logLevel.Value;
            }

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
