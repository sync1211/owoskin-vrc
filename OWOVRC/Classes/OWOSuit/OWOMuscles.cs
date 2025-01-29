using OWOGame;

namespace OWOVRC.Classes.OWOSuit
{
    public static partial class OWOMuscles
    {
        public static readonly Dictionary<string, Muscle> Muscles = new()
        {
            //NOTE: Example muscle values!
            //      Muscles are populated automatically at runtime to be up to date with the OWOGame package.
            //      See AddMusclesFromProperties() in OWOMuscles.Autofill.cs for more information.
            { "owo_suit_pectoral_r",  Muscle.Pectoral_R }, // Shadoki-compatible muscle name
            { "pectoral_l",  Muscle.Pectoral_L }           // Normal muscle name (used by WorldIntegrator)
        };

        static OWOMuscles()
        {
            // Populate Muscles dictionary
            ImportMusclesFromProperties();
        }

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
    }
}
