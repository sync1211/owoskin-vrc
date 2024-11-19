using OWOGame;
using OWOVRC.Classes.Effects.Builders;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public class Velocity : OSCEffectBase
    {
        private readonly OWOHelper owo;

        // OSC Addresses
        private const string ADDRESS_VEL_X = "VelocityX";
        private const string ADDRESS_VEL_Y = "VelocityY";
        private const string ADDRESS_VEL_Z = "VelocityZ";
        private const string ADDRESS_VEL_SPEED = "VelocityMagnitude";
        private const string ADDRESS_SEATED = "Seated";
        private const string ADDRESS_GROUNDED = "Grounded";

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

        // Calculated Values
        public double Speed { get; private set; }
        private double SpeedLast;
        private DateTime LastSpeedPacket;

        // Sensation duration
        public float SensationDuration = 0.3f;

        // Settings
        public readonly VelocityEffectSettings Settings;

        // Timer
        private readonly System.Timers.Timer timer;

        public Velocity(OWOHelper owo, VelocityEffectSettings Settings)
        {
            this.owo = owo;
            this.Settings = Settings;
            timer = new System.Timers.Timer
            {
                Interval = SensationDuration * 1000
            };
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessSensations();
        }

        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            ProcessMessage(message);
        }

        private void ProcessMessage(OSCMessage message)
        {
            // Empty message
            if (message.Values.ElementCount == 0)
            {
                Log.Verbose("Ignoring empty message: {message}", message.Address);
                return;
            }

            // Grounded
            if (message.Address.Equals(ADDRESS_GROUNDED))
            {
                IsGrounded = message.Values.ReadBooleanElement(0);
                return;
            }

            // Seated
            if (message.Address.Equals(ADDRESS_SEATED))
            {
                IsSeated = message.Values.ReadBooleanElement(0);
                return;
            }

            // Angular Velocity
            if (message.Address.StartsWith("Angular"))
            {
                //Log.Verbose("Ignoring Angular velocity: This feature is not yet implemented!");
                return;
            }

            // Velocity
            if (!message.Address.StartsWith("Velocity"))
            {
                Log.Verbose("Ignoring non-velocity message: {message}", message.Address);
                return;
            }

            float value = message.Values.ReadFloatElement(0);

            switch (message.Address)
            {
                case ADDRESS_VEL_X:
                    VelX = value;
                    break;
                case ADDRESS_VEL_Y:
                    VelY = value;
                    break;
                case ADDRESS_VEL_Z:
                    VelZ = value;
                    break;
                case ADDRESS_VEL_SPEED:
                    Speed = value;
                    break;
                default:
                    Log.Warning("Unknown velocity component '{message}' with value {value}", message.Address, value);
                    break;
            }

            // Sudden stop effect (e.g. hitting the ground after falling)
            TimeSpan stoppingTime = DateTime.Now - LastSpeedPacket;
            if (Settings.ImpactEnabled && stoppingTime < Settings.StopVelocityTime && Speed <= 1 && SpeedLast > 0)
            {
                double stopVelocity = Math.Min(SpeedLast, Settings.SpeedCap);
                int velocityPercent = (int)(100 * stopVelocity / Settings.SpeedCap);

                if (stopVelocity >= Settings.StopVelocityThreshold)
                {
                    owo.StopAllSensations();
                    Log.Debug("Stop velocity: {speed}, Time: {time} => {percent}%", SpeedLast, stoppingTime, velocityPercent);
                    Sensation stopSensation = CreateStopSensation(velocityPercent);
                    owo.AddSensation(stopSensation);
                    LastSpeedPacket = DateTime.MinValue;
                    return;
                }
            }

            if (message.Address.Equals(ADDRESS_VEL_SPEED))
            {
                LastSpeedPacket = DateTime.Now;
                SpeedLast = Speed;
            }
        }

        private void ProcessSensations()
        {
            bool feedbackEnabled = !IsGrounded                  // Is flying
                || (IsGrounded && !Settings.IgnoreWhenGrounded) // Is not flying (non-grounded setting disabled)
                || (IsSeated && !Settings.IgnoreWhenSeated);    // Is sitting (non-seated setting disabled)

            if (!owo.IsConnected || !Settings.Enabled)
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
                    LastSpeedPacket = DateTime.MinValue;
                    owo.StopAllSensations();
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

            // Send senstations to vest
            WindSensation windSensation = CreateWindSensation();
            windSensation.Intensity = speedPercent;
            windSensation.Play(owo, Settings.Priority);

            LastSpeedPacket = DateTime.Now;
            SpeedLast = Speed;
        }

        private WindSensation CreateWindSensation()
        {
            float windVelX = VelX * -1;
            float windVelY = VelY * -1;
            float windVelZ = VelZ * -1;
            return WindSensation.CreateFromVelocity(windVelX, windVelY, windVelZ, SensationDuration);
        }

        private Sensation CreateStopSensation(int power)
        {
            //TODO: Make this directional!
            return owo.Sensations.FallDmg.MultiplyIntensityBy(power / 100).WithPriority(Settings.StopPriority);
        }
    }
}
