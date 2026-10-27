using BuildSoft.OscCore;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Collections.Immutable;


namespace OWOVRC.Classes.Effects
{
    public class OSCControl: OSCEffectBase
    {
        private const string PREFIX = "OWOVRC/"

        private readonly ImmutableDictionary<string, Action<OscMessageValues>> oscCallbacks;

        private readonly OSCControlSettings settings;
        //TODO: Add other settings for controlling other parts of the program
        //TODO: Implement ways to control other effects
        //TODO: Create a package for this!

        public OSCControl(OWOHelper owo, OSCControlSettings settings) : base(owo)
        {
            this.settings = settings;
            //TODO: Add more settings 

            oscCallbacks = new Dictionary<string, Action<OscMessageValues>>()
            {
                { "OWO/Triggers/StopAll", Action_StopAllSensations }
            }.ToImmutableDictionary();
        }


        // Actions
        //NOTE: Need to be registered in oscCallbacks above
        private void Action_StopAllSensations(OscMessageValues values)
        {
            if (!values.ReadBooleanElement(0))
            {
                return; // Only perform action when activating this trigger
            }

            owo.StopAllSensations();
        }


        // End Actions


        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override void RegisterCallbacks(OSCReceiver receiver)
        {
            if (!settings.Enabled)
            {
                return;
            }

            foreach (KeyValuePair<string, Action<OscMessageValues>> callbackKvp in oscCallbacks)
            {
                bool result = receiver.TryAddMessageCallback(callbackKvp.Key, callbackKvp.Value);

                if (result)
                {
                    Log.Debug("Registered OSC callback for {Param}.", callbackKvp.Key);
                }
                else
                {
                    Log.Warning("Failed to register OSC callback for {Param}!", callbackKvp.Key);
                }
            }
        }

        public override void Stop()
        {
            // Does not do anything that needs to be stopped
        }

        public override void UnregisterCallbacks(OSCReceiver receiver)
        {
            if (!settings.Enabled)
            {
                return;
            }

            foreach (KeyValuePair<string, Action<OscMessageValues>> callbackKvp in oscCallbacks)
            {
                bool result = receiver.TryAddMessageCallback(callbackKvp.Key, callbackKvp.Value);

                if (result)
                {
                    Log.Debug("Registered OSC callback for {Param}.", callbackKvp.Key);
                }
                else
                {
                    Log.Warning("Failed to register OSC callback for {Param}!", callbackKvp.Key);
                }
            }
        }
    }
}
