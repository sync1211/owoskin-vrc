using OWOVRC.Classes.OSC;

namespace OWOVRC.Classes.Sensations
{
    public abstract class OSCSensationBase
    {
        public abstract void OnOSCMessageReceived(object? sender, OSCMessage message);
    }
}
