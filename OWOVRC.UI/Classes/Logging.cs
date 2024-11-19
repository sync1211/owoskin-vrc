using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Linq;

namespace OWOVRC.UI.Classes
{
    public static class Logging
    {
        public static LoggingLevelSwitch SetUpLogger(RichTextBox textBox)
        {
            LoggingLevelSwitch logLevelSwitch = new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(logLevelSwitch)
                .WriteTo.Console()
#if DEBUG
                .WriteTo.Debug()
#endif
                .WriteTo.RichTextBox(textBox)
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
