using BuildSoft.OscCore;

namespace OWOVRC.Classes.OSC
{
    public readonly struct OSCMessage
    {
        public readonly string Address;
        public readonly OscMessageValues Values;

        public OSCMessage(string address, OscMessageValues values)
        {
            Address = address;
            Values = values;
        }
    }
}
