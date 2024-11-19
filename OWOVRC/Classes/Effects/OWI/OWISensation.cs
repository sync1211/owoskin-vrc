using Newtonsoft.Json;
using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;

namespace OWOVRC.Classes.Effects.OWI
{
    public class OWISensation
    {

        [JsonProperty("priority")]
        public readonly int Priority;
        [JsonProperty("sensation ")]
        public readonly string SensationName = String.Empty;
        [JsonProperty("frequency")]
        public readonly int Frequency;
        [JsonProperty("duration")]
        public readonly float Duration;
        [JsonProperty("intensity")]
        public readonly int Intensity;
        [JsonProperty("rampup")]
        public readonly float Rampup;
        [JsonProperty("rampdown")]
        public readonly float Rampdown;
        [JsonProperty("exitdelay")]
        public readonly float ExitDelay;
        [JsonProperty("Muscles")]
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

            foreach (string muscleName in Muscles.Keys)
            {
                int intensity = Muscles[muscleName];
                Muscle? muscle = OWOHelper.Muscles.GetValueOrDefault(muscleName);

                if (muscle == null)
                {
                    Log.Warning("Muscle not found {muscleName}", muscleName);
                    continue;
                }

                musclesScaled.Append(muscle.Value.WithIntensity(intensity));
            }

            return musclesScaled.ToArray();
        }
    }
}
