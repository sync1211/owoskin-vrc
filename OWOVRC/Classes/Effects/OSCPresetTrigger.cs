using BuildSoft.OscCore;
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
        private const string PATH_BASE = "OWO/SensationsTrigger";
        public readonly OSCPresetsSettings Settings;

        public OSCPresetTrigger(OWOHelper owo, OSCPresetsSettings settings) : base(owo)
        {
            Settings = settings;
        }

        public bool AddPreset(OSCSensationPreset preset)
        {
            if (!Settings.Presets.TryAdd(preset.Name, preset))
            {
                return false;
            }

            AddCallbackToPreset(preset);
            return true;
        }

        public void UpdatePresetRegistration(OSCReceiver receiver, OSCSensationPreset preset)
        {
            // Remove registration
            UnregisterCallbackForPreset(receiver, preset);

            // Re-create callback with new values
            AddCallbackToPreset(preset);

            // Re-register
            RegisterCallbackForPreset(receiver, preset);
        }

        public void ReRegisterAllPresets(OSCReceiver receiver)
        {
            foreach (OSCSensationPreset preset in Settings.Presets.Values)
            {
                UpdatePresetRegistration(receiver, preset);
            }
        }

        private void AddCallbackToPreset(OSCSensationPreset preset)
        {
            // Muscle groups
            for (int i = 0; i < OWOMuscles.MuscleGroups.Count; i++)
            {
                KeyValuePair<string, Muscle[]> muscleGroup = OWOMuscles.MuscleGroups.ElementAt(i);

                string address = $"{PATH_BASE}/{preset.Name}/{muscleGroup.Key}";

                if (preset.MessageCallbacks.TryAdd(address, (values) => ProcessMessage(values, preset, muscleGroup.Value)))
                {
                    Log.Debug("Created callback for preset {PresetName} at address {Address}!", preset.Name, address);
                }
                else
                {
                    Log.Warning("Failed to create callback for preset {PresetName} at address {Address}!", preset.Name, address);
                }
            }

            // Individual muscles
            for (int i = 0; i < OWOMuscles.MusclesCount; i++)
            {
                KeyValuePair<string, Muscle> muscleInfo = OWOMuscles.MusclesCaptialized.ElementAt(i);

                string address = $"{PATH_BASE}/{preset.Name}/{muscleInfo.Key}";
                Muscle[] muscles = [muscleInfo.Value];
                
                if (preset.MessageCallbacks.TryAdd(address, (values) => ProcessMessage(values, preset, muscles)))
                {
                    Log.Debug("Created callback for preset {PresetName} at address {Address}!", preset.Name, address);
                }
                else
                {
                    Log.Warning("Failed to create callback for preset {PresetName} at address {Address}!", preset.Name, address);
                }
            }

            // Base sensation without any muscles specified
            Muscle[] sensationMuscles = OWOMuscles.GetMusclesFromSensation(preset.SensationObject);
            
            string presetBaseAddr = $"{PATH_BASE}/{preset.Name}";
            if (preset.MessageCallbacks.TryAdd(presetBaseAddr, (values) => ProcessMessage(values, preset, sensationMuscles)))
            {
                Log.Debug("Created callback for preset {PresetName} at address {Address}!", preset.Name, presetBaseAddr);
            }
            else
            {
                Log.Warning("Failed to create callback for preset {PresetName} at address {Address}!", preset.Name, presetBaseAddr);
            }
        }

        public bool RemovePreset(string name, OSCReceiver receiver)
        {
            if (!Settings.Presets.TryRemove(name, out OSCSensationPreset? preset))
            {
                return false;
            }

            UnregisterCallbackForPreset(receiver, preset);
            return true;
        }

        public void ClearPresets(OSCReceiver receiver)
        {
            UnregisterCallbacks(receiver);
            Settings.Presets.Clear();
        }

        public override void RegisterCallbacks(OSCReceiver receiver)
        {
            foreach (OSCSensationPreset preset in Settings.Presets.Values)
            {
                RegisterCallbackForPreset(receiver, preset);
            }
        }

        public void RegisterCallbackForPreset(OSCReceiver receiver, OSCSensationPreset preset)
        {
            if (preset.MessageCallbacks.Count == 0)
            {
                AddCallbackToPreset(preset);
            }

            foreach (KeyValuePair<string, Action<OscMessageValues>> kvp in preset.MessageCallbacks)
            {
                bool result = receiver.TryAddMessageCallback(kvp.Key, kvp.Value);
                if (result)
                {
                    Log.Debug("Register OSC callback for preset {PresetName} at address {Address}!", preset.Name, kvp.Key);
                }
                else
                {
                    Log.Warning("Failed to register OSC callback for preset {PresetName} at address {Address}!", preset.Name, kvp.Key);
                }
            }
        }

        public override void UnregisterCallbacks(OSCReceiver receiver)
        {
            foreach (OSCSensationPreset preset in Settings.Presets.Values)
            {
                UnregisterCallbackForPreset(receiver, preset);
            }
        }

        public static void UnregisterCallbackForPreset(OSCReceiver receiver, OSCSensationPreset preset, bool clear=false)
        {
            foreach (KeyValuePair<string, Action<OscMessageValues>> kvp in preset.MessageCallbacks)
            {
                bool result = receiver.TryRemoveMessageCallback(kvp.Key, kvp.Value);
                if (result)
                {
                    Log.Debug("Unregistered OSC callback for preset {PresetName} at address {Address}!", preset.Name, kvp.Key);
                }
                else
                {
                    Log.Warning("Failed to unregister OSC callback for preset {PresetName} at address {Address}!", preset.Name, kvp.Key);
                }
            }

            if (clear)
            {
                preset.MessageCallbacks.Clear();
            }
        }

        public void ProcessMessage(OscMessageValues values, OSCSensationPreset preset, Muscle[] muscles)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Get intensity
            float oscIntensity = OSCHelpers.GetFloatValueFromMessageValues(values);

            if (!preset.Enabled)
            {
                Log.Debug("Disabled preset {PresetName} called!", preset.Name);
                return;
            }

            // Stop looped sensation
            if (oscIntensity <= 0)
            {
                Log.Debug("Stopping preset {PresetName}!", preset.Name);

                owo.StopSensation(preset.Name, preset.Interruptable);
                return;
            }

            float intensity = preset.Intensity * oscIntensity;

            // Apply intensity to muscles
            for (int i = 0; i < muscles.Length; i++)
            {
                muscles[i] = muscles[i].WithIntensity((int)intensity);
            }

            Log.Debug("Triggering preset {PresetName} at {Intensity} intensity!", preset.Name, intensity);
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

        public override void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
