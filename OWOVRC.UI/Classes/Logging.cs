using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace OWOVRC.UI.Classes
{
    public static class Logging
    {
        public static void SetUpLogger(LogEventLevel logLevel)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            Log.Information("Logging started!");
        }

        public static LoggingLevelSwitch SetUpWithTextBox(RichTextBox textBox)
        {
            LoggingLevelSwitch logLevelSwitch = new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RichTextBox(textBox)
                .CreateLogger();

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
