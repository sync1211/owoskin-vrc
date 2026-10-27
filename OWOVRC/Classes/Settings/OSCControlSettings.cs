using OWOVRC.Classes.Helpers;

namespace OWOVRC.Classes.Settings
{
    public class OSCControlSettings : EffectSettingsBase
    {
        public OSCControlSettings(bool enabled, int priority) : base(enabled, priority) {
        
        }

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "oscControl.json", "Remote Control effect", SettingsJsonContext.Default.OSCControlSettings);
        }
    }
}
