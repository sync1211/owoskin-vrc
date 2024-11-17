using OWOVRC.Classes.OSC;

namespace OWOVRC.Classes.Effects
{
    public abstract class OSCEffectBase
    {
        public abstract void OnOSCMessageReceived(object? sender, OSCMessage message);
    }
}
