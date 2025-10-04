using BuildSoft.OscCore;
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


        public override void RegisterCallbacks(OSCReceiver receiver)
        {
            bool onVelocityXMsgSuccess = receiver.TryAddMessageCallback("VelocityX", OnVelocityXMsg);
            bool onVelocityYMsgSuccess = receiver.TryAddMessageCallback("VelocityY", OnVelocityYMsg);
            bool onVelocityZMsgSuccess = receiver.TryAddMessageCallback("VelocityZ", OnVelocityZMsg);
            bool onGroundedMsgSuccess = receiver.TryAddMessageCallback("Grounded", OnGroundedMsg);
            bool onSeatedMsgSuccess = receiver.TryAddMessageCallback("Seated", OnSeatedMsg);
            bool onVelocityMagnitudeMsgSuccess = receiver.TryAddMessageCallback("VelocityMagnitude", OnVelocityMagnitudeMsg);
        }

        public override void UnregisterCallbacks(OSCReceiver receiver)
        {
            receiver.TryRemoveMessageCallback("VelocityX", OnVelocityXMsg);
            receiver.TryRemoveMessageCallback("VelocityY", OnVelocityYMsg);
            receiver.TryRemoveMessageCallback("VelocityZ", OnVelocityZMsg);
            receiver.TryRemoveMessageCallback("Grounded", OnGroundedMsg);
            receiver.TryRemoveMessageCallback("Seated", OnSeatedMsg);
            receiver.TryRemoveMessageCallback("VelocityMagnitude", OnVelocityMagnitudeMsg);
        }

        private void OnVelocityXMsg(OscMessageValues values)
        {
            VelX = OSCHelpers.GetFloatValueFromMessageValues(values);
        }

        private void OnVelocityYMsg(OscMessageValues values)
        {
            VelY = OSCHelpers.GetFloatValueFromMessageValues(values);
        }

        private void OnVelocityZMsg(OscMessageValues values)
        {
            VelZ = OSCHelpers.GetFloatValueFromMessageValues(values);
        }

        private void OnGroundedMsg(OscMessageValues values)
        {
            IsGrounded = values.ReadBooleanElement(0);
        }

        private void OnSeatedMsg(OscMessageValues values)
        {
            IsSeated = values.ReadBooleanElement(0);
        }

        private void OnVelocityMagnitudeMsg(OscMessageValues values)
        {
            Speed = OSCHelpers.GetFloatValueFromMessageValues(values);
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
