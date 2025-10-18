using BuildSoft.OscCore;
using OWOGame;
using OWOVRC.Classes.Effects.Muscles;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Timers;

namespace OWOVRC.Classes.Effects
{
    // (Loosely based on https://github.com/shadorki/vrc-owo-suit)
    public class Colliders : OSCEffectBase
    {
        // Event handler for UI updates
        //public EventHandler? OnCollisionChange;
        //public string[] ActiveMuscles => [.. activeMuscles.Keys];

        // Dictionary to keep track of active haptic effects
        private readonly ConcurrentDictionary<int, MuscleCollisionData> activeMuscles = new(); // Dictionary of active muscles and their intensity

        // Name for looping sensation
        public const string SENSATION_NAME = "Colliders";

        // Settings
        public readonly CollidersEffectSettings Settings;

        // Callbacks for OSC messages
        private readonly Dictionary<string, Action<OscMessageValues>> messageCallbacks = [];

        public Colliders(OWOHelper owo, CollidersEffectSettings settings) : base(owo)
        {
            Settings = settings;

            owo.OnCalculationCycle += OnTimerElapsed;

            PopulateCallbacks();
        }

        private void PopulateCallbacks()
        {
            for (int i = 0; i < OWOMuscles.MusclesCaptialized.Count; i++)
            {
                KeyValuePair<string, Muscle> kvp = OWOMuscles.MusclesCaptialized.ElementAt(i);
                string muscleName = kvp.Key;
                string muscleNameLower = muscleName.ToLower();

                Muscle muscle = kvp.Value;

                messageCallbacks[muscleName] = (values) => ProcessMessage(muscle.id, values);
                messageCallbacks[muscleNameLower] = (values) => ProcessMessage(muscle.id, values); //NOTE: Needs to be a new function to avoid an exception as the method is used as a key when registering message handlers
            }
        }

        private void ProcessMessage(int muscleID, OscMessageValues values)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            float proximity = OSCHelpers.GetFloatValueFromMessageValues(values);

            MuscleCollisionData muscleData = activeMuscles.GetOrAdd(
                muscleID,
                new MuscleCollisionData(Settings.MaxSensationCycles)
            );

            muscleData.UpdateProximity(proximity, Settings.DecayCycleCount);
        }

        private int GetMuscleIntensity(int muscleID)
        {
            MuscleCollisionData? muscleData = activeMuscles.GetValueOrDefault(muscleID);
            if (muscleData == null || muscleData.MaxCyclesLeft <= 0)
            {
                return 0;
            }

            // No proximity change and no objects in collider -> remove entry
            if (muscleData.ProximityDelta == 0 && muscleData.CurrentProximity == 0 && (Settings.DecayCycleCount == 0))
            {
                // Removal unnecessary as we can update the entry in-place on the next update
                muscleData.Disable();
                return 0;
            }

            int maxIntensity = Settings.MuscleIntensities.GetValueOrDefault(muscleID);
            int intensity = Settings.MinIntensity;

            // The range between min and max intensity
            int intensityIncrease = maxIntensity - Settings.MinIntensity;

            // Proximity calculation
            if (Settings.SpeedMultiplier > 0)
            {
                // Intensity increase based on collision speed
                float multiplier = Math.Min(1f, muscleData.ProximityDelta * Settings.SpeedMultiplier);
                intensityIncrease = (int)(intensityIncrease * multiplier);
            }
            else
            {
                // Intensity increase based on proximity only
                intensityIncrease = (int)(intensityIncrease * muscleData.CurrentProximity);
            }

            // No decay
            if (Settings.DecayCycleCount == 0)
            {
                return intensity + intensityIncrease;
            }

            // Apply decay
            intensity = ApplyDecay(intensity, intensityIncrease, muscleData);

            // Update decay counter and other values
            muscleData.ProcessCycle();

            return intensity;
        }

        private int ApplyDecay(int intensity, int intensityIncrease, MuscleCollisionData muscleData)
        {
            if (muscleData.CurrentProximity > 0)
            {
                if (!Settings.DecayOnChanges)
                {
                    return intensity + intensityIncrease;
                }

                if (muscleData.DecayStartIntensity < intensityIncrease)
                {
                    muscleData.DecayStartIntensity = intensityIncrease;
                    muscleData.ResetDecay();
                }

                // Decay towards minimum intensity
                return intensity + (int)(muscleData.DecayStartIntensity * muscleData.DecayPercent);
            }
            else
            {
                if (!Settings.DecayOnExit)
                {
                    muscleData.Disable();
                    return 0;
                }
                // Decay towards 0
                return (int)((intensity + intensityIncrease) * muscleData.DecayPercent);
            }
        }

        private void UpdateHaptics()
        {
            if (!Settings.Enabled)
            {
                return;
            }

            Muscle[] musclesScaled = OWOMuscles.MuscleGroups["all"];

            bool musclesIdle = true; // Flag to check if any muscles have an intensity > 0, if not we stop the sensation
            for (int i = 0; i < musclesScaled.Length; i++)
            {
                Muscle muscle = musclesScaled[i];
                int muscleIntensity = GetMuscleIntensity(muscle.id);
                musclesScaled[i] = muscle.WithIntensity(muscleIntensity);

                musclesIdle = (musclesIdle && muscleIntensity <= 0);
            }

            if (musclesIdle)
            {
                owo.StopSensation(SENSATION_NAME, false);
                Log.Debug("Colliders sensation stopped, no active muscles!");
                return;
            }

            Sensation sensation = Settings.GetSensation();

            // Run or update sensation
            if (owo.GetRunningSensations().ContainsKey(SENSATION_NAME))
            {
                owo.UpdateLoopedSensation(SENSATION_NAME, sensation, musclesScaled);
            }
            else
            {
                owo.AddLoopedSensation(SENSATION_NAME, sensation, musclesScaled);
            }
        }

        public override void RegisterCallbacks(OSCReceiver receiver)
        {
            for (int i = 0; i < messageCallbacks.Count; i++)
            {
                KeyValuePair<string, Action<OscMessageValues>> pair = messageCallbacks.ElementAt(i);
                string muscleName = pair.Key;
                Action<OscMessageValues> callback = pair.Value;

                bool result = receiver.TryAddMessageCallback(muscleName, callback);
                if (result)
                {
                    Log.Debug("Registered OSC callback for muscle {Muscle}.", muscleName);
                }
                else
                {
                    Log.Warning("Failed to register OSC callback for muscle {Muscle}!", muscleName);
                }
            }
        }

        public override void UnregisterCallbacks(OSCReceiver receiver)
        {
            for (int i = 0; i < messageCallbacks.Count; i++)
            {
                KeyValuePair<string, Action<OscMessageValues>> pair = messageCallbacks.ElementAt(i);
                string muscleName = pair.Key;
                Action<OscMessageValues> callback = pair.Value;

                bool result = receiver.TryRemoveMessageCallback(muscleName, callback);
                if (result)
                {
                    Log.Debug("Unregistered OSC callback for muscle {Muscle}.", muscleName);
                }
                else
                {
                    Log.Warning("Failed to unregister OSC callback for muscle {Muscle}!", muscleName);
                }
            }
        }

        public void OnTimerElapsed(object? sender, EventArgs e)
        {
            UpdateHaptics();
        }

        public override void Stop()
        {
            activeMuscles.Clear();
            Log.Debug("Collision effect reset!");
        }

        public override void Dispose()
        {
            owo.OnCalculationCycle -= OnTimerElapsed;
            GC.SuppressFinalize(this);
        }
    }
}