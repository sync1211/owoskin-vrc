using OWOGame;
using OWOVRC.Classes.OWOSuit;
using Serilog;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class WindSensation: DirectionalSensation
    {
        // Sensation parameters
        private const int _frequency = 100;
        private const int _intensity = 25;
        new public int Intensity
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

        public WindSensation(float durationSeconds = 0.2f): base(_frequency, _intensity, durationSeconds) { }

        public WindSensation(Dictionary<Muscle, int> muscles, float durationSeconds = 0.2f): base(muscles, _frequency, _intensity / 4, durationSeconds) { }

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
            return new WindSensation(GetMuscleValues(velocityX, velocityY, velocityZ), durationSeconds);
        }
    }
}
