namespace OWOVRC.Classes.Effects.Muscles
{
    public struct MuscleCollisionData
    {
        public string Name { get; private set; }
        public float Proximity { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public float VelocityMultiplier { get; set; }

        public MuscleCollisionData(string name, float proximity)
        {
            Name = name;
            Proximity = proximity;
            LastUpdate = DateTime.Now;
            VelocityMultiplier = 0;
        }
    }
}
