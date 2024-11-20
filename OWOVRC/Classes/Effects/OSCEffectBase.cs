using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.Classes.Effects
{
    public abstract class OSCEffectBase(OWOHelper owo)
    {
        protected readonly OWOHelper owo = owo;

        public abstract void OnOSCMessageReceived(object? sender, OSCMessage message);
        public abstract void RegisterSensations();
        public abstract void Reset();
    }
}
