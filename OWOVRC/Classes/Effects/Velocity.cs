using OWOGame;
using OWOVRC.Classes.Effects.Sensations;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public class Velocity : OSCEffectBase
    {
        // OSC Avatar Parameters
        // https://creators.vrchat.com/avatars/animator-parameters/
        public bool IsGrounded { get; private set; }
        public bool IsSeated { get; private set; }
        /// <summary>
        /// LEFT/Right (-1 to 1)
        /// </summary>
        public float VelX { get; private set; }
        /// <summary>
        /// DOWN/UP (-1 to 1)
        /// </summary>
        public float VelY { get; private set; }
        /// <summary>
        /// BACK/FWD (-1 to 1)
        /// </summary>
        public float VelZ { get; private set; }
        // public float VelAngularX { get; private set; } // Not implemented by VRChat
        // public float VelAngularY { get; private set; } // Not relevant to us
        // public float VelAngularZ { get; private set; } // Not implemented by VRChat
        public double Speed { get; private set; }

        // Keep previous values for calculations
        private float lastVelX;
        private float lastVelY;
        private float lastVelZ;
        private double SpeedLast;
        private DateTime LastSpeedPacket;

        // Sensations
        private readonly WindSensation windSensation;
        private readonly ImpactSensation impactSensation;

        // Settings
        public readonly VelocityEffectSettings Settings;

        public Velocity(OWOHelper owo, VelocityEffectSettings Settings): base(owo)
        {
            this.Settings = Settings;
            windSensation = new WindSensation(0.3f);
            impactSensation = new ImpactSensation(0.1f);
        }

        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Process OSC message
            if (ProcessMessage(message))
            {
                //TODO: (Optimization) Use a timer instead of re-creating the sensation on message?
                ProcessSensations();
            }
        }

        private bool ProcessMessage(OSCMessage message)
        {
            switch (message.Address)
            {
                case "VelocityX":
                    lastVelX = VelX;
                    VelX = OSCHelpers.GetFloatValueFromMessage(message); ;
                    break;
                case "VelocityY":
                    lastVelY = VelY;
                    VelY = OSCHelpers.GetFloatValueFromMessage(message); ;
                    break;
                case "VelocityZ":
                    lastVelZ = VelZ;
                    VelZ = OSCHelpers.GetFloatValueFromMessage(message); ;
                    break;
                case "VelocityMagnitude":
                    Speed = OSCHelpers.GetFloatValueFromMessage(message); ;
                    ProcessStopVelocity();
                    break;
                case "Seated":
                    IsSeated = message.Values.ReadBooleanElement(0);
                    break;
                case "Grounded":
                    IsGrounded = message.Values.ReadBooleanElement(0);
                    break;
                default:
                    //Log.Warning("Unknown velocity component '{Message}' with value {Value}", message.Address, value);
                    return false;
            }
            return true;
        }

        // Sudden stop effect (e.g. hitting the ground after falling)
        /// <summary>
        /// Checks whether the player has stopped moving suddenly and plays an impact sensation.
        /// </summary>
        private void ProcessStopVelocity()
        {
            TimeSpan decelerationDuration = DateTime.UtcNow - LastSpeedPacket;

            if (!Settings.ImpactEnabled || (decelerationDuration >= Settings.StopVelocityTime) || (Speed > 1) || (SpeedLast <= 0))
            {
                LastSpeedPacket = DateTime.UtcNow;
                SpeedLast = Speed;
                return;
            }

            // Calculate stop velocity
            double stopVelocity = Math.Min(SpeedLast, Settings.SpeedCap);
            int velocityPercent = (int)(100 * stopVelocity / Settings.SpeedCap);

            if (stopVelocity < Settings.StopVelocityThreshold)
            {
                LastSpeedPacket = DateTime.UtcNow;
                SpeedLast = Speed;
                return;
            }

            owo.StopSensation(WindSensation._Name);

            // Play impact sensation
            Log.Debug("Stop velocity: {Speed}, Time: {Time} => {Percent}%", SpeedLast, decelerationDuration, velocityPercent);
            PlayStopSensation(velocityPercent);

            LastSpeedPacket = DateTime.MinValue;
        }

        private void ProcessSensations()
        {
            bool feedbackEnabled = !IsGrounded                  // Is flying
                || (IsGrounded && !Settings.IgnoreWhenGrounded) // Is not flying (non-grounded setting disabled)
                || (IsSeated && !Settings.IgnoreWhenSeated);    // Is sitting (non-seated setting disabled)

            if (!Settings.Enabled)
            {
                return;
            }

            // Speed too low
            if (Speed < Settings.Threshold)
            {
                //Log.Debug("Speed below threshold: {speed} < {threshold}", Speed, Threshold);

                // Stop sensations
                if (SpeedLast > Settings.Threshold)
                {
                    SpeedLast = -1;
                    lastVelX = 0;
                    lastVelY = 0;
                    lastVelZ = 0;
                    LastSpeedPacket = DateTime.MinValue;
                }

                if (owo.GetRunningSensations().ContainsKey(WindSensation._Name))
                {
                    owo.StopSensation(WindSensation._Name);
                }
                return;
            }

            // Flying only: Ignore velocity if grounded (unless seated and IgnoreWhenSeated is enabled)
            if (!feedbackEnabled)
            {
                Log.Debug("Ignoring grounded velocity.");
                return;
            }

            double speedCapped = Math.Min(Speed, Settings.SpeedCap);
            int speedPercent = (int)(100 * (speedCapped / Settings.SpeedCap));

            // Send sensation to vest
            PlayWindSensation(speedPercent);

            LastSpeedPacket = DateTime.UtcNow;
            SpeedLast = Speed;
        }

        private void PlayWindSensation(int intensity)
        {
            float windVelX = VelX;
            float windVelY = VelY * -1;
            float windVelZ = VelZ * -1;

            windSensation.UpdateDirection(windVelX, windVelY, windVelZ, intensity / 4);

            windSensation.Play(owo, Settings.Priority);
        }

        private void PlayStopSensation(int power)
        {
            float hitVelX = lastVelX * -1;
            float hitVelY = lastVelY * -1;
            float hitVelZ = lastVelZ * -1;

            impactSensation.UpdateDirection(hitVelX, hitVelY, hitVelZ, power);

            impactSensation.Play(owo, Settings.Priority);
        }

        public override void Stop()
        {
            VelX = 0;
            VelY = 0;
            VelZ = 0;
            Speed = 0;
            SpeedLast = 0;
            LastSpeedPacket = DateTime.MinValue;
            IsGrounded = false;
            IsSeated = false;

            owo.StopSensation(WindSensation._Name);
            owo.StopSensation(ImpactSensation._Name);

            Log.Debug("Velocity effect reset!");
        }
    }
}
