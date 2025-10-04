using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.Classes.Effects
{
    public abstract class OSCEffectBase(OWOHelper owo)
    {
        protected readonly OWOHelper owo = owo;

        public abstract void RegisterCallbacks(OSCReceiver receiver);
        public abstract void UnregisterCallbacks(OSCReceiver receiver);

        public abstract void Stop();
    }
}
