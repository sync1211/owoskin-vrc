using OWOGame;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class InertiaSensation : DirectionalSensation
    {
        // Sensation parameters
        public static readonly string _Name = "InertiaSensation";
        private const int _frequency = 100;

        public InertiaSensation(float durationSeconds = 0.2f): base(name: _Name, frequency: _frequency, durationSeconds: durationSeconds, loop: false) { }

        public static InertiaSensation CreateFromVelocity(float velocityX, float velocityY, float velocityZ, float durationSeconds = 0.2f)
        {
            InertiaSensation sensation = new(durationSeconds);
            sensation.UpdateDirection(velocityX, velocityY, velocityZ);
            return sensation;
        }
    }
}
