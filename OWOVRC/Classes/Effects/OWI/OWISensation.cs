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
        [JsonPropertyName("Muscles")]
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

        private float GetScaledIntensity(int muscleID, Dictionary<int, int> muscleIntensities)
        {
            if (muscleIntensities.TryGetValue(muscleID, out int scaledIntensity))
            {
                return (Math.Min(Intensity, 100) / 100f) * scaledIntensity;
            }
            return Intensity;
        }

        public Muscle[] GetMusclesWithIntensity(Dictionary<int, int> muscleIntensities, int maxSensationIntensity)
        {
            List<Muscle> musclesScaled = [];

            //NOTE: Intensity is applied like this:
            // * Intensity from the received sensation is scaled by the muscle intensity setting
            // * The result from this is then again scaled by the maximum intensity allowed for this sensation

            // Apply intensity for each muscle
            for (int i = 0; i < Muscles.Count; i++)
            {
                KeyValuePair<string, int> muscleData = Muscles.ElementAt(i);
                string muscleName = muscleData.Key;

                // Get Single muscle
                if (OWOMuscles.MusclesLowercase.TryGetValue(muscleName.ToLower(), out Muscle muscle))
                {
                    int intensity = (int) Math.Round((GetScaledIntensity(muscle.id, muscleIntensities) / 100f) * maxSensationIntensity);
                    musclesScaled.Add(muscle.WithIntensity(intensity));
                }

                // Get Muscle group
                else if (OWOMuscles.MuscleGroups.TryGetValue(muscleName, out Muscle[]? muscles))
                {
                    for (int j = 0; j < muscles.Length; j++)
                    {
                        int intensity = (int) Math.Round((GetScaledIntensity(muscle.id, muscleIntensities) / 100f) * maxSensationIntensity);
                        musclesScaled.Add(muscles[j].WithIntensity(intensity));
                    }
                }

                // Not found
                else
                {
                    Log.Warning("Muscle not found {MuscleName}", muscleName);
                }
            }

            return musclesScaled.ToArray();
        }
    }
}
