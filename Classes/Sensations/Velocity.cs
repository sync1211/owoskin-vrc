using System.Numerics;
using OWOGame;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using Serilog;

namespace OWOVRC.Classes.Sensations
{
    public class Velocity(OWOHelper owo) : OSCSensationBase
    {
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
        //TODO: Implement rotational velocity. (Not enough coffee to look up the math right now. Will revisit later.)
        // public float VelAngularX { get; private set; } // Not implemented by VRChat
        // public float VelAngularY { get; private set; }
        // public float VelAngularZ { get; private set; } // Not implemented by VRChat

        // Calculated Values
        public double Speed { get;  private set; }
        private double SpeedLast;
        private DateTime LastSpeedPacket;
        public Vector3 Direction { get; private set; }

        // Settings
        public double Threshold { get; set; } = 6;
        public double StopVelocityThreshold = 10;
        public double SpeedCap { get; set; } = 200.0;
        public bool IgnoreWhenGrounded { get; set; }
        public bool IgnoreWhenSeated { get; set; }
        public TimeSpan StopVelocityTime = TimeSpan.FromSeconds(1);
        public int Frequency = 50;

        private double Normalize(float x, float y, float z)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        private Vector3 GetDirectionVector()
        {
            // Generate a vector from the 3 velocity components
            return new Vector3(VelX, VelY, VelZ);
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
                Log.Verbose("Ignoring Angular velocity: This feature is not yet implemented!");
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

            // Update
            Update();
            ProcessSensations();

            if (message.Address == ADDRESS_VEL_SPEED)
            {
                LastSpeedPacket = DateTime.Now;
                SpeedLast = Speed;
            }
        }

        private void Update()
        {
            Direction = GetDirectionVector();
        }

        private void ProcessSensations()
        {
            bool feedbackEnabled = (!IsGrounded)       // Is flying
                || (IsGrounded && !IgnoreWhenGrounded) // Is not flying (non-grounded setting disabled)
                || (IsSeated && !IgnoreWhenSeated);    // Is sitting (non-seated setting disabled)

            // Sudden stop effect (e.g. hitting the ground after falling)
            TimeSpan stoppingTime = DateTime.Now - LastSpeedPacket;
            if (feedbackEnabled && (stoppingTime < StopVelocityTime) && Speed <= 1 && SpeedLast > 0)
            {
                double stopVelocity = Math.Min(SpeedLast, SpeedCap);
                int velocityPercent = (int)(100 * stopVelocity / SpeedCap);

                if (stopVelocity >= StopVelocityThreshold)
                {
                    Log.Information("Stop velocity: {speed}, Time: {time} => {percent}%", SpeedLast, stoppingTime, velocityPercent);
                    Sensation stopSensation = CreateStopSensation(velocityPercent);
                    owo.AddSensation(stopSensation);
                    return;
                }
            }

            // Speed too low
            if (Speed < Threshold)
            {
                //Log.Debug("Speed below threshold: {speed} < {threshold}", Speed, Threshold);
                return;
            }

            // Flying only: Ignore velocity if grounded (unless seated and IgnoreWhenSeated is enabled)
            if (!feedbackEnabled)
            {
                Log.Debug("Ignoring grounded velocity.");
                return;
            }

            double speedCapped = Math.Min(Speed, SpeedCap);
            int speedPercent = (int)(100 * speedCapped / SpeedCap);
            Log.Information("Movement speed: {speedCapped} ({speed}) => {intensity}%", speedCapped, Speed, speedPercent);

            // Send senstations to vest
            //throw new NotImplementedException(); //TODO: Implement me!
            Sensation sensation = CreateSensation(speedPercent);
            owo.AddSensation(sensation);

            LastSpeedPacket = DateTime.Now;
            SpeedLast = Speed;
        }

        private Sensation CreateSensation(int speed)
        {
            int speedScaled = (int)(50 * (speed / 100.0)); // Temporary until I figure out how to play the wind sensation
            return SensationsFactory.Create(Frequency, 0.3f, speedScaled, 0, 0, 0);
        }

        private Sensation CreateStopSensation(int power)
        {
            return Sensation.Ball;
        }
    }
}
