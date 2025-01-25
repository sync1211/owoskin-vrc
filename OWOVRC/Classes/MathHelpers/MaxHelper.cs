using System.Runtime.CompilerServices;

namespace OWOVRC.Classes.MathHelpers
{
    /// <summary>
    /// Implements multi-parameter max functions
    /// </summary>

    //NOTE: I wrote this function only because I don't like to create a new array whenever I have multiple maximum values to compare
    //      All functions are inlined at compile time.
    public static class MaxHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b, float c)
        {
            return Math.Max(a, Math.Max(b, c));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b, float c, float d)
        {
            return Math.Max(a, Max(b, c, d));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b, float c, float d, float e)
        {
            return Math.Max(a, Max(b, c, d, e));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float a, float b, float c, float d, float e, float f)
        {
            return Math.Max(a, Max(b, c, d, e, f));
        }
    }
}
