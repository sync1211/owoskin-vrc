using Avalonia.Controls;
using OWOVRC.AvaloniaUI.Classes.Logging.Sinks;
using Serilog;
using Serilog.Core;

namespace OWOVRC.AvaloniaUI.Classes.Logging
{
    public class Logging : OWOVRC.Classes.Logging
    {
        public static LoggingLevelSwitch SetUpWithTextBox(TextBox textBox, LoggingLevelSwitch? logLevelSwitch = null)
        {
            LoggingLevelSwitch loggingLevelSwitch = logLevelSwitch ?? new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.AvaloniaTextBox(textBox)
                .CreateLogger();

            Log.Debug("UI logger created!");

            return loggingLevelSwitch;
        }
    }
}
