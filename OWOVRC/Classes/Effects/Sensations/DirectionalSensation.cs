﻿using OWOGame;
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
        public readonly Dictionary<Muscle, int> MuscleIntensities;
        private readonly Muscle[] musclesScaled = new Muscle[OWOMuscles.MusclesCount];

        // Muscles to apply the sensation
        protected static readonly Dictionary<string, Muscle[]> directions = new()
        {
            { "front", Muscle.Front },
            { "back", Muscle.Back },
            { "left", OWOMuscles.MuscleGroups["leftMuscles"] },
            { "right", OWOMuscles.MuscleGroups["rightMuscles"] },
            { "up", [Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Dorsal_L, Muscle.Dorsal_L] },
            { "down", [Muscle.Abdominal_L, Muscle.Abdominal_R, Muscle.Lumbar_L, Muscle.Lumbar_R] }
        };
        /*
            (For reference)

            
                    Front                            Back
           ------------------------         ------------------------
              Arm_R        Arm_L               


            Pectoral_R  Pectoral_L            Dorsal_R  Dorsal_L
                                              
                                              
            Abdominal_R  Abdominal_L          Lumbar_R  Lumbar_L
        */

        protected DirectionalSensation(string name, int frequency, int intensity, float durationSeconds = 0.2f, float rampUp = 0, float rampDown = 0, float exitDelay = 0, bool loop = false)
        {
            Name = name;
            Frequency = frequency;
            Intensity = intensity;
            Duration = durationSeconds;
            RampUp = rampUp;
            RampDown = rampDown;
            ExitDelay = exitDelay;
            IsLoop = loop;

            // No direction specified -> front
            MuscleIntensities = [];
            Muscle[] frontMuscles = Muscle.Front;

            for (int i = 0; i < frontMuscles.Length; i++)
            {
                Muscle muscle = frontMuscles[i];
                MuscleIntensities[muscle] = 100;
            }
        }

        protected DirectionalSensation(string name, Dictionary<Muscle, int> muscles, int frequency, int intensity, float durationSeconds = 0.2f, float rampUp = 0, float rampDown = 0, float exitDelay = 0, bool loop = false)
        {
            Name = name;
            MuscleIntensities = muscles;
            Frequency = frequency;
            Intensity = intensity;
            Duration = durationSeconds;
            RampUp = rampUp;
            RampDown = rampDown;
            ExitDelay = exitDelay;
            IsLoop = loop;
        }

        private MicroSensation CreateSensation(int intensity)
        {
            return SensationsFactory.Create(Frequency, Duration, intensity, 0, 0, 0);
        }

        public void ApplyMuscleIntensities()
        {
            // Apply intensities
            for (int i = 0; i < MuscleIntensities.Count; i++)
            {
                KeyValuePair<Muscle, int> muscleData = MuscleIntensities.ElementAt(i);
                Muscle muscle = muscleData.Key;
                int muscleIntensity = muscleData.Value;

                musclesScaled[i] = muscle.WithIntensity(muscleIntensity);
            }
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
            float backVelocity = Math.Max(0, velocityZ);
            float leftVelocity = Math.Max(0, velocityX * -1);
            float rightVelocity = Math.Max(0, velocityX);
            float upVelocity = Math.Max(0, velocityY * -1);
            float downVelocity = Math.Max(0, velocityY);

            float[] velocities = [frontVelocity, backVelocity, leftVelocity, rightVelocity, upVelocity, downVelocity];
            float maxVelocity = velocities.Max();

            // 1. Create a dictionary of each muscle and their value (starting at 0)
            Muscle[] muscles = Muscle.All;
            Dictionary<Muscle, int> muscleIntensityScore = [];
            for (int i = 0; i < OWOMuscles.MusclesCount; i++)
            {
                Muscle muscle = muscles[i];
                muscleIntensityScore.Add(muscle, 0);
            }

            if (maxVelocity == 0)
            {
                Log.Debug("maxVelocity is 0! {x} {y} {z}", velocityX, velocityY, velocityZ);
                return;
            }

            // 2. Assign a weight to every direction based on their % of the maximum
            int frontWeight = (int)(frontVelocity / maxVelocity) * 100;
            int backWeight = (int)(backVelocity / maxVelocity) * 100;
            int leftWeight = (int)(leftVelocity / maxVelocity) * 100;
            int rightWeight = (int)(rightVelocity / maxVelocity) * 100;
            int upWeight = (int)(upVelocity / maxVelocity) * 100;
            int downWeight = (int)(downVelocity / maxVelocity) * 100;

            // 3. For each direction, add its weight to the muscles affected by it
            AddMuscleWeights(muscleIntensityScore, "front", frontWeight);
            AddMuscleWeights(muscleIntensityScore, "back", backWeight);
            AddMuscleWeights(muscleIntensityScore, "left", leftWeight);
            AddMuscleWeights(muscleIntensityScore, "right", rightWeight);
            AddMuscleWeights(muscleIntensityScore, "up", upWeight);
            AddMuscleWeights(muscleIntensityScore, "down", downWeight);

            // 4. Convert the muscle values to a % value of the maximum
            int maxMuscleValue = muscleIntensityScore.Values.Max();
            for (int i = 0; i < OWOMuscles.Muscles.Count; i++)
            {
                KeyValuePair<Muscle, int> muscleData = muscleIntensityScore.ElementAt(i);

                Muscle muscle = muscleData.Key;
                int muscleValue = muscleData.Value;

                MuscleIntensities[muscle] = (int)((float)muscleValue / (float)maxMuscleValue) * 100;
            }

            // Apply intensity to muscles
            ApplyMuscleIntensities();
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
