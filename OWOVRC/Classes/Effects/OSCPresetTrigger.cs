using OWOGame;
using OWOVRC.Classes.Effects.OSCPresets;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public class OSCPresetTrigger : OSCEffectBase
    {
        private const string OSC_ADDRESS_PREFIX = "OWO/SensationsTrigger/";
        public readonly OSCPresetsSettings Settings;

        public OSCPresetTrigger(OWOHelper owo, OSCPresetsSettings settings) : base(owo)
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

        public OSCSensationPreset? GetPresetFromMessage(OSCMessage message, out Muscle[] muscles)
        {
            muscles = [];
            if (!message.Address.StartsWith(OSC_ADDRESS_PREFIX) || message.Address.Length <= (OSC_ADDRESS_PREFIX.Length + 1))
            {
                return null;
            }

            string presetString = message.Address.Substring(OSC_ADDRESS_PREFIX.Length);
            string muscleGroupName = String.Empty;
            string presetName = presetString;

            // Check if preset contains muscle information
            if (presetString.Contains('/'))
            {
                string[] split = presetString.Split('/');
                presetName = split[0];
                muscleGroupName = split[1];
            }

            // Get preset
            if (!Settings.Presets.TryGetValue(presetName, out OSCSensationPreset? preset) || preset == null)
            {
                Log.Warning("Preset {presetName} not found!", presetName);
                return null;
            }

            // Get muscles
            if (OWOMuscles.MuscleGroups.TryGetValue(muscleGroupName, out Muscle[]? muscleGroup))
            {
                muscles = muscleGroup;
            }
            else if (OWOMuscles.Muscles.TryGetValue($"owo_suit_{muscleGroupName}", out Muscle muscle))
            {
                muscles = [muscle];
            }
            else
            {
                muscles = Muscle.All;
            }

            return preset;
        }

        public void ProcessMessage(OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Get intensity
            float oscIntensity = OSCHelpers.GetFloatValueFromMessage(message);
            if (oscIntensity == 0)
            {
                return;
            }

            // Get preset
            OSCSensationPreset? preset = GetPresetFromMessage(message, out Muscle[] muscles);
            if (preset == null)
            {
                return;
            }

            if (!preset.Enabled)
            {
                Log.Debug("Disabled preset {presetName} called!", preset.Name);
                return;
            }

            float presetIntensity = (float)preset.Intensity / 100;
            float intensity = presetIntensity * oscIntensity;

            // Apply intensity to muscles
            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i] = muscles[i].WithIntensity((int)(intensity * 100));
            }

            Log.Debug("Triggering preset {presetName} at {intensity} intensity!", preset.Name, oscIntensity);
            Sensation sensation = preset.SensationObject
                .MultiplyIntensityBy((Multiplier)intensity);

            owo.AddSensation(sensation, muscles, preset.Name);
        }

        public override void Reset()
        {
            // Nothing to reset
        }
    }
}
