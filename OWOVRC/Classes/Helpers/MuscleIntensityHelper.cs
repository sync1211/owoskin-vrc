using OWOGame;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.Classes.Helpers
{
    public static class MuscleIntensityHelper
    {
        public static void AddMissingMuscles(Dictionary<int, int> muscleIntensities)
        {
            Muscle[] muscles = OWOMuscles.MuscleGroups["all"];
            for (int i = 0; i <= muscles.Length; i++)
            {
                // Add value if the key doesn't already exist
                int muscleID = muscles[i].id;
                if (muscleIntensities.ContainsKey(muscleID))
                {
                    continue;
                }

                muscleIntensities[muscleID] = 100;
            }
        }
    }
}
