using OWOGame;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace OWOVRC.Classes.OWOSuit
{
    public static class OWOMuscles
    {
        public static readonly ImmutableDictionary<string, Muscle> MusclesCaptialized = new Dictionary<string, Muscle>()
        {
            // Shadoki-Style naming
            { "owo_suit_Pectoral_R",  Muscle.Pectoral_R },
            { "owo_suit_Pectoral_L",  Muscle.Pectoral_L },
            { "owo_suit_Abdominal_R",  Muscle.Abdominal_R },
            { "owo_suit_Abdominal_L",  Muscle.Abdominal_L },
            { "owo_suit_Arm_R",  Muscle.Arm_R },
            { "owo_suit_Arm_L",  Muscle.Arm_L },
            { "owo_suit_Dorsal_R",  Muscle.Dorsal_R },
            { "owo_suit_Dorsal_L",  Muscle.Dorsal_L },
            { "owo_suit_Lumbar_R",  Muscle.Lumbar_R },
            { "owo_suit_Lumbar_L",  Muscle.Lumbar_L },

            // Exact naming
            { "Pectoral_R",  Muscle.Pectoral_R },
            { "Pectoral_L",  Muscle.Pectoral_L },
            { "Abdominal_R",  Muscle.Abdominal_R },
            { "Abdominal_L",  Muscle.Abdominal_L },
            { "Arm_R",  Muscle.Arm_R },
            { "Arm_L",  Muscle.Arm_L },
            { "Dorsal_R",  Muscle.Dorsal_R },
            { "Dorsal_L",  Muscle.Dorsal_L },
            { "Lumbar_R",  Muscle.Lumbar_R },
            { "Lumbar_L",  Muscle.Lumbar_L },

            // bHaptics mapping - Front
            { "bOSC_v1_VestFront_1", Muscle.Pectoral_R },
            { "bOSC_v1_VestFront_2", Muscle.Pectoral_R },
            { "bOSC_v1_VestFront_5", Muscle.Pectoral_R },
            { "bOSC_v1_VestFront_6", Muscle.Pectoral_R },

            { "bOSC_v1_VestFront_3", Muscle.Pectoral_L },
            { "bOSC_v1_VestFront_4", Muscle.Pectoral_L },
            { "bOSC_v1_VestFront_7", Muscle.Pectoral_L },
            { "bOSC_v1_VestFront_8", Muscle.Pectoral_L },

            { "bOSC_v1_VestFront_13", Muscle.Abdominal_R },
            { "bOSC_v1_VestFront_14", Muscle.Abdominal_R },
            { "bOSC_v1_VestFront_17", Muscle.Abdominal_R },
            { "bOSC_v1_VestFront_18", Muscle.Abdominal_R },

            { "bOSC_v1_VestFront_15", Muscle.Abdominal_L },
            { "bOSC_v1_VestFront_16", Muscle.Abdominal_L },
            { "bOSC_v1_VestFront_19", Muscle.Abdominal_L },
            { "bOSC_v1_VestFront_20", Muscle.Abdominal_L },

            // bHaptics mapping - Back
            { "bOSC_v1_VestBack_1", Muscle.Dorsal_L },
            { "bOSC_v1_VestBack_2", Muscle.Dorsal_L },
            { "bOSC_v1_VestBack_5", Muscle.Dorsal_L },
            { "bOSC_v1_VestBack_6", Muscle.Dorsal_L },

            { "bOSC_v1_VestBack_3", Muscle.Dorsal_R },
            { "bOSC_v1_VestBack_4", Muscle.Dorsal_R },
            { "bOSC_v1_VestBack_7", Muscle.Dorsal_R },
            { "bOSC_v1_VestBack_8", Muscle.Dorsal_R },

            { "bOSC_v1_VestBack_13", Muscle.Lumbar_L },
            { "bOSC_v1_VestBack_14", Muscle.Lumbar_L },
            { "bOSC_v1_VestBack_17", Muscle.Lumbar_L },
            { "bOSC_v1_VestBack_18", Muscle.Lumbar_L },

            { "bOSC_v1_VestBack_15", Muscle.Lumbar_R },
            { "bOSC_v1_VestBack_16", Muscle.Lumbar_R },
            { "bOSC_v1_VestBack_19", Muscle.Lumbar_R },
            { "bOSC_v1_VestBack_20", Muscle.Lumbar_R },
        }.ToImmutableDictionary();

        public static readonly ImmutableDictionary<string, Muscle> MusclesLowercase = MusclesCaptialized
            .ToDictionary(kvp => kvp.Key.ToLowerInvariant(), kvp => kvp.Value)
            .ToImmutableDictionary();

        public static readonly ImmutableDictionary<string, Muscle[]> MuscleGroups = new Dictionary<string, Muscle[]>()
        {
            { "all", Muscle.All },
            { "upperChest", [Muscle.Pectoral_L, Muscle.Pectoral_R] },
            { "frontMuscles", Muscle.Front },
            { "backMuscles", Muscle.Back },
            { "leftMuscles", [Muscle.Arm_L, Muscle.Pectoral_L, Muscle.Dorsal_L, Muscle.Abdominal_L, Muscle.Lumbar_L] },
            { "rightMuscles", [Muscle.Arm_R, Muscle.Pectoral_R, Muscle.Dorsal_R, Muscle.Abdominal_R, Muscle.Lumbar_R] }
        }.ToImmutableDictionary();

        public static Muscle[] GetMusclesFromSensation(Sensation sensation)
        {
            if (sensation is SensationWithMuscles sensationWithMuscles)
            {
                return sensationWithMuscles.muscles;
            }
            if (sensation is BakedSensation bakedSensation && bakedSensation.reference is SensationWithMuscles refSensationWithMuscles)
            {
                return refSensationWithMuscles.muscles;
            }
            return Muscle.All;
        }

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
