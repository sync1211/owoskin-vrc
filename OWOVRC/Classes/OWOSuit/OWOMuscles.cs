using OWOGame;
using System.Runtime.CompilerServices;

namespace OWOVRC.Classes.OWOSuit
{
    public static class OWOMuscles
    {
        public static readonly Dictionary<string, Muscle> Muscles = new()
        {
            // Shadoki-Style naming
            { "owo_suit_pectoral_r",  Muscle.Pectoral_R },
            { "owo_suit_pectoral_l",  Muscle.Pectoral_L },
            { "owo_suit_abdominal_r",  Muscle.Abdominal_R },
            { "owo_suit_abdominal_l",  Muscle.Abdominal_L },
            { "owo_suit_arm_r",  Muscle.Arm_R },
            { "owo_suit_arm_l",  Muscle.Arm_L },
            { "owo_suit_dorsal_r",  Muscle.Dorsal_R },
            { "owo_suit_dorsal_l",  Muscle.Dorsal_L },
            { "owo_suit_lumbar_r",  Muscle.Lumbar_R },
            { "owo_suit_lumbar_l",  Muscle.Lumbar_L },

            // Exact naming
            { "pectoral_r",  Muscle.Pectoral_R },
            { "pectoral_l",  Muscle.Pectoral_L },
            { "abdominal_r",  Muscle.Abdominal_R },
            { "abdominal_l",  Muscle.Abdominal_L },
            { "arm_r",  Muscle.Arm_R },
            { "arm_l",  Muscle.Arm_L },
            { "dorsal_r",  Muscle.Dorsal_R },
            { "dorsal_l",  Muscle.Dorsal_L },
            { "lumbar_r",  Muscle.Lumbar_R },
            { "lumbar_l",  Muscle.Lumbar_L },

            // bHaptics mapping - Front
            { "bosc_v1_vestfront_1", Muscle.Pectoral_R },
            { "bosc_v1_vestfront_2", Muscle.Pectoral_R },
            { "bosc_v1_vestfront_5", Muscle.Pectoral_R },
            { "bosc_v1_vestfront_6", Muscle.Pectoral_R },

            { "bosc_v1_vestfront_3", Muscle.Pectoral_L },
            { "bosc_v1_vestfront_4", Muscle.Pectoral_L },
            { "bosc_v1_vestfront_7", Muscle.Pectoral_L },
            { "bosc_v1_vestfront_8", Muscle.Pectoral_L },

            { "bosc_v1_vestfront_13", Muscle.Abdominal_R },
            { "bosc_v1_vestfront_14", Muscle.Abdominal_R },
            { "bosc_v1_vestfront_17", Muscle.Abdominal_R },
            { "bosc_v1_vestfront_18", Muscle.Abdominal_R },

            { "bosc_v1_vestfront_15", Muscle.Abdominal_L },
            { "bosc_v1_vestfront_16", Muscle.Abdominal_L },
            { "bosc_v1_vestfront_19", Muscle.Abdominal_L },
            { "bosc_v1_vestfront_20", Muscle.Abdominal_L },

            // bHaptics mapping - Back
            { "bosc_v1_vestback_1", Muscle.Dorsal_L },
            { "bosc_v1_vestback_2", Muscle.Dorsal_L },
            { "bosc_v1_vestback_5", Muscle.Dorsal_L },
            { "bosc_v1_vestback_6", Muscle.Dorsal_L },

            { "bosc_v1_vestback_3", Muscle.Dorsal_R },
            { "bosc_v1_vestback_4", Muscle.Dorsal_R },
            { "bosc_v1_vestback_7", Muscle.Dorsal_R },
            { "bosc_v1_vestback_8", Muscle.Dorsal_R },

            { "bosc_v1_vestback_13", Muscle.Lumbar_L },
            { "bosc_v1_vestback_14", Muscle.Lumbar_L },
            { "bosc_v1_vestback_17", Muscle.Lumbar_L },
            { "bosc_v1_vestback_18", Muscle.Lumbar_L },

            { "bosc_v1_vestback_15", Muscle.Lumbar_R },
            { "bosc_v1_vestback_16", Muscle.Lumbar_R },
            { "bosc_v1_vestback_19", Muscle.Lumbar_R },
            { "bosc_v1_vestback_20", Muscle.Lumbar_R },
        };

        public static readonly Dictionary<string, Muscle[]> MuscleGroups = new()
        {
            { "all", Muscle.All },
            { "upperChest", [Muscle.Pectoral_L, Muscle.Pectoral_R] },
            { "frontMuscles", Muscle.Front },
            { "backMuscles", Muscle.Back },
            { "leftMuscles", [Muscle.Arm_L, Muscle.Pectoral_L, Muscle.Dorsal_L, Muscle.Abdominal_L, Muscle.Lumbar_L] },
            { "rightMuscles", [Muscle.Arm_R, Muscle.Pectoral_R, Muscle.Dorsal_R, Muscle.Abdominal_R, Muscle.Lumbar_R] }
        };

        //OPTIMIZATION: Cache muscle count
        // This is needed as Muscle.All creates a new array every time it's called.
        // So every time we call MuscleGroups.MusclesCount, we're creating a new array and discarding it again!
        public static readonly int MusclesCount = Muscle.All.Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRightMuscle(Muscle muscle)
        {
            return int.IsEvenInteger(muscle.id);
        }
    }
}
