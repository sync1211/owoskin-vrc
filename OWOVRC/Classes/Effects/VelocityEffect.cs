using OWOVRC.Classes.Effects.Sensations;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;

namespace OWOVRC.Classes.Effects
{
    public class VelocityEffect: OSCSpeedEffectBase
    {
        // Sensations
        private readonly WindSensation windSensation;

        // Settings
        public readonly VelocityEffectSettings Settings;

        public VelocityEffect(OWOHelper owo, VelocityEffectSettings settings): base(owo, settings)
        {
            this.Settings = settings;
            windSensation = new WindSensation(0.3f);

            owo.OnCalculationCycle += OnTimerElapsed;
        }

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            ProcessWindHaptics();
        }

        private void ProcessWindHaptics()
        {
            // Speed too low
            if (Speed < Settings.MinSpeed)
            {
                //Log.Debug("Speed below threshold: {speed} < {threshold}", Speed, Threshold);

                // Stop sensations
                if (LastSpeed > Settings.MinSpeed)
                {
                    LastSpeed = -1;
                    LastVelX = 0;
                    LastVelY = 0;
                    LastVelZ = 0;
                }

                if (owo.GetRunningSensations().ContainsKey(WindSensation._Name))
                {
                    owo.StopSensation(WindSensation._Name);
                }
                return;
            }

            if ((IsGrounded && Settings.IgnoreWhenGrounded) || (IsSeated && Settings.IgnoreWhenSeated))
            {
                owo.StopSensation(WindSensation._Name, true);
                return;
            }

            double speedCapped = Math.Min(Speed, Settings.MaxSpeed);
            int speedPercent = (int)(100 * (speedCapped / Settings.MaxSpeed));

            // Send sensation to vest
            PlayWindSensation(speedPercent);
        }

        private void PlayWindSensation(int intensity)
        {
            float windVelX = VelX;
            float windVelY = VelY * -1;
            float windVelZ = VelZ * -1;

            windSensation.UpdateDirection(windVelX, windVelY, windVelZ, intensity / 4);

            windSensation.Play(owo, Settings.Priority);
        }

        public override void Stop()
        {
            base.Stop();

            owo.StopSensation(WindSensation._Name);

            Log.Debug("Velocity effect reset!");
        }

        public override void Dispose()
        {
            owo.OnCalculationCycle -= OnTimerElapsed;
            GC.SuppressFinalize(this);
        }
    }
}
