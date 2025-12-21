using Avalonia.Controls;
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
                .CreateLogger();

            //TODO: Create a TextBox sink for Serilog

            Log.Debug("UI logger created!");

            return loggingLevelSwitch;
        }
    }
}
