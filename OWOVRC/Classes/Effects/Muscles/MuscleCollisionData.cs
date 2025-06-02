namespace OWOVRC.Classes.Effects.Muscles
{
    public class MuscleCollisionData
    {
        public string Name { get; private set; }
        public float Proximity { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public float VelocityMultiplier { get; set; }
        public float DecayFactor { get; private set; }

        public MuscleCollisionData(string name, float proximity)
        {
            Name = name;
            Proximity = proximity;
            LastUpdate = DateTime.Now;
            VelocityMultiplier = 0;
        }

        public void AddDecay(int cycleCount)
        {
            DecayFactor = VelocityMultiplier / cycleCount;
        }

        public void ApplyDecay()
        {
            VelocityMultiplier = Math.Max(0, VelocityMultiplier - DecayFactor);
        }
    }
}
