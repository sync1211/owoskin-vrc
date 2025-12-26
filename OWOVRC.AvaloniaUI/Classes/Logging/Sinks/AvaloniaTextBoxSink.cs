using Avalonia.Controls;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace OWOVRC.AvaloniaUI.Classes.Logging.Sinks
{
    internal class AvaloniaTextBoxSink: ILogEventSink
    {
        private readonly TextBox textBox;
        private readonly IFormatProvider? formatProvider;

        public AvaloniaTextBoxSink(IFormatProvider? formatProvider, TextBox textBox)
        {
            this.textBox = textBox;
            this.formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            textBox.Text += $"[{logEvent.Level}] {logEvent.RenderMessage(formatProvider)}{Environment.NewLine}";
        }

        public void Clear()
        {
            textBox.Text = string.Empty;
        }
    }
}
