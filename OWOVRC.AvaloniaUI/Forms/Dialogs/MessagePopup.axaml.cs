using Avalonia.Controls;
using OWOVRC.AvaloniaUI.Classes;
namespace OWOVRC.AvaloniaUI.Forms.Dialogs;

public partial class MessagePopup : Window
{
    public MessageBoxResult Result { get; private set; } = MessageBoxResult.None;
    public MessagePopup(string description, string title, bool showOkButton, string okButtonText = "Ok", bool showCancelButton = false, string cancelButtonText = "Cancel")
    {

        this.Title = title;
        messageLabel.Content = description;

        InitializeComponent();
    }

    private void OkButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Result = MessageBoxResult.Confirm;
        this.Close();
    }

    private void CancelButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Result = MessageBoxResult.Cancel;
        this.Close();
    }
}