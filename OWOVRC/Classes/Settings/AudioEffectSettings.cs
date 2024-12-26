using OWOVRC.Audio.Classes;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    //NOTE: Priority is not used as each spectrum (Bass, SubBass, etc) has their own priorities
    public partial class AudioEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public AudioEffectSpectrumSettings BassSettings { get; } =
            new("Bass", AudioSpectrum.Bass, DefaultBassMuscles, 25, 2, 12, 20, 10);

        [JsonInclude] //NOTE: zero-width space (U+200B) is used to force a linebreak in the UI
        public AudioEffectSpectrumSettings SubBassSettings { get; } =
            new("Sub-​Bass", AudioSpectrum.SubBass, DefaultSubBassMuscles, 25, 5, 15, 25);
        [JsonIgnore] //NOTE: This array is auto-sorted on priority changes via the OnPriorityChanged event
        public readonly AudioEffectSpectrumSettings[] SpectrumSettings;


        [JsonConstructor]
        public AudioEffectSettings(bool enabled, AudioEffectSpectrumSettings bassSettings, AudioEffectSpectrumSettings subBassSettings) : base(enabled, 0)
        {
            BassSettings = bassSettings;
            SubBassSettings = subBassSettings;

            SpectrumSettings = [BassSettings, SubBassSettings];
            SetUpPriorityListener();
        }

        public AudioEffectSettings(bool enabled = true) : base(enabled, 0)
        {
            SpectrumSettings = [BassSettings, SubBassSettings];
            SetUpPriorityListener();
        }

        private void SetUpPriorityListener()
        {
            foreach (AudioEffectSpectrumSettings spectrum in SpectrumSettings)
            {
                spectrum.OnPriorityChanged += HandleSpectrumSettingsPriorityChanged;
            }
        }

        private void HandleSpectrumSettingsPriorityChanged(object? sender, System.EventArgs e)
        {
            SortSettings();
        }

        public void SortSettings()
        {
            Array.Sort(SpectrumSettings, (a, b) => b.Priority.CompareTo(a.Priority));
        }
    }
}
