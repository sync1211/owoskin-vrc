using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace OWOVRC.CLI.Classes
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
