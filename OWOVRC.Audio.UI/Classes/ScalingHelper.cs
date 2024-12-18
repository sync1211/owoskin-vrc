namespace OWOVRC.Audio.UI.Classes
{
    public class ScalingHelper
    {
        public float MaxAmplitude { get; private set; }

        public ScalingHelper(float maxAmplitude = 10)
        {
            this.MaxAmplitude = maxAmplitude;
        }

        public int ToPercentage(float input)
        {
            MaxAmplitude = Math.Max(input, MaxAmplitude);

            if (input == 0)
            {
                return 0;
            }

            // Scale input to be a percentage of maxAmplitude
            float inputPercent = (float) ((float) input / (float) MaxAmplitude) * 100;

            return (int) inputPercent;
        }
    }
}
