﻿using OWOGame;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Effects.Muscles;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Collections.Concurrent;
using System.Timers;
using Windows.Networking.Proximity;

namespace OWOVRC.Classes.Effects
{
    // (Loosely based on https://github.com/shadorki/vrc-owo-suit)
    public class Collision : OSCEffectBase
    {
        // OWOHelper
        private readonly OWOHelper owo;

        // Event handler for UI updates
        //public EventHandler? OnCollisionChange;
        //public string[] ActiveMuscles => [.. activeMuscles.Keys];

        // Timer for haptic updates without OSC messages
        private readonly System.Timers.Timer timer;

        // Param base
        private const string ADDRESS_BASE = "owo_suit_";

        // Dictionary to keep track of active haptic effects
        private readonly ConcurrentDictionary<string, MuscleCollisionData> activeMuscles = new(); // Dictionary of active muscles and their intensity

        // Settings
        //TODO: Implement per-muscle intensity
        public readonly CollisionEffectSettings Settings;

        public Collision(OWOHelper owo, CollisionEffectSettings settings)
        {
            this.owo = owo;
            Settings = settings;

            timer = new System.Timers.Timer()
            {
                Interval = (Settings.SensationSeconds * 1000) - 50, // Subtract 50ms to reduce "gaps" between sensations
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
            // Non-OWO message
            if (!message.Address.StartsWith(ADDRESS_BASE))
            {
                return;
            }

            string muscle = message.Address;

            float proximity;
            try
            {
                proximity = message.Values.ReadFloatElement(0);
            }
            catch (InvalidOperationException)
            {
                Log.Error("InvalidOperationException while reading float value on address {address}, trying to read int value", message.Address);
                try
                {
                    proximity = (float)message.Values.ReadIntElement(0);
                }
                catch(InvalidOperationException)
                {
                    Log.Error("InvalidOperationException while reading int value on address {address}", message.Address);
                    proximity = 0f;
                }
            }

            if (proximity > 0)
            {
                OnCollisionEnter(muscle, proximity);
            }
            else
            {
                OnCollisionExit(muscle);
            }

            //UpdateHaptics();
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
            MuscleCollisionData? valuePrev = activeMuscles.GetValueOrDefault(muscle);

            if (valuePrev == null)
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
            float distance = Math.Abs(valuePrev.Proximity - proxmimity);

            TimeSpan timediff = muscleData.LastUpdate - valuePrev.LastUpdate;

            float time = (float)timediff.TotalMilliseconds;
            float speed = distance / time;

            if (timediff < Settings.MaxTimeDiff)
            {
                muscleData.VelocityMultiplier = speed * Settings.SpeedMultiplier;

                //Log.Debug(
                //    "Speed: {speed}, Current proximity: {current}, Last proximity: {last}, Distance: {distance}, Time: {time}",
                //    speed,
                //    proxmimity,
                //    valuePrev.Proximity,
                //    distance,
                //    timediff.TotalSeconds
                //);
            }
            activeMuscles[muscle] = muscleData;
        }

        private void OnCollisionExit(string muscle)
        {
            if (activeMuscles.ContainsKey(muscle))
            {
                Log.Debug("Stop: {muscle}", muscle);
                if (!activeMuscles.TryRemove(muscle, out MuscleCollisionData? _))
                {
                    Log.Warning("Muscle '{muscle}' could not be from active muscles.", muscle);
                }
            }

            // Stop timer to preserve resources
            if (activeMuscles.IsEmpty)
            {
                Log.Debug("No sensations playing, timer stopped.");
                timer.Stop();
            }
        }

        private void UpdateHaptics()
        {
            if (!Settings.Enabled)
            {
                return;
            }

            if (!owo.IsConnected)
            {
                Log.Information("OWO Suit not connected, skipping haptics update.");
                return;
            }

            if (activeMuscles.IsEmpty)
            {
                owo.StopAllSensations();
                return;
            }

            MuscleCollisionData[] muscleCollisionData = [.. activeMuscles.Values];
            List<Muscle> musclesScaled = [];

            foreach (MuscleCollisionData muscleData in muscleCollisionData)
            {
                Muscle? muscle = OWOHelper.Muscles.GetValueOrDefault(muscleData.Name);
                if (muscle == null)
                {
                    Log.Warning(
                        "Muscle '{muscle}' not found in muscle list. Skipping sensation.",
                        muscleData.Name
                    );
                    continue;
                }

                // Velocity-based intensity
                int intensity = Settings.BaseIntensity;
                if (Settings.UseVelocity)
                {
                    float increase = muscleData.VelocityMultiplier * Settings.MinIntensity;
                    Log.Debug("Increase: {inc}", increase);
                    intensity = Settings.MinIntensity + (int)increase;
                    intensity = Math.Min(Math.Max(intensity, Settings.MinIntensity), 100);
                }

                Log.Information(
                    "Muscle: {muscle}, Intensity: {intensity}% (Min: {base}%, Multiplier: {multiplier})",
                    muscleData.Name,
                    intensity,
                    Settings.MinIntensity,
                    muscleData.VelocityMultiplier
                );

                musclesScaled.Add(muscle.Value.WithIntensity(intensity));
            }

            Sensation sensation = SensationsFactory.Create(Settings.Frequency, Settings.SensationSeconds, 100, 0, 0, 0);
            owo.AddSensation(sensation, [.. musclesScaled]);
        }

        public void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateHaptics();

            // Disable velocity-based haptics until the next time we receive a message
            foreach (MuscleCollisionData muscleData in activeMuscles.Values)
            {
                muscleData.VelocityMultiplier = 0;
            }
        }
    }
}
