using OWOVRC.Classes.OSC;

namespace OWOVRC.Classes.Effects
{
    public abstract class OSCSensationBase
    {
        public abstract void OnOSCMessageReceived(object? sender, OSCMessage message);
    }
}
