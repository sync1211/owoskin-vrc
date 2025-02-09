namespace OWOVRC.Classes.Effects.Sensations
{
    public class ImpactSensation: DirectionalSensation
    {
        // Sensation parameters
        public const string _Name = "ImpactSensation";
        private const int _frequency = 100;
        public ImpactSensation(float durationSeconds = 0.1f) : base(_Name, _frequency, durationSeconds) { }

        /// <summary>
        /// Creates a sensation from a velocity vector.
        /// <param name="velocityX">left/right</param>
        /// <param name="velocityY">down/up</param>
        /// <param name="velocityZ">back/front</param></param>
        /// <param name="durationSeconds">duration of the sensation</param></param>
        /// </summary>
        public static ImpactSensation CreateFromVelocity(float velocityX, float velocityY, float velocityZ, float durationSeconds = 0.1f)
        {
            ImpactSensation sensation = new(durationSeconds);
            sensation.UpdateDirection(velocityX, velocityY, velocityZ);
            return sensation;
        }
    }
}
