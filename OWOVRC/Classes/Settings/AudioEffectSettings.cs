using OWOVRC.Audio.Classes;
using OWOVRC.Classes.Helpers;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    //NOTE: Priority is not used as each spectrum (Bass, SubBass, etc) has their own priorities
    public partial class AudioEffectSettings : EffectSettingsBase
    {
        [JsonInclude]
        public AudioEffectSpectrumSettings BassSettings { get; set; } =
            new("Bass", AudioSpectrum.Bass, DefaultBassMuscles, baseIntensity: 55, priority: 5, minDB: 4, maxDB: 20, sensationFrequency: 15);

        [JsonInclude] //NOTE: zero-width space (U+200B) is used to force a linebreak in the UI
        public AudioEffectSpectrumSettings SubBassSettings { get; set; } =
            new("Sub-\u200BBass", AudioSpectrum.SubBass, DefaultSubBassMuscles, baseIntensity: 25, priority: 4, minDB: 3, maxDB: 15, sensationFrequency: 5);
        [JsonInclude]
        public AudioEffectSpectrumSettings TrebleSettings { get; set; } =
            new("Treble", AudioSpectrum.Brilliance, DefaultTrebleMuscles, baseIntensity: 30, priority: 3, minDB: 0.5f, maxDB: 4, sensationFrequency: 75);
        [JsonInclude]
        public AudioEffectSpectrumSettings LowMidSettings { get; set; } =
            new("Low-\u200BMid", AudioSpectrum.LowMid, DefaultLowMidMuscles, baseIntensity: 30, priority: 2, minDB: 1, maxDB: 4, sensationFrequency: 21);
        [JsonInclude]  //NOTE: zero-width space (U+200B) is used to force a linebreak in the UI
        public AudioEffectSpectrumSettings MidSettings { get; set; } =
            new("Mid", AudioSpectrum.Mid, DefaultMidMuscles, baseIntensity: 30, priority: 1, minDB: 2, maxDB: 10, sensationFrequency: 100);
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
                TrebleSettings,
                LowMidSettings,
                MidSettings
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

        public AudioEffectSettings(bool enabled = false) : base(enabled, 0)
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

        public override void SaveToFile()
        {
            SettingsHelper.SaveSettingsToFile(this, "audio.json", "Audio", SettingsJsonContext.Default.AudioEffectSettings);
        }
    }
}
