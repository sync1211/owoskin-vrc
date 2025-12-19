using Avalonia;
using Avalonia.Controls;

namespace OWOVRC.AvaloniaUI.Controls
{
    public class GroupBox: ContentControl
        {
        public static readonly StyledProperty<string?> HeaderProperty =
            AvaloniaProperty.Register<GroupBox, string?>(nameof(Header));

        public string? Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
    }
}
