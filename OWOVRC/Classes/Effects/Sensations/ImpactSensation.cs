namespace OWOVRC.Classes.Effects.Sensations
{
    public class ImpactSensation: DirectionalSensation
    {
        // Sensation parameters
        private const string _Name = "ImpactSensation";
        private const int _frequency = 100;
        private const int _intensity = 100;
        public ImpactSensation(float durationSeconds = 0.2f) : base(_Name, _frequency, _intensity, durationSeconds) { }

        /// <summary>
        /// Creates a sensation from a velocity vector.
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// <param name="durationSeconds">duration of the sensation</param></param>
        /// </summary>
        public static ImpactSensation CreateFromVelocity(float velocityX, float velocityY, float velocityZ, float durationSeconds = 0.2f)
        {
            ImpactSensation sensation = new(durationSeconds);
            sensation.UpdateDirection(velocityX, velocityY, velocityZ);
            return sensation;
        }
    }
}
