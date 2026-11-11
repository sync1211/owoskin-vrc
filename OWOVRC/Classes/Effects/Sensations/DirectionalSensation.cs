using OWOGame;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OWOSuit;
using Serilog;

namespace OWOVRC.Classes.Effects.Sensations
{
    public abstract class DirectionalSensation
    {
        public readonly int Frequency;
        public float Duration;
        public readonly bool IsLoop;
        public readonly string Name;
        private readonly Muscle[] musclesScaled = Muscle.All;
        private readonly Sensation sensation;

        // Muscles to apply the sensation
        protected readonly MuscleDirectionGroups directions = new();

        protected DirectionalSensation(string name, int frequency, float durationSeconds = 0.2f, bool loop = false, MuscleDirectionGroups? directions = null)
        {
            Name = name;
            Frequency = frequency;
            Duration = durationSeconds;
            IsLoop = loop;

            this.directions = directions ?? this.directions;

            sensation = CreateSensation(100);

            // No direction specified -> front
            UpdateDirection(0, 0, 0);
        }

        private MicroSensation CreateSensation(int intensity)
        {
            return SensationsFactory.Create(Frequency, Duration, intensity, 0, 0, 0);
        }

        public void Play(OWOHelper owo, int priority = 0)
        {
            Sensation sensationPriority = sensation.WithPriority(priority);

            // Play sensation
            if (!IsLoop)
            {
                owo.AddSensation(Name, sensationPriority, musclesScaled);
            }
            else
            {
                // Run or update sensation loop
                if (owo.GetRunningSensations().ContainsKey(Name))
                {
                    owo.UpdateLoopedSensation(Name, sensationPriority, musclesScaled);
                }
                else
                {
                    owo.AddLoopedSensation(Name, sensationPriority, musclesScaled);
                }
            }
        }

        /// <summary>
        /// Updates the direction of the sensation.
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// </summary>
        private readonly Dictionary<Muscle, float> muscleIntensityScore = []; // OPTIMIZATION: Prevents reallocation on every function call
        public void UpdateDirection(float velocityX, float velocityY, float velocityZ, int intensity = 100)
        {
            // Generate affected muscles dynamically based on direction
            // 1. Assign a weight to every direction based on their % of the maximum
            // 2. Create a dictionary of each muscle and their value (starting at 0)
            // 3. For each direction, add its weight to the muscles affected by it
            // 4. Convert the muscle values to a % value of the maximum

            // 0. Split velocity values into directions
            float frontVelocity = Math.Max(0, velocityZ * -1);
            float backVelocity  = Math.Max(0, velocityZ);
            float leftVelocity  = Math.Max(0, velocityX * -1);
            float rightVelocity = Math.Max(0, velocityX);
            float upVelocity    = Math.Max(0, velocityY * -1);
            float downVelocity  = Math.Max(0, velocityY);

            // Calculate max velocity
            float maxVelocity = MaxHelper.Max(frontVelocity, backVelocity, leftVelocity, rightVelocity, upVelocity, downVelocity);

            if (maxVelocity <= 0)
            {
                Log.Debug("maxVelocity is <= 0! {X} {Y} {Z}", velocityX, velocityY, velocityZ);
                return;
            }

            // 1. Create a dictionary of each muscle and their value (starting at 0)
            //NOTE: Dictionary is reused and zeroed
            for (int i = 0; i < directions.All.Length; i++)
            {
                Muscle key = directions.All[i];
                muscleIntensityScore[key] = 0f;
            }

            // 2. Assign a weight to every direction based on their % of the maximum
            float frontWeight = (frontVelocity / maxVelocity) * 100f;
            float backWeight  = (backVelocity / maxVelocity)  * 100f;
            float leftWeight  = (leftVelocity / maxVelocity)  * 100f;
            float rightWeight = (rightVelocity / maxVelocity) * 100f;
            float upWeight    = (upVelocity / maxVelocity)    * 100f;
            float downWeight  = (downVelocity / maxVelocity)  * 100f;

            // 3. For each direction, add its weight to the muscles affected by it
            AddMuscleWeights(muscleIntensityScore, directions.Front, frontWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Back,  backWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Left,  leftWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Right, rightWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Up,    upWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Down,  downWeight);

            // 4. Convert the muscle values to a % value of the maximum
            float maxMuscleValue = (float) muscleIntensityScore.Values.Max();
            int j = 0;
            foreach (KeyValuePair<Muscle, float> muscleData in muscleIntensityScore)
            {
                Muscle muscle = muscleData.Key;
                float muscleValue = muscleData.Value;

                float muscleIntensity = (muscleValue / maxMuscleValue) * intensity;
                musclesScaled[j] = muscle.WithIntensity((int) muscleIntensity);
                j++;
            }
        }

        private static void AddMuscleWeights(Dictionary<Muscle, float> muscles, Muscle[] directionMuscles, float weight)
        {
            for (int i = 0; i < directionMuscles.Length; i++)
            {
                Muscle muscle = directionMuscles[i];
                muscles[muscle] += weight;
            }
        }
    }
}
