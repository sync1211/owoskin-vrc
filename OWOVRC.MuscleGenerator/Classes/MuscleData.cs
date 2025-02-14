using OWOGame;
using System.Reflection;

namespace OWOVRC.MuscleGenerator.Classes
{
    internal static class MuscleData
    {
        public static Dictionary<string, string> GetMusclesFromProperties()
        {
            Console.WriteLine("Loading muscles from OWOGame.Muscle...");
            Dictionary<string, string> muscles = [];

            foreach (FieldInfo prop in typeof(Muscle).GetFields())
            {
                if (!(prop.Name.EndsWith("_L") || prop.Name.EndsWith("_R")))
                {
                    continue;
                }

                if (prop.GetValue(null) is not Muscle muscle)
                {
                    continue;
                }

                Console.WriteLine($"-> {prop.Name}");

                //NOTE: Since we already auto-generate the dictionary, we also create a duplicate entry with the name Shadoki uses
                //      This removes the (admittedly very small) string formatting overhead during lookups.

                string muscleName = $"Muscle.{prop.Name}";
                string nameLower = prop.Name.ToLower();
                string nameShadoki = $"owo_suit_{nameLower}";

                muscles.Add(nameShadoki, muscleName); // Shadoki-compatible muscle name
                muscles.Add(nameLower, muscleName);
            }

            return muscles;
        }
    }
}
