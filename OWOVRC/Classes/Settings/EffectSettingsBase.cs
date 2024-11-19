namespace OWOVRC.Classes.Settings
{
    public abstract class EffectSettingsBase
    {
        public bool Enabled { get; set; } = true;
        public int Priority { get; set; }
    }
}
