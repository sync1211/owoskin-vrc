using OWOVRC.AvaloniaUI.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Foundation.Metadata;

namespace OWOVRC.AvaloniaUI.Classes
{
    internal static class MessageBox
    {
        [Deprecated("Calls to MessageBox need to be modernized for AvaloniaUI.", DeprecationType.Deprecate, 0)]
        public static DialogResult Show(string description, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessagePopup dialog = null!;
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    dialog = new MessagePopup(
                        description: description,
                        title: title,
                        showOkButton: true,
                        showCancelButton: false
                    );
                    break;
                case MessageBoxButtons.OKCancel:
                    dialog = new MessagePopup(
                        description: description,
                        title: title,
                        showOkButton: true,
                        showCancelButton: true
                    );
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    throw new NotImplementedException("AbortRetryIgnore button configuration is not implemented.");
                //TODO: Implement me!
                case MessageBoxButtons.YesNoCancel:
                    throw new NotImplementedException("AbortRetryIgnore button configuration is not implemented.");
                //TODO: Implement me!
                case MessageBoxButtons.YesNo:
                    dialog = new MessagePopup(
                        description: description,
                        title: title,
                        showOkButton: true,
                        okButtonText: "Yes",
                        showCancelButton: true,
                        cancelButtonText: "No"
                    );
                    break;
                case MessageBoxButtons.RetryCancel:
                    throw new NotImplementedException("AbortRetryIgnore button configuration is not implemented.");
                    //TODO: Implement me!
                case MessageBoxButtons.CancelTryContinue:
                    throw new NotImplementedException("AbortRetryIgnore button configuration is not implemented.");
                    //TODO: Implement me!
                default:
                    break;
            }

            dialog.Show();

            return DialogResult.None; // Placeholder return value
        }
    }
}
