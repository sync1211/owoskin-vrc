using BuildSoft.OscCore;
using Serilog;

namespace OWOVRC.Classes.OSC
{
    public static class OSCHelpers
    {
        public static float GetFloatValueFromMessage(OSCMessage message, int index = 0)
        {
            TypeTag typeTag = message.Values.GetTypeTag(index);

            switch (typeTag)
            {
                case TypeTag.Float64:
                    return (float) message.Values.ReadFloat64Element(index);
                case TypeTag.Float32:
                    return message.Values.ReadFloatElement(index);
                case TypeTag.False:
                    return 0f;
                case TypeTag.True:
                    return 1f;
                case TypeTag.Int64:
                    return message.Values.ReadInt64Element(index);
                case TypeTag.Int32:
                    return message.Values.ReadIntElement(index);
                default:
                    Log.Warning("No valid float value received in message for {address}!", message.Address);
                    return 0f;
            }
        }
    }
}
