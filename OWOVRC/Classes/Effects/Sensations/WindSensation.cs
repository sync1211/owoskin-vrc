﻿using OWOGame;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class WindSensation: DirectionalSensation
    {
        // Sensation parameters
        public static readonly string _Name = "WindSensation";
        private const int _frequency = 100;

        public WindSensation(float durationSeconds = 0.2f): base(_Name, _frequency, durationSeconds, 0, 0, 0, true, new MuscleDirectionGroups(down: Muscle.All)) { }

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
            WindSensation sensation = new(durationSeconds);
            sensation.UpdateDirection(velocityX, velocityY, velocityZ);
            return sensation;
        }
    }
}
