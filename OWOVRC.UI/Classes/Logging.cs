using Serilog;
using Serilog.Core;

namespace OWOVRC.UI.Classes
{
    public class Logging: OWOVRC.Classes.Logging
    {
        public static LoggingLevelSwitch SetUpWithTextBox(RichTextBox textBox, LoggingLevelSwitch? logLevelSwitch = null)
        {
            LoggingLevelSwitch loggingLevelSwitch = logLevelSwitch ?? new();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RichTextBox(textBox)
                .CreateLogger();

            Log.Debug("UI logger created!");

            return loggingLevelSwitch;
        }
    }
}
