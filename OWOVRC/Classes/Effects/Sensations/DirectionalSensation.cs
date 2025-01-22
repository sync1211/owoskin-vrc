using OWOGame;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OWOSuit;
using Serilog;

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
        public float Duration;
        public readonly float RampUp;
        public readonly float RampDown;
        public readonly float ExitDelay;
        public readonly bool IsLoop;
        public readonly string Name;
        private readonly Muscle[] musclesScaled = Muscle.All;

        // Muscles to apply the sensation
        protected readonly MuscleDirectionGroups directions = new();

        protected DirectionalSensation(string name, int frequency, int intensity, float durationSeconds = 0.2f, float rampUp = 0, float rampDown = 0, float exitDelay = 0, bool loop = false, MuscleDirectionGroups? directions = null)
        {
            Name = name;
            Frequency = frequency;
            Intensity = intensity;
            Duration = durationSeconds;
            RampUp = rampUp;
            RampDown = rampDown;
            ExitDelay = exitDelay;
            IsLoop = loop;

            this.directions = directions ?? this.directions;

            // No direction specified -> front
            UpdateDirection(0, 0, 0);
        }

        private MicroSensation CreateSensation(int intensity)
        {
            return SensationsFactory.Create(Frequency, Duration, intensity, 0, 0, 0);
        }

        public void Play(OWOHelper owo, int priority = 0)
        {
            // Play sensation
            Log.Verbose("Playing wind sensation at {0}%", Intensity);
            Sensation sensation = CreateSensation(intensity).WithPriority(priority);

            if (!IsLoop)
            {
                owo.AddSensation(sensation, musclesScaled, Name);
            }
            else
            {
                // Run or update sensation loop
                if (owo.GetRunningSensations().ContainsKey(Name))
                {
                    owo.UpdateLoopedSensation(Name, sensation, musclesScaled);
                }
                else
                {
                    owo.AddLoopedSensation(Name, sensation, musclesScaled);
                }
            }
        }

        /// <summary>
        /// Updates the direction of the sensation.
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// </summary>
        public void UpdateDirection(float velocityX, float velocityY, float velocityZ)
        {
            // Generate affected muscles dynamically based on direction
            // 1. Assign a weight to every direction based on their % of the maximum
            // 2. Create a dictionary of each muscle and their value (starting at 0)
            // 3. For each direction, add its weight to the muscles affected by it
            // 4. Convert the muscle values to a % value of the maximum

            // 0. Split velocity values into directions
            float frontVelocity = Math.Max(0, velocityZ * -1);
            float backVelocity =  Math.Max(0, velocityZ);
            float leftVelocity =  Math.Max(0, velocityX * -1);
            float rightVelocity = Math.Max(0, velocityX);
            float upVelocity =    Math.Max(0, velocityY * -1);
            float downVelocity =  Math.Max(0, velocityY);

            // Calculate max velocity
            float maxVelocity = MaxHelper.Max(frontVelocity, backVelocity, leftVelocity, rightVelocity, upVelocity, downVelocity);
                    
            if (maxVelocity == 0)
            {
                Log.Debug("maxVelocity is 0! {x} {y} {z}", velocityX, velocityY, velocityZ);
                return;
            }

            // 1. Create a dictionary of each muscle and their value (starting at 0)
            Dictionary<Muscle, int> muscleIntensityScore = directions.All
                .ToDictionary(muscle => muscle, _ => 0);

            // 2. Assign a weight to every direction based on their % of the maximum
            int frontWeight = (int)(frontVelocity / maxVelocity) * 100;
            int backWeight  = (int)(backVelocity / maxVelocity) * 100;
            int leftWeight  = (int)(leftVelocity / maxVelocity) * 100;
            int rightWeight = (int)(rightVelocity / maxVelocity) * 100;
            int upWeight    = (int)(upVelocity / maxVelocity) * 100;
            int downWeight  = (int)(downVelocity / maxVelocity) * 100;

            // 3. For each direction, add its weight to the muscles affected by it
            AddMuscleWeights(muscleIntensityScore, directions.Front, frontWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Back, backWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Left, leftWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Right, rightWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Up, upWeight);
            AddMuscleWeights(muscleIntensityScore, directions.Down, downWeight);

            // 4. Convert the muscle values to a % value of the maximum
            int maxMuscleValue = muscleIntensityScore.Values.Max();
            for (int i = 0; i < musclesScaled.Length; i++)
            {
                KeyValuePair<Muscle, int> muscleData = muscleIntensityScore.ElementAt(i);

                Muscle muscle = muscleData.Key;
                int muscleValue = muscleData.Value;

                int intensity = (int)((float)muscleValue / (float)maxMuscleValue) * 100;
                musclesScaled[i] = muscle.WithIntensity(intensity);
            }
        }

        private static void AddMuscleWeights(Dictionary<Muscle, int> muscles, Muscle[] directionMuscles, int weight)
        {
            for (int i = 0; i < directionMuscles.Length; i++)
            {
                Muscle muscle = directionMuscles[i];
                muscles[muscle] += weight;
            }
        }
    }
}
