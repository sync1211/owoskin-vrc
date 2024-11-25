using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Effects.OWI
{
    public class OWISensation
    {
        [JsonPropertyName("priority")]
        public int Priority { get; }
        [JsonPropertyName("sensation")]
        public string Sensation { get; }
        [JsonPropertyName("frequency")]
        public int Frequency { get; }
        [JsonPropertyName("duration")]
        public float Duration { get; }
        [JsonPropertyName("intensity")]
        public int Intensity { get; }
        [JsonPropertyName("rampup")]
        public float Rampup { get; }
        [JsonPropertyName("rampdown")]
        public float Rampdown { get; }
        [JsonPropertyName("exitdelay")]
        public float ExitDelay { get; }
        [JsonPropertyName("muscles")]
        public Dictionary<string, int> Muscles { get; }

        [JsonConstructor]
        public OWISensation(int priority, string sensation, int frequency, float duration, int intensity, float rampup, float rampdown, float exitdelay, Dictionary<string, int> muscles)
        {
            Priority = priority;
            Sensation = sensation;
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
            for (int i = 0; i < Muscles.Count; i++)
            {
                KeyValuePair<string, int> muscleData = Muscles.ElementAt(i);
                string muscleName = muscleData.Key;
                int intensity = muscleData.Value;
                string muscleKey = $"owo_suit_{muscleName.ToLower()}";

                // Single muscle
                if (OWOHelper.Muscles.TryGetValue(muscleKey, out Muscle muscle))
                {
                    musclesScaled.Add(muscle.WithIntensity(intensity));
                }

                // Muscle group
                else if (OWOHelper.MuscleGroups.TryGetValue(muscleName, out Muscle[]? muscles))
                {
                    for (int j = 0; j < muscles.Length; j++)
                    {
                        musclesScaled.Add(muscles[j].WithIntensity(intensity));
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
