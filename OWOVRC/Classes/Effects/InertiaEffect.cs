using OWOVRC.Classes.Effects.Sensations;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Classes.Effects
{
    public class InertiaEffect: OSCSpeedEffectBase
    {
        private readonly InertiaSensation inertiaSensation;
        public readonly InertiaEffectSettings Settings;

        public InertiaEffect(OWOHelper owo, InertiaEffectSettings settings): base(owo, settings)
        {
            Settings = settings;
            inertiaSensation = new InertiaSensation(0.2f);

            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessInertiaHaptics();
        }

        private void ProcessInertiaHaptics()
        {
            if (!Settings.Enabled || (IsGrounded && Settings.IgnoreWhenGrounded) || (IsSeated && Settings.IgnoreWhenSeated))
            {
                owo.StopSensation(InertiaSensation._Name, true);
                return;
            }

            float deltaX = VelX - LastVelX;
            float deltaY = VelY - LastVelY;
            float deltaZ = VelZ - LastVelZ;
            float deltaSpeed = Speed - LastSpeed;

            LastVelX = VelX;
            LastVelY = VelY;
            LastVelZ = VelZ;
            LastSpeed = Speed;

            bool isAcceleration = (deltaSpeed > 0);

            float deltaSpeedAbs = Math.Abs(deltaSpeed);

            // Check if trigger conditions are met (delta above threshold, respective sign of the speed delta enabled)
            if ((deltaSpeedAbs < Settings.MinDelta) || (isAcceleration && Settings.AccelEnabled) || ((!isAcceleration) && Settings.DecelEnabled))
            {
                return;
            }

            // Calculate intensity
            double speedCapped = Math.Min(deltaSpeedAbs, Settings.MaxDelta);
            int speedPercent = (int)(100 * (speedCapped / Settings.MaxDelta));

            // Create sensation
            inertiaSensation.UpdateDirection(deltaX, deltaY, deltaZ, (int) ((float) speedPercent * (Settings.Intensity / 100f)));

            // Play sensation
            inertiaSensation.Play(owo, Settings.Priority);
        }

        public override void Stop()
        {
            base.Stop();

            // Stop sensation
            owo.StopSensation(InertiaSensation._Name, true);
        }
    }
}
