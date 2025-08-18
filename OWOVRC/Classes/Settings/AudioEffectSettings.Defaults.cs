using OWOGame;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Settings
{
    public partial class AudioEffectSettings
    {
        [JsonIgnore]
        private static readonly Muscle[] DefaultBassMuscles = [
            Muscle.Pectoral_R,
            Muscle.Pectoral_L,
            Muscle.Dorsal_R,
            Muscle.Dorsal_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultSubBassMuscles = [
            Muscle.Abdominal_R,
            Muscle.Abdominal_L,
            Muscle.Lumbar_R,
            Muscle.Lumbar_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultTrebleMuscles = [
            Muscle.Pectoral_R,
            Muscle.Pectoral_L,
            Muscle.Dorsal_R,
            Muscle.Dorsal_L,
            Muscle.Arm_R,
            Muscle.Arm_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultLowMidMuscles = [
            Muscle.Arm_R,
            Muscle.Arm_L
        ];
        [JsonIgnore]
        private static readonly Muscle[] DefaultMidMuscles = [
            Muscle.Pectoral_L,
            Muscle.Pectoral_R
        ];
    }
}
