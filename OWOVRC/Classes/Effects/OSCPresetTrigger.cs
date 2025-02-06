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
            else if (OWOMuscles.Muscles.TryGetValue($"owo_suit_{muscleGroupName.ToLower()}", out Muscle muscle))
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

            // Stop looped sensation
            if (oscIntensity == 0)
            {
                if (!preset.Loop)
                {
                    return;
                }

                Log.Debug("Stopping preset {presetName}!", preset.Name);
                owo.StopSensation(preset.Name);
                return;
            }

            float intensity = preset.Intensity * oscIntensity;

            // Apply intensity to muscles
            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i] = muscles[i].WithIntensity((int)intensity);
            }

            Log.Debug("Triggering preset {presetName} at {intensity} intensity!", preset.Name, intensity);
            Sensation sensation = preset.SensationObject;

            // Play sensation
            if (!preset.Loop)
            {
                owo.AddSensation(preset.Name, sensation, muscles);
            }
            else if (owo.GetRunningSensations().ContainsKey(preset.Name))
            {
                owo.UpdateLoopedSensation(preset.Name, sensation, muscles);
            }
            else
            {
                owo.AddLoopedSensation(preset.Name, sensation, muscles);
            }
        }

        public override void Stop()
        {
            foreach (OSCSensationPreset preset in Settings.Presets.Values)
            {
                owo.StopSensation(preset.Name);
            }
        }
    }
}
