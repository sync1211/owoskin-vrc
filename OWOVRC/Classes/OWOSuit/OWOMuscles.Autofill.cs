using OWOGame;
using System.Reflection;

namespace OWOVRC.Classes.OWOSuit
{
    public static partial class OWOMuscles
    {
        /// <summary>
        /// Populates the "Muscles" dictionary with all muscles from OWOGame.Muscle.
        /// </summary>
        private static void ImportMusclesFromProperties()
        {
            Muscles.Clear();

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

                //NOTE: Since we already auto-generate the dictionary, we also create a duplicate entry with the name Shadoki uses
                //      This removes the (admittedly very small) string formatting overhead during lookups.

                string nameLower = prop.Name.ToLower();
                string nameShadoki = $"owo_suit_{nameLower}";

                Muscles.Add(nameShadoki, muscle); // Shadoki-compatible muscle name
                Muscles.Add(nameLower, muscle);
            }
        }
    }
}
