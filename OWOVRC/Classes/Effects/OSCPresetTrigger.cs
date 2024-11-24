﻿using BuildSoft.OscCore;
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

        public OSCSensationPreset? GetPresetFromMessage(OSCMessage message, out Muscle[] muscles)
        {
            string presetString = message.Address.Substring(OSC_ADDRESS_PREFIX.Length);
            string muscleGroupName = String.Empty;
            string presetName = presetString;
            muscles = [];

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
                return null;
            }

            // Get muscles
            if (OWOHelper.MuscleGroups.TryGetValue(muscleGroupName, out Muscle[]? muscleGroup))
            {
                muscles = muscleGroup;
            }
            else if (OWOHelper.Muscles.TryGetValue($"owo_suit_{muscleGroupName}", out Muscle muscle))
            {
                muscles = [muscle];
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
            float intensityMultiplier = GetIntensityFromMessage(message);
            if (intensityMultiplier == 0)
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

            Log.Debug("Triggering preset {presetName} at {intensity} intensity!", preset.Name, intensityMultiplier);
            float presetIntensity = (float)preset.Intensity / 100;
            Sensation sensation = preset.SensationObject
                .MultiplyIntensityBy((Multiplier) presetIntensity)       // Multiplier from preset settings
                .MultiplyIntensityBy((Multiplier) intensityMultiplier);  // Multiplier from OSC message

            owo.AddSensation(sensation, muscles);
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
