using OWOGame;
using OWOVRC.Classes.OWOSuit;

namespace OWOVRC.Classes.Effects.Sensations
{
    public class MuscleDirectionGroups
    {
        public readonly Muscle[] Front;
        public readonly Muscle[] Back;
        public readonly Muscle[] Left;
        public readonly Muscle[] Right;
        public readonly Muscle[] Up;
        public readonly Muscle[] Down;
        public readonly Muscle[] All = Muscle.All;

        /*
            For reference:


                    Front                            Back
            ------------------------         ------------------------
                Arm_R        Arm_L               


            Pectoral_R  Pectoral_L            Dorsal_R  Dorsal_L


            Abdominal_R  Abdominal_L          Lumbar_R  Lumbar_L
        */

        public MuscleDirectionGroups(
            Muscle[]? front = null,
            Muscle[]? back = null,
            Muscle[]? left = null,
            Muscle[]? right = null,
            Muscle[]? up = null,
            Muscle[]? down = null
        )
        {
            Front = front ?? Muscle.Front;
            Back  = back  ?? Muscle.Back;
            Left  = left  ?? OWOMuscles.MuscleGroups["leftMuscles"];
            Right = right ?? OWOMuscles.MuscleGroups["rightMuscles"];
            Up    = up    ?? [Muscle.Arm_L, Muscle.Arm_R, Muscle.Pectoral_L, Muscle.Pectoral_R, Muscle.Dorsal_L, Muscle.Dorsal_L];
            Down  = down  ?? [Muscle.Abdominal_L, Muscle.Abdominal_R, Muscle.Lumbar_L, Muscle.Lumbar_R];
        }
    }
}
