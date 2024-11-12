using OWOGame;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Sensations.Muscles;
using Serilog;

namespace OWOVRC.Classes.Sensations
{
    // (Core functionality copied from https://github.com/shadorki/vrc-owo-suit)
    public class Collision(OWOHelper owo) : OSCSensationBase
    {
        // Event handler
        //public EventHandler? OnCollisionChange;
        //public string[] ActiveMuscles => [.. activeMuscles.Keys];

        // Param base
        private const string ADDRESS_BASE = "owo_suit_";

        // Dictionary to keep track of active haptic effects
        private readonly Dictionary<string, MuscleCollisionData> activeMuscles = new(); // Dictionary of active muscles and their intensity

        // Settings
        public bool IsEnabled = true;
        public bool UseVelocity = true;
        public int BaseIntensity = 100;
        public int MinIntensity = 25;
        public int Frequency = 50;
        public float SpeedMultiplier = 200.0f;
        public TimeSpan MaxTimeDiff = TimeSpan.FromSeconds(1);

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

            if (proxmimity > 0) {
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
            MuscleCollisionData muscleData = new(muscle, proxmimity);
            MuscleCollisionData? valuePrev = activeMuscles.GetValueOrDefault(muscle);

            if (valuePrev == null)
            {
                activeMuscles.Add(muscle, muscleData);
                return;
            }

            // Calculate speed
            //NOTE: I am using a delta of the proximity value as well as a time delta from the last received message to calculate the speed
            //      This is not very ideal, but it's the best I've got so far
            float distance = Math.Abs(valuePrev.Proximity - proxmimity);

            TimeSpan timediff = (muscleData.LastUpdate - valuePrev.LastUpdate);

            float time = (float) timediff.TotalMilliseconds;
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
                activeMuscles.Remove(muscle);
            }
        }

        private Sensation CreateSensation(MuscleCollisionData muscleData)
        {
            int intensity = BaseIntensity;

            if (UseVelocity)
            {
                float increase = (muscleData.VelocityMultiplier * MinIntensity);
                Log.Debug("Increase: {inc}", increase);
                intensity = MinIntensity + (int) increase;
                intensity = Math.Min(Math.Max(intensity, MinIntensity), 100); //Math.Clamp(intensity, MinIntensity, 100);
            }

            Log.Information(
                "Muscle: {muscle}, Intensity: {intensity}% (Min: {base}%, Multiplier: {multiplier})",
                muscleData.Name,
                intensity,
                MinIntensity,
                muscleData.VelocityMultiplier
            );

            return SensationsFactory.Create(Frequency, 0.3f, intensity, 0, 0, 0);
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

            if (activeMuscles.Count == 0)
            {
                owo.StopAllSensations();
                return;
            }

            foreach (MuscleCollisionData muscleData in activeMuscles.Values)
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
    }
}
