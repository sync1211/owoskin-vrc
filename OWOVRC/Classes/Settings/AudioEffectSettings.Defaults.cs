using OWOGame;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public partial class AudioEffectSettings
    {
        [JsonIgnore]
        private static readonly Muscle[] DefaultBassMuscles = [
            Muscle.Pectoral_R,
            Muscle.Pectoral_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultSubBassMuscles = [
            Muscle.Abdominal_R,
            Muscle.Abdominal_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultTrebleMuscles = [
            Muscle.Pectoral_R,
            Muscle.Pectoral_L,
            Muscle.Arm_R,
            Muscle.Arm_L
        ];
    }
}
