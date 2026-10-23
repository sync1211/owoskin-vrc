using BuildSoft.OscCore;
using Serilog;
using Windows.Perception.Spatial;

namespace OWOVRC.Classes.OSC
{
    public static class OSCHelpers
    {       
        public static float GetFloatValueFromMessageValues(OscMessageValues values, int index = 0)
        {
            TypeTag typeTag = values.GetTypeTag(index);
            switch (typeTag)
            {
                case TypeTag.Float64:
                    return (float) values.ReadFloat64Element(index);
                case TypeTag.Float32:
                    return values.ReadFloatElement(index);
                case TypeTag.False:
                    return 0f;
                case TypeTag.True:
                    return 1f;
                case TypeTag.Int64:
                    return values.ReadInt64Element(index);
                case TypeTag.Int32:
                    return values.ReadIntElement(index);
                case TypeTag.String:
                    string stringElement = values.ReadStringElement(index);
                    if (float.TryParse(stringElement, out float floatValue)) {
                        return floatValue;
                    }
                    Log.Warning("Message value string {value} could not be converted to float!", stringElement);
                    return 0f;
                default:
                    Log.Warning("No valid float value received in message values!");
                    return 0f;
            }
        }
    }
}
