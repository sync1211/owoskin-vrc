using BuildSoft.OscCore;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Collections.Immutable;

namespace OWOVRC.Classes.Effects
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

        // Callback functions
        private readonly ImmutableDictionary<string, Action<OscMessageValues>> oscCallbacks;

        protected OSCSpeedEffectBase(OWOHelper owo, EffectSettingsBase settings) : base(owo)
        {
            this.settings = settings;
            timer = new System.Timers.Timer()
            {
                Interval = INTERVAL,
                AutoReset = true
            };

            oscCallbacks = new Dictionary<string, Action<OscMessageValues>>()
            {
                { "VelocityX", OnVelocityXMsg },
                { "VelocityY", OnVelocityYMsg },
                { "VelocityZ", OnVelocityZMsg },
                { "Grounded", OnGroundedMsg },
                { "Seated", OnSeatedMsg },
                { "VelocityMagnitude", OnVelocityMagnitudeMsg }
            }.ToImmutableDictionary();
        }


        public override void RegisterCallbacks(OSCReceiver receiver)
        {
            for (int i = 0; i < oscCallbacks.Count; i++)
            {
                KeyValuePair<string, Action<OscMessageValues>> callbackKvp = oscCallbacks.ElementAt(i);
                bool result = receiver.TryAddMessageCallback(callbackKvp.Key, callbackKvp.Value);

                if (!result)
                {
                    Log.Warning("Failed to register OSC callback for {Param}!", callbackKvp.Key);
                }
            }
        }

        public override void UnregisterCallbacks(OSCReceiver receiver)
        {
            for (int i = 0; i < oscCallbacks.Count; i++)
            {
                KeyValuePair<string, Action<OscMessageValues>> callbackKvp = oscCallbacks.ElementAt(i);
                bool result = receiver.TryRemoveMessageCallback(callbackKvp.Key, callbackKvp.Value);

                if (!result)
                {
                    Log.Warning("Failed to unregister OSC callback for {Param}!", callbackKvp.Key);
                }

            }
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
