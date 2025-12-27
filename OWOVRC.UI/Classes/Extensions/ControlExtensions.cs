namespace OWOVRC.UI.Classes.Extensions
{
    using System;

    public static class ControlExtensions
    {
        public static void InvokeIfRequired(this Control control, Delegate del, params object?[]? args)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(del, args);
            }
            else
            {
                del.DynamicInvoke(args);
            }
        }
    }
}