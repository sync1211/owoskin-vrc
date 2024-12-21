using OWOGame;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public partial class AudioEffectSettings
    {
        [JsonIgnore]
        private static readonly Muscle[] DefaultBassMuscles = [
            Muscle.Lumbar_L,
            Muscle.Abdominal_R,
            Muscle.Lumbar_R,
            Muscle.Abdominal_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultSubBassMuscles = [
            Muscle.Abdominal_R,
            Muscle.Abdominal_L
        ];
    }
}
