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

        public void ProcessMessage(OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            try
            {
                bool value = message.Values.ReadBooleanElement(0); //TODO: Support floats for variable intensity
                if (!value)
                {
                    return;
                }
            }
            catch (InvalidOperationException)
            {
                Log.Error("Unable to parse OSC message at {address} as boolean value!", message.Address);
            }

            string presetName = message.Address.Substring(OSC_ADDRESS_PREFIX.Length);
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
                .MultiplyIntensityBy((Multiplier) intensity);

            owo.AddSensation(sensation, []);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
