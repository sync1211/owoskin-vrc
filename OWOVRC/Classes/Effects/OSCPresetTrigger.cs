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
        public readonly OSCPresetsSettings Settings;

        public OSCPresetTrigger(OWOHelper owo, OSCPresetsSettings settings) : base(owo)
        {
            Settings = settings;
        }

        public void AddPreset(OSCSensationPreset preset)
        {
            Settings.Presets.Add(preset.Path, preset);
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

        private static void SplitPathAndMuscleName(string path, out string presetPath, out string muscleName)
        {
            presetPath = path;
            muscleName = String.Empty;

            int index = path.LastIndexOf('/');
            if (index == -1)
            {
                return;
            }

            muscleName = path.Substring(index + 1);
            presetPath = path.Substring(0, index);
        }

        private static Muscle[]? GetMuscles(string muscleName)
        {
            if (OWOMuscles.MuscleGroups.TryGetValue(muscleName, out Muscle[]? muscleGroup))
            {
                return muscleGroup;
            }
            else if (OWOMuscles.Muscles.TryGetValue(muscleName.ToLower(), out Muscle muscle))
            {
                return [muscle];
            }
            else
            {
                return null;
            }
        }

        public OSCSensationPreset? GetPresetFromMessage(OSCMessage message, out Muscle[] muscles)
        {
            string address = message.Address;
            SplitPathAndMuscleName(address, out string presetPath, out string muscleGroupName);

            // Attempt to parse last element of path as muscle
            Muscle[]? muscles1 = GetMuscles(muscleGroupName);
            if (muscles1 == null)
            {
                // Message does not contain a valid muscle -> Use complete path as preset name
                muscles = Muscle.All;
            }
            else
            {
                address = presetPath;
                muscles = muscles1;
            }

            // Get preset
            if (!Settings.Presets.TryGetValue(address, out OSCSensationPreset? preset) || preset == null)
            {
                Log.Debug("Preset {presetName} not found!", address);
                return null;
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
                Log.Debug("Disabled preset {presetName} called!", preset.Path);
                return;
            }

            // Stop looped sensation
            if (oscIntensity == 0)
            {
                Log.Debug("Stopping preset {presetName}!", preset.Path);

                owo.StopSensation(preset.Path, preset.Interruptable);
                return;
            }

            float intensity = preset.Intensity * oscIntensity;

            // Apply intensity to muscles
            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i] = muscles[i].WithIntensity((int)intensity);
            }

            Log.Debug("Triggering preset {presetName} at {intensity} intensity!", preset.Path, intensity);
            Sensation sensation = preset.SensationObject;

            // Play sensation
            if (!preset.Loop)
            {
                owo.AddSensation(preset.Path, sensation, muscles);
            }
            else if (owo.GetRunningSensations().ContainsKey(preset.Path))
            {
                owo.UpdateLoopedSensation(preset.Path, sensation, muscles);
            }
            else
            {
                owo.AddLoopedSensation(preset.Path, sensation, muscles);
            }
        }

        public override void Stop()
        {
            foreach (OSCSensationPreset preset in Settings.Presets.Values)
            {
                owo.StopSensation(preset.Path);
            }
        }
    }
}
