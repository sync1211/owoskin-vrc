using OWOVRC.Audio.Classes;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    //NOTE: Priority is not used as each spectrum (Bass, SubBass, etc) has their own priorities
    public partial class AudioEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public AudioEffectSpectrumSettings BassSettings { get; set; } =
            new("Bass", AudioSpectrum.Bass, DefaultBassMuscles, 55, 3, 4, 20, 15);

        [JsonInclude] //NOTE: zero-width space (U+200B) is used to force a linebreak in the UI
        public AudioEffectSpectrumSettings SubBassSettings { get; set; } =
            new("Sub-​Bass", AudioSpectrum.SubBass, DefaultSubBassMuscles, 40, 2, 3, 15, 5);
        [JsonInclude]
        public AudioEffectSpectrumSettings TrebleSettings { get; set; } =
            new("Treble", AudioSpectrum.Brilliance, DefaultTrebleMuscles, 30, 1, 0.5f, 4, 75);
        [JsonIgnore] //NOTE: This array is auto-sorted on priority changes via the OnPriorityChanged event
        public AudioEffectSpectrumSettings[] SpectrumSettings { get; private set; } = null!;

        //NOTE: Add all spectrum settings here to be recognized by the audio effect and UI
        //      This array is used to sort the spectrum settings by priority to avoid sorting overhead when triggering audio effects
        private void SetUpSpectrumSettings()
        {
            SpectrumSettings =
            [
                SubBassSettings,
                BassSettings,
                TrebleSettings
            ];
        }

        [JsonConstructor]
        public AudioEffectSettings(bool enabled, AudioEffectSpectrumSettings bassSettings, AudioEffectSpectrumSettings subBassSettings, AudioEffectSpectrumSettings trebleSettings) : base(enabled, 0)
        {
            BassSettings = bassSettings;
            SubBassSettings = subBassSettings;
            TrebleSettings = trebleSettings;

            SetUpSpectrumSettings();
            SetUpPriorityListener();
        }

        public AudioEffectSettings(bool enabled = true) : base(enabled, 0)
        {
            SetUpSpectrumSettings();
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
