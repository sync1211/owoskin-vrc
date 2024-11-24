using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class WindSensation
    {
        // Sensation parameters
        private const int frequency = 100;
        public int Intensity
        {
            get
            {
                return intensity * 4;
            }
            set
            {
                intensity = value / 4;
            }
        }
        private int intensity = 25;
        public readonly float Duration;
        public readonly Dictionary<Muscle, int> Muscles;

        // Muscles to apply the sensation
        private static readonly Dictionary<string, Muscle[]> directions = new()
        {
            { "front", OWOHelper.MuscleGroups["frontMuscles"] },
            { "back", OWOHelper.MuscleGroups["backMuscles"] },
            { "left", OWOHelper.MuscleGroups["leftMuscles"] },
            { "right", OWOHelper.MuscleGroups["rightMuscles"] },
            { "up", [Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Dorsal_L, Muscle.Dorsal_L] },
            { "down", [Muscle.Dorsal_L, Muscle.Dorsal_R, Muscle.Lumbar_L, Muscle.Lumbar_R, Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Abdominal_L, Muscle.Abdominal_R] }
        };
        /*
            (For reference)

            
                    Front                            Back
           ------------------------         ------------------------
              Arm_R        Arm_L               


            Pectoral_R  Pectoral_L            Dorsal_R  Dorsal_L
                                              
                                              
            Abdominal_R  Abdominal_L          Lumbar_R  Lumbar_L
        */

        public WindSensation(float durationSeconds = 0.2f)
        {
            Duration = durationSeconds;

            // No direction specified -> front
            Muscles = [];
            Muscle[] frontMuscles = OWOHelper.MuscleGroups["frontMuscles"];

            for (int i = 0; i < frontMuscles.Length; i++)
            {
                Muscle muscle = frontMuscles[i];
                Muscles[muscle] = 100;
            }
        }

        private WindSensation(Dictionary<Muscle, int> muscles, float durationSeconds = 0.2f)
        {
            Duration = durationSeconds;

            Muscles = muscles;
        }

        private MicroSensation CreateSensation(int intensity)
        {
            return SensationsFactory.Create(frequency, Duration, intensity, 0, 0, 0);
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
        /// Creates a sensation from the wind velocity.
        /// IMPORTANT: The wind velocity is the inverse of the velocity provided by VRChat
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// <param name="durationSeconds">duration of the sensation</param></param>
        /// </summary>
        public static WindSensation CreateFromVelocity(float velocityX, float velocityY, float velocityZ, float durationSeconds = 0.2f)
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
                return new WindSensation(muscleValues, durationSeconds);
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

            return new WindSensation(muscleValues, durationSeconds);
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
