using OWOGame;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class InertiaSensation : DirectionalSensation
    {
        // Sensation parameters
        public static readonly string _Name = "InertiaSensation";
        private const int _frequency = 100;

        public InertiaSensation(float durationSeconds = 0.2f): base(_Name, _frequency, durationSeconds, 0, 0, 0, true) { }

        public static InertiaSensation CreateFromVelocity(float velocityX, float velocityY, float velocityZ, float durationSeconds = 0.2f)
        {
            InertiaSensation sensation = new(durationSeconds);
            sensation.UpdateDirection(velocityX, velocityY, velocityZ);
            return sensation;
        }
    }
}
