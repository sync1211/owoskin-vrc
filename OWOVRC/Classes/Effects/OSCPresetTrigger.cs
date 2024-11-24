using BuildSoft.OscCore;
using OWOGame;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public class OSCPresetTrigger: OSCEffectBase
    {
        private const string OSC_ADDRESS_PREFIX = "OWO/SensationsTrigger/";
        public readonly OSCPresetsSettings Settings;

        public OSCPresetTrigger(OWOHelper owo, OSCPresetsSettings settings): base(owo)
        {
            Settings = settings;
        }

        public void AddPreset(OSCSensationPreset preset)
        {
            Settings.Presets.Add(preset.Name, preset);
        }

        public void RemovePreset(string name)
        {
            Settings.Presets.Remove(name);
        }

        public void ClearPresets()
        {
            Settings.Presets.Clear();
        }

        public override void RegisterSensations()
        {
            for (int i = 0; i < Settings.Presets.Count; i++)
            {
                OSCSensationPreset preset = Settings.Presets.Values.ElementAt(i);

                Log.Debug("Registering custom sensation {name}...", preset.Name);
                owo.AddBakedSensation(preset.SensationObject);
            }
        }

        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            ProcessMessage(message);
        }

        private float GetIntensityFromMessage(OSCMessage message)
        {
            // Float
            try
            {
                return message.Values.ReadFloatElement(0);
            }
            catch (InvalidOperationException)
            {
                Log.Debug("Unable to parse OSC message as float value!");
            }

            // Bool
            try
            {
                if (message.Values.ReadBooleanElement(0))
                {
                    return 1;
                }
            }
            catch (InvalidOperationException)
            {
                Log.Error("Unable to parse OSC message at {address} as boolean value!", message.Address);
            }

            return 0;
        }

        public void ProcessMessage(OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Get intensity
            float intensityMultiplier = GetIntensityFromMessage(message);
            if (intensityMultiplier == 0)
            {
                return;
            }

            // Get preset name
            string presetName = message.Address.Substring(OSC_ADDRESS_PREFIX.Length);

            // Get preset
            if (!Settings.Presets.TryGetValue(presetName, out OSCSensationPreset? preset) || preset == null)
            {
                return;
            }

            if (!preset.Enabled)
            {
                Log.Debug("Disabled preset {presetName} called!", preset.Name);
                return;
            }

            Log.Debug("Triggering preset {presetName}!", preset.Name);
            float intensity = (float)preset.Intensity / 100;
            Sensation sensation = preset.SensationObject
                .MultiplyIntensityBy((Multiplier) intensity)             // Multiplier from preset settings
                .MultiplyIntensityBy((Multiplier) intensityMultiplier);  // Multiplier from OSC message

            owo.AddSensation(sensation, []);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
