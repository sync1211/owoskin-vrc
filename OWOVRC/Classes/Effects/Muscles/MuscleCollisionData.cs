namespace OWOVRC.Classes.Effects.Muscles
{
    public class MuscleCollisionData
    {
        //public int ID { get; }

        public int DecayCyclesTotal { get; private set; }
        public int DecayCyclesLeft { get; private set; }
        public float DecayPercent
        {
            get
            {
                return (float)DecayCyclesLeft / (float)DecayCyclesTotal;
            }
        }
        public int DecayStartIntensity { get; set; }

        public int MaxCyclesLeft { get; private set; } // Failsafe to prevent stuck muscles, decreased each calculation cycle, resets on every update
        private readonly int MaxCycles = 100;
        public float CurrentProximity { get; private set; }
        public float ProximityDelta { get; set; } // Sum of all proximity changes between haptic updates

        public MuscleCollisionData(int maxCycles = 100)
        {
            MaxCyclesLeft = maxCycles;
            MaxCycles = maxCycles;
        }

        public void UpdateProximity(float newProximity, int DecayCycleCount)
        {
            float delta = Math.Abs(newProximity - CurrentProximity);
            ProximityDelta += delta;

            CurrentProximity = newProximity;

            MaxCyclesLeft = MaxCycles;

            DecayCyclesTotal = DecayCycleCount;
            DecayCyclesLeft = DecayCycleCount;
        }

        public void ResetDecay()
        {
            DecayCyclesLeft = DecayCyclesTotal;
        }

        public void ProcessCycle()
        {
            MaxCyclesLeft = Math.Max(0, MaxCyclesLeft - 1);
            DecayCyclesLeft = Math.Max(0, DecayCyclesLeft - 1);

            ProximityDelta = 0f;
        }

        public void Disable()
        {
            MaxCyclesLeft = 0;
        }
    }
}
