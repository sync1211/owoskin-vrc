using Avalonia.Controls;
using Serilog;
using Serilog.Configuration;
using System;

namespace OWOVRC.AvaloniaUI.Classes.Logging.Sinks
{
    internal static class AvaloniaTextBoxSinkExtensions
    {
        public static LoggerConfiguration AvaloniaTextBox(this LoggerSinkConfiguration loggerConfiguration, TextBox textBox, IFormatProvider? formatProvider = null)
        {
            return loggerConfiguration.Sink(new AvaloniaTextBoxSink(formatProvider, textBox));
        }
    }
}
