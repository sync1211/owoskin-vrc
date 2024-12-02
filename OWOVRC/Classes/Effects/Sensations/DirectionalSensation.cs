using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;
using Windows.Security.Cryptography.Core;

namespace OWOVRC.Classes.Effects.Sensations
{
    public abstract class DirectionalSensation
    {
        protected int intensity;
        public int Intensity
        {
            get
            {
                return intensity;
            }
            set
            {
                intensity = value;
            }
        }
        public readonly int Frequency;
        public readonly float Duration;
        public readonly float RampUp;
        public readonly float RampDown;
        public readonly float ExitDelay;
        public readonly Dictionary<Muscle, int> Muscles;

        // Muscles to apply the sensation
        protected static readonly Dictionary<string, Muscle[]> directions = new()
        {
            { "front", Muscle.Front },
            { "back", Muscle.Back },
            { "left", OWOHelper.MuscleGroups["leftMuscles"] },
            { "right", OWOHelper.MuscleGroups["rightMuscles"] },
            { "up", [Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Dorsal_L, Muscle.Dorsal_L] },
            { "down", [.. Muscle.Front, ..Muscle.Back] }
        };
        /*
            (For reference)

            
                    Front                            Back
           ------------------------         ------------------------
              Arm_R        Arm_L               


            Pectoral_R  Pectoral_L            Dorsal_R  Dorsal_L
                                              
                                              
            Abdominal_R  Abdominal_L          Lumbar_R  Lumbar_L
        */

        protected DirectionalSensation(int frequency, int intensity, float durationSeconds = 0.2f, float rampUp = 0, float rampDown = 0, float exitDelay = 0)
        {
            Frequency = frequency;
            Intensity = intensity;
            Duration = durationSeconds;
            RampUp = rampUp;
            RampDown = rampDown;
            ExitDelay = exitDelay;

            // No direction specified -> front
            Muscles = [];
            Muscle[] frontMuscles = Muscle.Front;

            for (int i = 0; i < frontMuscles.Length; i++)
            {
                Muscle muscle = frontMuscles[i];
                Muscles[muscle] = 100;
            }
        }

        protected DirectionalSensation(Dictionary<Muscle, int> muscles, int frequency, int intensity, float durationSeconds = 0.2f, float rampUp = 0, float rampDown = 0, float exitDelay = 0)
        {
            Muscles = muscles;
            Frequency = frequency;
            Intensity = intensity;
            Duration = durationSeconds;
            RampUp = rampUp;
            RampDown = rampDown;
            ExitDelay = exitDelay;
        }

        private MicroSensation CreateSensation(int intensity)
        {
            return SensationsFactory.Create(Frequency, Duration, intensity, 0, 0, 0);
        }

        public void Play(OWOHelper owo, int priority = 0)
        {
            // Apply intensities
            Muscle[] musclesScaled = new Muscle[Muscles.Count];
            for (int i = 0; i < Muscles.Count; i++)
            {
                KeyValuePair<Muscle, int> muscleData = Muscles.ElementAt(i);
                Muscle muscle = muscleData.Key;
                int muscleIntensity = muscleData.Value;

                if (muscleIntensity > 0)
                {
                    musclesScaled[i] = muscle.WithIntensity(muscleIntensity);
                }
            }

            Log.Verbose("Playing wind sensation at {0}%", Intensity);
            Sensation sensation = CreateSensation(intensity).WithPriority(priority);
            owo.AddSensation(sensation, musclesScaled);
        }

        /// <summary>
        /// Prepares an array of muscles from a velocity vector and calculates their intensity.
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// </summary>
        protected static Dictionary<Muscle, int> GetMuscleValues(float velocityX, float velocityY, float velocityZ)
        {

            // Generate affected muscles dynamically based on direction
            // 1. Assign a weight to every direction based on their % of the maximum
            // 2. Create a dictionary of each muscle and their value (starting at 0)
            // 3. For each direction, add its weight to the muscles affected by it
            // 4. Convert the muscle values to a % value of the maximum

            // 0. Split velocity values into directions
            float frontVelocity = Math.Max(0, velocityZ * -1);
            float backVelocity = Math.Max(0, velocityZ);
            float leftVelocity = Math.Max(0, velocityX * -1);
            float rightVelocity = Math.Max(0, velocityX);
            float upVelocity = Math.Max(0, velocityY * -1);
            float downVelocity = Math.Max(0, velocityY);

            float[] velocities = [frontVelocity, backVelocity, leftVelocity, rightVelocity, upVelocity, downVelocity];
            float maxVelocity = velocities.Max();

            // 1. Create a dictionary of each muscle and their value (starting at 0)
            Dictionary<Muscle, int> muscleValues = [];
            for (int i = 0; i < OWOHelper.Muscles.Count; i++)
            {
                Muscle muscle = OWOHelper.Muscles.Values.ElementAt(i);
                muscleValues.Add(muscle, 0);
            }

            if (maxVelocity == 0)
            {
                Log.Debug("maxVelocity is 0! {x} {y} {z}", velocityX, velocityY, velocityZ);
                return muscleValues;
            }

            // 2. Assign a weight to every direction based on their % of the maximum
            int frontWeight = (int)(frontVelocity / maxVelocity) * 100;
            int backWeight = (int)(backVelocity / maxVelocity) * 100;
            int leftWeight = (int)(leftVelocity / maxVelocity) * 100;
            int rightWeight = (int)(rightVelocity / maxVelocity) * 100;
            int upWeight = (int)(upVelocity / maxVelocity) * 100;
            int downWeight = (int)(downVelocity / maxVelocity) * 100;

            // 3. For each direction, add its weight to the muscles affected by it
            AddMuscleWeights(muscleValues, "front", frontWeight);
            AddMuscleWeights(muscleValues, "back", backWeight);
            AddMuscleWeights(muscleValues, "left", leftWeight);
            AddMuscleWeights(muscleValues, "right", rightWeight);
            AddMuscleWeights(muscleValues, "up", upWeight);
            AddMuscleWeights(muscleValues, "down", downWeight);

            // 4. Convert the muscle values to a % value of the maximum
            int maxMuscleValue = muscleValues.Values.Max();
            for (int i = 0; i < OWOHelper.Muscles.Count; i++)
            {
                KeyValuePair<Muscle, int> muscleData = muscleValues.ElementAt(i);

                Muscle muscle = muscleData.Key;
                int muscleValue = muscleData.Value;

                muscleValues[muscle] = (int)((float)muscleValue / (float)maxMuscleValue) * 100;
            }

            return muscleValues;
        }

        private static void AddMuscleWeights(Dictionary<Muscle, int> muscles, string directionName, int weight)
        {
            Muscle[] directionMuscles = directions[directionName];
            for (int i = 0; i < directionMuscles.Length; i++)
            {
                Muscle muscle = directionMuscles[i];
                muscles[muscle] += weight;
            }
        }
    }
}
