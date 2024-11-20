using OWOGame;
using System;
using System.Linq;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper
    {
        public static readonly Dictionary<string, Muscle> Muscles = new()
        {
            { "owo_suit_pectoral_r",  Muscle.Pectoral_R},
            { "owo_suit_pectoral_l",  Muscle.Pectoral_L},
            { "owo_suit_abdominal_r",  Muscle.Abdominal_R},
            { "owo_suit_abdominal_l",  Muscle.Abdominal_L},
            { "owo_suit_arm_r",  Muscle.Arm_R},
            { "owo_suit_arm_l",  Muscle.Arm_L},
            { "owo_suit_dorsal_r",  Muscle.Dorsal_R},
            { "owo_suit_dorsal_l",  Muscle.Dorsal_L},
            { "owo_suit_lumbar_r",  Muscle.Lumbar_R},
            { "owo_suit_lumbar_l",  Muscle.Lumbar_L}
        };

        public static readonly Dictionary<string, Muscle[]> MuscleGroups = new()
        {
            { "frontMuscles", [Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Abdominal_L, Muscle.Abdominal_R] },
            { "backMuscles", [Muscle.Dorsal_L, Muscle.Dorsal_R, Muscle.Lumbar_L, Muscle.Lumbar_R] },
            { "leftMuscles", [Muscle.Arm_L, Muscle.Pectoral_L, Muscle.Dorsal_L, Muscle.Abdominal_L, Muscle.Lumbar_L] },
            { "rightMuscles", [Muscle.Arm_R, Muscle.Pectoral_R, Muscle.Dorsal_R, Muscle.Abdominal_R, Muscle.Lumbar_R] }
        };
    }
}
