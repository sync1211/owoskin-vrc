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
        public float VelX { get; private set; } // LEFT/RIGHT (-1 to 1)
        public float VelY { get; private set; } // DOWN/UP (-1 to 1)
        public float VelZ { get; private set; } // BACK/FWD (-1 to 1)
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
            RegisterSensations();

            this.Settings = Settings;
            windSensation = new WindSensation(0.3f);
            impactSensation = new ImpactSensation(0.1f);
        }

        public override void RegisterSensations()
        {
            // Nothing to register
        }

        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Empty message
            if (message.Values.ElementCount == 0)
            {
                //Log.Verbose("Ignoring empty message: {message}", message.Address);
                return;
            }

            // Process OSC message
            if (ProcessMessage(message))
            {
                ProcessSensations();
            }
        }

        private bool ProcessMessage(OSCMessage message)
        {
            // Grounded
            if (message.Address.Equals("Grounded"))
            {
                IsGrounded = message.Values.ReadBooleanElement(0);
                return true;
            }

            // Seated
            if (message.Address.Equals("Seated"))
            {
                IsSeated = message.Values.ReadBooleanElement(0);
                return true;
            }

            // Angular Velocity
            if (message.Address.StartsWith("Angular"))
            {
                //Log.Verbose("Ignoring Angular velocity: This feature is not yet implemented!");
                return true;
            }

            // Velocity
            if (!message.Address.StartsWith("Velocity"))
            {
                return false;
            }

            float value = OSCHelpers.GetFloatValueFromMessage(message);

            switch (message.Address)
            {
                case "VelocityX":
                    lastVelX = VelX;
                    VelX = value;
                    break;
                case "VelocityY":
                    lastVelY = VelY;
                    VelY = value;
                    break;
                case "VelocityZ":
                    lastVelZ = VelZ;
                    VelZ = value;
                    break;
                case "VelocityMagnitude":
                    Speed = value;
                    ProcessStopVelocity();
                    break;
                default:
                    Log.Warning("Unknown velocity component '{message}' with value {value}", message.Address, value);
                    break;
            }
            return true;
        }


        // Sudden stop effect (e.g. hitting the ground after falling)
        /// <summary>
        /// Checks whether the player has stopped moving suddenly and plays an impact sensation.
        /// </summary>
        private void ProcessStopVelocity()
        {
            TimeSpan decelerationDuration = DateTime.Now - LastSpeedPacket;

            if (!Settings.ImpactEnabled || (decelerationDuration >= Settings.StopVelocityTime) || (Speed > 1) || (SpeedLast <= 0))
            {
                LastSpeedPacket = DateTime.Now;
                SpeedLast = Speed;
                return;
            }

            // Calculate stop velocity
            double stopVelocity = Math.Min(SpeedLast, Settings.SpeedCap);
            int velocityPercent = (int)(100 * stopVelocity / Settings.SpeedCap);

            if (stopVelocity < Settings.StopVelocityThreshold)
            {
                LastSpeedPacket = DateTime.Now;
                SpeedLast = Speed;
                return;
            }

            owo.StopSensation(WindSensation._Name);

            // Play impact sensation
            Log.Debug("Stop velocity: {speed}, Time: {time} => {percent}%", SpeedLast, decelerationDuration, velocityPercent);
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
            Log.Debug("Movement speed: {speed} (max {speedCap}) => {intensity}%", Speed, Settings.SpeedCap, speedPercent);

            // Send sensation to vest
            PlayWindSensation(speedPercent);

            LastSpeedPacket = DateTime.Now;
            SpeedLast = Speed;
        }

        private void PlayWindSensation(int intensity)
        {
            float windVelX = VelX;
            float windVelY = VelY * -1;
            float windVelZ = VelZ * -1;

            windSensation.UpdateDirection(windVelX, windVelY, windVelZ);

            windSensation.Intensity = intensity;
            windSensation.Play(owo, Settings.Priority);
        }

        private void PlayStopSensation(int power)
        {
            float hitVelX = lastVelX * -1;
            float hitVelY = lastVelY * -1;
            float hitVelZ = lastVelZ * -1;

            impactSensation.UpdateDirection(hitVelX, hitVelY, hitVelZ);
            impactSensation.Intensity = power;

            impactSensation.Play(owo, Settings.Priority);
            Log.Information($"{owo.GetRunningSensations().ContainsKey(impactSensation.Name)}");
        }

        public override void Reset()
        {
            VelX = 0;
            VelY = 0;
            VelZ = 0;
            Speed = 0;
            SpeedLast = 0;
            LastSpeedPacket = DateTime.MinValue;
            IsGrounded = false;
            IsSeated = false;

            Log.Debug("Velocity effect reset!");
        }
    }
}
