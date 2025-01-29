using OWOVRC.Classes;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using OWOVRC.CLI.Classes;
using Serilog;
using Serilog.Core;

namespace OWOVRC.CLI
{
    internal static class Program
    {
        static void Main()
        {
            // Check if running as admin
            //NOTE: The point of the CLI version is to run unattended / as part of a script, so waiting
            //      for user input is counter-productive. Though so is running as admin, so show this annoying
            //      warning to, hopefully, make the user see reason.
            //      (If they *really* need to run as admin, they should know how to get around this confirmation)
            if (AdminDetection.IsRunningAsAdmin())
            {
                Console.WriteLine("/!\\ This application is not intended to be run as administrator! /!\\");
                Console.WriteLine("If you encounter permission errors, please file an issue on GitHub instead!");
                Console.WriteLine($"{Environment.NewLine}Please re-launch this application as unprivileged user or press ENTER to proceed...");
                Console.ReadLine();
            }

            // Logger
            LoggingLevelSwitch logLevel = Logging.SetUpLogger();
#if DEBUG
            logLevel.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
#else
            logLevel.MinimumLevel = Serilog.Events.LogEventLevel.Information;
#endif

            Log.Debug("Preparing...");
            Log.Debug("Importing settings...");

            // Get Settings
            ConnectionSettings settings = SettingsHelper
                .LoadSettingsFromFile("connection.json", "connection", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings) ?? new();
            VelocityEffectSettings velocitySettings = SettingsHelper
                .LoadSettingsFromFile("velocity.json", "colliders effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings) ?? new();
            CollidersEffectSettings collisionSettings = SettingsHelper
                .LoadSettingsFromFile("colliders.json", "velocity effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings) ?? new();
            OSCPresetsSettings presetsSettings = SettingsHelper
                .LoadSettingsFromFile("oscPresets.json", "OSC presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings) ?? new();
            WorldIntegratorSettings owiSettings = SettingsHelper
                .LoadSettingsFromFile("owi.json", "OWI integration", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings) ?? new();
            // AudioEffectSettings audioSettings = SettingsHelper
            //    .LoadSettingsFromFile("audio.json", "Audio", SettingsHelper.AudioEffectSettingsContext.Default.AudioEffectSettings) ?? new();

            // Prepare OWOHelper
            Log.Debug("Preparing OSC listener...");
            OWOHelper owo = new(settings.OWOAddress);

            // Prepare effects
            Log.Debug("Preparing effects...");
            OSCEffectBase[] effects = [
                new Colliders(owo, collisionSettings),
                new Velocity(owo, velocitySettings),
                new OSCPresetTrigger(owo, presetsSettings)
            ];

            // Set up World Integrator
            Log.Debug("Preparing OWI...");
            WorldIntegrator owi = new(owiSettings, owo);
            if (owiSettings.Enabled)
            {
                try
                {
                    owi.Start();
                }
                catch (FileNotFoundException)
                {
                    Log.Warning("OWI client failed to initialize!");
                }
            }

            // Set up audio effects
            Log.Debug("Preparing audio effects...");
            //AudioEffect audio = new(owo, audioSettings);
            //if (audioSettings.Enabled)
            //{
            //    audio.Start();
            //}

            // Start OSC listener
            Log.Information("Starting OSC receiver...");
            OSCReceiver receiver = new(settings.OSCPort);
            RegisterSensations(effects, receiver);
            receiver.Start();

            // Start main task
            try
            {
                Log.Information("Starting MainLoop...");
                Task.Run(() => MainLoop(owo)).Wait();
            }
            finally
            {
                // Unregister osc effects
                UnregisterEffects(effects, receiver);

                // Stop everything
                owi.Stop();
                //audio.Stop();

                // Clean up
                receiver.Dispose();
                owo.Dispose();
                owi.Dispose();
                //audio.Dispose();
            }
        }

        public static async Task MainLoop(OWOHelper owo)
        {
            await owo.Connect();
            try
            {
                await Task.Delay(-1);
            }
            finally
            {
                Log.Information("Quit");
            }
        }

        public static void RegisterSensations(OSCEffectBase[] effects, OSCReceiver receiver)
        {
            foreach (OSCEffectBase effect in effects)
            {
                receiver.OnMessageReceived += effect.OnOSCMessageReceived;
                effect.RegisterSensations();
            }
        }

        public static void UnregisterEffects(OSCEffectBase[] sensations, OSCReceiver receiver)
        {
            foreach (OSCEffectBase sensation in sensations)
            {
                receiver.OnMessageReceived -= sensation.OnOSCMessageReceived;
            }
        }
    }
}
