using OWOGame;
using System;
using System.Linq;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper
    {
        public static readonly Dictionary<string, Muscle> Muscles = new()
        {
            { "owo_suit_Pectoral_R",  Muscle.Pectoral_R},
            { "owo_suit_Pectoral_L",  Muscle.Pectoral_L},
            { "owo_suit_Abdominal_R",  Muscle.Abdominal_R},
            { "owo_suit_Abdominal_L",  Muscle.Abdominal_L},
            { "owo_suit_Arm_R",  Muscle.Arm_R},
            { "owo_suit_Arm_L",  Muscle.Arm_L},
            { "owo_suit_Dorsal_R",  Muscle.Dorsal_R},
            { "owo_suit_Dorsal_L",  Muscle.Dorsal_L},
            { "owo_suit_Lumbar_R",  Muscle.Lumbar_R},
            { "owo_suit_Lumbar_L",  Muscle.Lumbar_L}
        };
    }
}
