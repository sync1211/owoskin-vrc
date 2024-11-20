using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Effects.OWI
{
    public class OWISensation
    {
        [JsonPropertyName("priority")]
        public readonly int Priority;
        [JsonPropertyName("sensation ")]
        public readonly string SensationName = String.Empty;
        [JsonPropertyName("frequency")]
        public readonly int Frequency;
        [JsonPropertyName("duration")]
        public readonly float Duration;
        [JsonPropertyName("intensity")]
        public readonly int Intensity;
        [JsonPropertyName("rampup")]
        public readonly float Rampup;
        [JsonPropertyName("rampdown")]
        public readonly float Rampdown;
        [JsonPropertyName("exitdelay")]
        public readonly float ExitDelay;
        [JsonPropertyName("Muscles")]
        public readonly Dictionary<string, int> Muscles = [];

        [JsonConstructor]
        public OWISensation(int priority, string sensation, int frequency, float duration, int intensity, float rampup, float rampdown, float exitdelay, Dictionary<string, int> muscles)
        {
            Priority = priority;
            SensationName = sensation;
            Frequency = frequency;
            Duration = duration;
            Intensity = intensity;
            Rampup = rampup;
            Rampdown = rampdown;
            ExitDelay = exitdelay;
            Muscles = muscles;
        }

        public Sensation AsSensation()
        {
            return SensationsFactory.Create(Frequency, Duration, Intensity, Rampup, Rampdown, ExitDelay);
        }

        public Muscle[] GetMusclesWithIntensity()
        {
            List<Muscle> musclesScaled = [];

            // Apply intensity for each muscle
            foreach (string muscleName in Muscles.Keys)
            {
                int intensity = Muscles[muscleName];
                string muscleKey = $"owo_suit_{muscleName.ToLower()}";

                // Single muscle
                if (OWOHelper.Muscles.ContainsKey(muscleKey))
                {
                    musclesScaled.Add(OWOHelper.Muscles[muscleKey].WithIntensity(intensity));
                }

                // Muscle group
                else if (OWOHelper.MuscleGroups.ContainsKey(muscleName))
                {
                    foreach (Muscle muscle in OWOHelper.MuscleGroups[muscleName])
                    {
                        musclesScaled.Add(muscle.WithIntensity(intensity));
                    }
                }

                // Not found
                else
                {
                    Log.Warning("Muscle not found {muscleName}", muscleName);
                }
            }

            return musclesScaled.ToArray();
        }
    }
}
