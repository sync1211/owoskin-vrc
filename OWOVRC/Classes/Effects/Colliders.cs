using OWOGame;
using OWOVRC.Classes.Effects.Muscles;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Collections.Concurrent;
using System.Timers;

namespace OWOVRC.Classes.Effects
{
    // (Loosely based on https://github.com/shadorki/vrc-owo-suit)
    public class Colliders : OSCEffectBase
    {
        // Event handler for UI updates
        //public EventHandler? OnCollisionChange;
        //public string[] ActiveMuscles => [.. activeMuscles.Keys];

        // Timer for haptic updates without OSC messages
        private readonly System.Timers.Timer timer;

        // Param base
        private const string OWO_ADDRESS_BASE = "owo_suit_";
        private const string BHAPTICS_ADDRESS_BASE = "bosc_v1_";

        // Dictionary to keep track of active haptic effects
        private readonly ConcurrentDictionary<string, MuscleCollisionData> activeMuscles = new(); // Dictionary of active muscles and their intensity

        // Name for looping sensation
        public const string SENSATION_NAME = "Colliders";

        // Settings
        public readonly CollidersEffectSettings Settings;

        public Colliders(OWOHelper owo, CollidersEffectSettings settings): base(owo)
        {
            Settings = settings;

            timer = new System.Timers.Timer()
            {
                Interval = (Settings.SensationSeconds * 1000) - 100, // Subtract 100ms to reduce "gaps" between sensations
                AutoReset = true
            };
            timer.Elapsed += OnTimerElapsed;
        }

        public override void OnOSCMessageReceived(object? sender, OSCMessage message)
        {
            ProcessMessage(message);
        }

        private void ProcessMessage(OSCMessage message)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            // Non-OWO or bHaptics message
            string muscle = message.Address.ToLower();
            if (!(muscle.StartsWith(OWO_ADDRESS_BASE) || muscle.StartsWith(BHAPTICS_ADDRESS_BASE)))
            {
                return;
            }

            float proximity = OSCHelpers.GetFloatValueFromMessage(message);

            if (proximity > 0)
            {
                OnCollisionEnter(muscle, proximity);
            }
            else
            {
                OnCollisionExit(muscle);
            }
        }

        private void OnCollisionEnter(string muscle, float proxmimity)
        {
            // Make sure the timer is running
            if (!timer.Enabled && Settings.AllowContinuous)
            {
                Log.Debug("Timer started!");
                timer.Start();
            }

            MuscleCollisionData muscleData = new(muscle, proxmimity);

            // Muscle does not exist yet, skip velocity calculation
            if (!activeMuscles.TryGetValue(muscle, out MuscleCollisionData? muscleDataPrev) || muscleDataPrev == null)
            {
                if (!activeMuscles.TryAdd(muscle, muscleData))
                {
                    Log.Warning("Unable to add muscle '{Muscle}' to active muscles.", muscle);
                }
                return;
            }

            // Calculate speed
            //NOTE: I am using a delta of the proximity value as well as a time delta from the last received message to calculate the speed
            //      This is not very ideal, but it's the best I've got so far
            float distance = Math.Abs(muscleDataPrev.Proximity - proxmimity);

            TimeSpan timediff = muscleData.LastUpdate - muscleDataPrev.LastUpdate;

            float time = (float)timediff.TotalMilliseconds;
            float speed = distance / time;

            if (timediff < Settings.MaxTimeDiff)
            {
                muscleData.VelocityMultiplier = speed * (Settings.SpeedMultiplier * 100);
                muscleData.AddDecay(Settings.DecayCycleCount);
            }
            activeMuscles.AddOrUpdate(muscle, muscleData, (_, _) => muscleData);
            Log.Debug("Add/Update muscle {muscle} (Active: {muscleCount})", muscleData.Name, activeMuscles.Count);
        }

        private void OnCollisionExit(string muscle)
        {
            if (activeMuscles.ContainsKey(muscle))
            {
                Log.Debug("Stop: {Muscle} (Active muscles: {MuscleCount}", muscle, activeMuscles.Count);
                if (!activeMuscles.TryGetValue(muscle, out MuscleCollisionData? muscleData))
                {
                    Log.Warning("Muscle '{Muscle}' could not be from active muscles.", muscle);
                }
                else
                {
                    // Mark muscle to stop on next calculation cycle
                    muscleData.StopOnNextCycle = true;
                }
            }

            // Stop timer to preserve resources
            if (activeMuscles.IsEmpty)
            {
                Log.Debug("No sensations playing, timer stopped.");
                owo.StopSensation(SENSATION_NAME, false);
                timer.Stop();
            }
        }

        private void UpdateHaptics()
        {
            if (!Settings.Enabled)
            {
                return;
            }

            if (activeMuscles.IsEmpty)
            {
                owo.StopSensation(SENSATION_NAME, false);
                return;
            }

            Span<MuscleCollisionData> muscleCollisionData = [.. activeMuscles.Values];
            Muscle[] musclesScaled = new Muscle[muscleCollisionData.Length];

            for (int i = 0; i < muscleCollisionData.Length; i++)
            {
                MuscleCollisionData muscleData = muscleCollisionData[i];
                if (!OWOMuscles.Muscles.TryGetValue(muscleData.Name, out Muscle muscle))
                {
                    Log.Warning(
                        "Muscle '{Muscle}' not found in muscle list. Skipping sensation.",
                        muscleData.Name
                    );
                    continue;
                }

                // Maximum intensity
                if (!Settings.MuscleIntensities.TryGetValue(muscle.id, out int maxIntensity))
                {
                    maxIntensity = 100;
                }
                int intensity = maxIntensity;

                // Velocity-based intensity
                if (Settings.UseVelocity)
                {
                    float increase = muscleData.VelocityMultiplier * Math.Max(Settings.MinIntensity, 1);
                    Log.Verbose("Increase: {Inc}", increase);

                    intensity = Settings.MinIntensity + (int)increase;
                    intensity = Math.Max(intensity, Settings.MinIntensity); // Lower limit
                    intensity = Math.Min(intensity, maxIntensity);          // Upper limit
                }

                Log.Verbose(
                    "Muscle: {Muscle}, Intensity: {Intensity}% (Min: {Base}%, Multiplier: {Multiplier})",
                    muscleData.Name,
                    intensity,
                    Settings.MinIntensity,
                    muscleData.VelocityMultiplier
                );

                musclesScaled[i] = muscle.WithIntensity(maxIntensity);

                // Remove if requested
                if (muscleData.StopOnNextCycle)
                {
                    activeMuscles.TryRemove(muscleData.Name, out _);
                }
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

        public void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateHaptics();

            // Apply decay to velocity-based multiplier
            //NOTE: The decayFactor needs to be determined when VelocityMultiplier is set
            for (int i = 0; i < activeMuscles.Count; i++)
            {
                MuscleCollisionData muscleData = activeMuscles.ElementAt(i).Value;
                muscleData.ApplyDecay();
            }
        }

        public override void Stop()
        {
            activeMuscles.Clear();
            Log.Debug("Collision effect reset!");
        }
    }
}
