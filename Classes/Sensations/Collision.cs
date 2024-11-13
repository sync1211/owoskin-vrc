using OWOGame;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Sensations.Muscles;
using Serilog;
using System.Collections.Concurrent;
using System.Timers;

namespace OWOVRC.Classes.Sensations
{
    // (Core functionality copied from https://github.com/shadorki/vrc-owo-suit)
    public class Collision : OSCSensationBase
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
        public bool IsEnabled = true;
        public bool UseVelocity = true;
        public bool AllowContinuous = true;
        public int BaseIntensity = 100;
        public int MinIntensity = 50; // Min intensity when calculating speed
        public int Frequency = 50;
        public float SensationSeconds = 0.3f;
        public float SpeedMultiplier = 200.0f;
        public TimeSpan MaxTimeDiff = TimeSpan.FromSeconds(1);

        public Collision(OWOHelper owo)
        {
            this.owo = owo;

            timer = new System.Timers.Timer()
            {
                Interval = (SensationSeconds * 1000) - 50, // Subtract 50ms to reduce "gaps" between sensations
                AutoReset = true
            };
            timer.Elapsed += OnTimerElapsed; //TODO: Would it be better to calculate the speed on timer elapsed rather than on message received?
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
            float proxmimity = message.Values.ReadFloatElement(0);

            if (proxmimity > 0)
            {
                OnCollisionEnter(muscle, proxmimity);
            }
            else
            {
                OnCollisionExit(muscle);
            }

            UpdateHaptics();
        }

        private void OnCollisionEnter(string muscle, float proxmimity)
        {
            // Make sure the timer is running
            if (!timer.Enabled && AllowContinuous)
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

            if (timediff < MaxTimeDiff)
            {
                muscleData.VelocityMultiplier = speed * SpeedMultiplier;

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

        private MicroSensation CreateSensation(MuscleCollisionData muscleData)
        {
            int intensity = BaseIntensity;

            if (UseVelocity)
            {
                float increase = muscleData.VelocityMultiplier * MinIntensity;
                Log.Debug("Increase: {inc}", increase);
                intensity = MinIntensity + (int)increase;
                intensity = Math.Min(Math.Max(intensity, MinIntensity), 100);
            }

            Log.Information(
                "Muscle: {muscle}, Intensity: {intensity}% (Min: {base}%, Multiplier: {multiplier})",
                muscleData.Name,
                intensity,
                MinIntensity,
                muscleData.VelocityMultiplier
            );

            return SensationsFactory.Create(Frequency, SensationSeconds, intensity, 0, 0, 0);
        }

        private void UpdateHaptics()
        {
            if (!IsEnabled)
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
            foreach (MuscleCollisionData muscleData in muscleCollisionData)
            {
                Sensation sensation = CreateSensation(muscleData);
                Muscle? muscle = OWOHelper.Muscles.GetValueOrDefault(muscleData.Name);
                if (muscle == null)
                {
                    Log.Warning(
                        "Muscle '{muscle}' not found in muscle list. Skipping sensation.",
                        muscleData.Name
                    );
                    continue;
                }

                Muscle[] muscles = [muscle.Value];
                owo.AddSensation(sensation, muscles);
            }
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
