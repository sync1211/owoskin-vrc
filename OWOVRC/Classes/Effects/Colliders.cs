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
        private const string ADDRESS_BASE = "owo_suit_";

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
                Interval = Settings.SensationSeconds * 1000 - 100, // Subtract 100ms to reduce "gaps" between sensations
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

            // Non-OWO message
            if (!message.Address.StartsWith(ADDRESS_BASE))
            {
                return;
            }

            string muscle = message.Address;
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
            if (!activeMuscles.TryGetValue(muscle, out MuscleCollisionData muscleDataPrev))
            {
                if (!activeMuscles.TryAdd(muscle, muscleData))
                {
                    Log.Warning("Unable to add muscle '{muscle}' to active muscles.", muscle);
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
            }
            activeMuscles[muscle] = muscleData;
        }

        private void OnCollisionExit(string muscle)
        {
            if (activeMuscles.ContainsKey(muscle))
            {
                Log.Debug("Stop: {muscle}", muscle);
                if (!activeMuscles.TryRemove(muscle, out MuscleCollisionData _))
                {
                    Log.Warning("Muscle '{muscle}' could not be from active muscles.", muscle);
                }
            }

            // Stop timer to preserve resources
            if (activeMuscles.IsEmpty)
            {
                Log.Debug("No sensations playing, timer stopped.");
                owo.StopSensation(SENSATION_NAME);
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
                owo.StopSensation(SENSATION_NAME);
                return;
            }

            Span<MuscleCollisionData> muscleCollisionData = [.. activeMuscles.Values];
            Muscle[] musclesScaled = new Muscle[muscleCollisionData.Length];

            for (int i = 0; i < muscleCollisionData.Length; i++)
            {
                MuscleCollisionData muscleData = muscleCollisionData[i];
                Muscle? muscle = OWOMuscles.Muscles.GetValueOrDefault(muscleData.Name.ToLower());
                if (muscle == null)
                {
                    Log.Warning(
                        "Muscle '{muscle}' not found in muscle list. Skipping sensation.",
                        muscleData.Name
                    );
                    continue;
                }

                // Velocity-based intensity
                int intensity = 100;
                if (Settings.MuscleIntensities.TryGetValue(muscle.Value.id, out int baseIntensity))
                {
                    intensity = baseIntensity;
                }

                if (Settings.UseVelocity)
                {
                    float increase = muscleData.VelocityMultiplier * Settings.MinIntensity;
                    Log.Debug("Increase: {inc}", increase);
                    intensity = Settings.MinIntensity + (int)increase;
                    intensity = Math.Min(Math.Max(intensity, Settings.MinIntensity), 100);
                }

                Log.Debug(
                    "Muscle: {muscle}, Intensity: {intensity}% (Min: {base}%, Multiplier: {multiplier})",
                    muscleData.Name,
                    intensity,
                    Settings.MinIntensity,
                    muscleData.VelocityMultiplier
                );

                musclesScaled[i] = muscle.Value.WithIntensity(intensity);
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

            // Disable velocity-based haptics until the next time we receive a message
            for (int i = 0; i < activeMuscles.Count; i++)
            {
                MuscleCollisionData muscleData = activeMuscles.ElementAt(i).Value;
                muscleData.VelocityMultiplier = 0;
            }
        }

        public override void Stop()
        {
            activeMuscles.Clear();
            Log.Debug("Collision effect reset!");
        }
    }
}
