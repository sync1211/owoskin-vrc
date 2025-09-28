using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;

namespace OWOVRC.Classes.Effects.Sensations
{
    public abstract class OSCSpeedEffectBase: OSCEffectBase
    {
        // OSC Avatar Parameters
        // https://creators.vrchat.com/avatars/animator-parameters/
        /// <summary>
        /// LEFT/Right (-1 to 1)
        /// </summary>
        public float VelX { get; protected set; }
        public float LastVelX { get; protected set; }
        /// <summary>
        /// DOWN/UP (-1 to 1)
        /// </summary>
        public float VelY { get; protected set; }
        public float LastVelY { get; protected set; }
        /// <summary>
        /// BACK/FWD (-1 to 1)
        /// </summary>
        public float VelZ { get; protected set; }
        public float Speed { get; protected set; }
        public float LastSpeed { get; protected set; }
        public float LastVelZ { get; protected set; }
        public bool IsGrounded { get; protected set; }
        public bool IsSeated { get; protected set; }

        // Timer
        protected const int INTERVAL = 100;
        protected readonly System.Timers.Timer timer;

        // Settings
        protected readonly EffectSettingsBase settings;

        protected OSCSpeedEffectBase(OWOHelper owo, EffectSettingsBase settings) : base(owo)
        {
            this.settings = settings;
            timer = new System.Timers.Timer()
            {
                Interval = INTERVAL,
                AutoReset = true
            };
        }


        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            if (!settings.Enabled)
            {
                return;
            }

            switch (message.Address)
            {
                case "VelocityX":
                    VelX = OSCHelpers.GetFloatValueFromMessage(message);
                    break;
                case "VelocityY":
                    VelY = OSCHelpers.GetFloatValueFromMessage(message);
                    break;
                case "VelocityZ":
                    VelZ = OSCHelpers.GetFloatValueFromMessage(message);
                    break;
                case "Grounded":
                    IsGrounded = message.Values.ReadBooleanElement(0);
                    break;
                case "Seated":
                    IsSeated = message.Values.ReadBooleanElement(0);
                    break;
                case "VelocityMagnitude":
                    Speed = OSCHelpers.GetFloatValueFromMessage(message);
                    break;
            }
        }

        public override void Stop()
        {
            VelX = 0;
            VelY = 0;
            VelZ = 0;

            LastVelX = 0;
            LastVelY = 0;
            LastVelZ = 0;

            Speed = 0;
            LastSpeed = 0;
        }
    }
}
