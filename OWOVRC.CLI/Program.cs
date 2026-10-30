using OWOVRC.Classes.Commandline;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Helpers;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using VRC.OSCQuery;
using Serilog.Core;

namespace OWOVRC.CLI
{
    internal static class Program
    {
        private static readonly CancellationTokenSource cancellationTokenSource = new();
        static void Main()
        {
            // Logger
            LoggingLevelSwitch logLevelSwitch = Classes.Logging.SetUpLogger();

            // Parse commandline switches
            CommandlineSettings.ParseAndApply(logLevelSwitch);

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

            Log.Debug("Importing settings...");

            // Get Settings
            ConnectionSettings settings = SettingsHelper
                .LoadSettingsFromFile("connection.json", "connection", SettingsJsonContext.Default.ConnectionSettings) ?? new();
            VelocityEffectSettings velocitySettings = SettingsHelper
                .LoadSettingsFromFile("velocity.json", "colliders effect", SettingsJsonContext.Default.VelocityEffectSettings) ?? new();
            CollidersEffectSettings collisionSettings = SettingsHelper
                .LoadSettingsFromFile("colliders.json", "velocity effect", SettingsJsonContext.Default.CollidersEffectSettings) ?? new();
            OSCPresetsSettings presetsSettings = SettingsHelper
                .LoadSettingsFromFile("oscPresets.json", "OSC presets", SettingsJsonContext.Default.OSCPresetsSettings) ?? new();
            WorldIntegratorSettings owiSettings = SettingsHelper
                .LoadSettingsFromFile("owi.json", "OWI integration", SettingsJsonContext.Default.WorldIntegratorSettings) ?? new();
#if !TARGET_LINUX
            AudioEffectSettings audioSettings = SettingsHelper
                .LoadSettingsFromFile("audio.json", "Audio", SettingsJsonContext.Default.AudioEffectSettings) ?? new();
#endif
            // Prepare OWOHelper
            Log.Debug("Preparing OSC listener...");
            OWOHelper owo = new(settings.OWOAddress);

            // Prepare effects
            Log.Debug("Preparing effects...");
            OSCEffectBase[] effects = [
                new Colliders(owo, collisionSettings),
                new VelocityEffect(owo, velocitySettings),
                new OSCPresetTrigger(owo, presetsSettings)
            ];

            // Create OSCQueryHelper if enabled
            int oscPort = settings.OSCPort;
            if (settings.UseOSCQuery)
            {
                oscPort = Extensions.GetAvailableUdpPort();
            }

            // Start OSC listener
            Log.Information("Starting OSC receiver...");
            OSCReceiver receiver = new(oscPort, true, "OWOVRC-CLI");
            receiver.Start();

            // Register OSC effects
            RegisterEffects(effects, receiver);

            // Set up World Integrator
            Log.Debug("Preparing OWI...");
            WorldIntegrator owi = new(owiSettings, owo);

            // Set up Audio effect

#if TARGET_LINUX
#warning Audio effect is disabled for Linux targets!
Log.Information("Audio effect is not yet supported on Linux!");
#else
            AudioEffect? audio = null;

            // Set up audio effects
            Log.Debug("Preparing audio effects...");
            audio = new(owo, audioSettings);
#endif

            // Set up cancel & exit logic
            Console.CancelKeyPress += CancellationHandler;
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            
            try
            {
                // Wait for the connection to be ready
                Task<bool> task = ConnectToVRChat(receiver, settings.OSCQuery_MaxWait, settings.OSCQuery_RefreshInterval, cancellationToken);
                task.Wait();

                if (!task.Result)
                {
                    return;
                }

                // Start World Integrator
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

                // Start Audio effect
#if !TARGET_LINUX
                if (audioSettings.Enabled)
                {
                    audio?.Start();
                }
#endif

                // Start main task
                Log.Information("Starting MainLoop...");
                Task.Run(() => MainLoop(owo, cancellationToken)).Wait();
            }
            finally
            {
                // Unregister osc effects
                UnregisterEffects(effects, receiver);

                // Stop everything
                owi.Stop();
#if !TARGET_LINUX
                audio.Stop();
#endif

                // Clean up
                receiver.Dispose();
                owo.Disconnect();
                owo.Dispose();
                owi.Dispose();
#if !TARGET_LINUX
                audio.Dispose();
#endif

                // Dispose effects
                Log.Debug("Cleaning up events..");
                for (int i = 0; i < effects.Length; i++)
                {
                    effects[i].Dispose();
                }

                Log.Information("Goodbye!");
            }
        }

        public static async Task<bool> ConnectToVRChat(OSCReceiver receiver, int maxwait, int refreshInterval, CancellationToken cancellationToken = default)
        {
            bool result = await receiver.WaitForVRChatClientConnected(maxwait, refreshInterval, cancellationToken);
            if (!result)
            {
                Log.Error("Unable to find any VRChat clients via OSCQuery!");
                return false;
            }

            Log.Information("Connected to VRChat!");
            return true;
        }

        public static async Task MainLoop(OWOHelper owo, CancellationToken cancellationToken = default)
        {
            bool connectResult = await owo.Connect() //FIXME: OWO.Connect() does not accept a cancellationToken
                .ConfigureAwait(false);

            if (!connectResult) {
                return;
            }

            try
            {
                await Task.Delay(-1, cancellationToken)
                    .ConfigureAwait(false);
            }
            finally
            {
                Log.Information("Shutting down...");
            }
        }

        private static void CancellationHandler(object? sender, ConsoleCancelEventArgs args)
        {
            Log.Information("Shutdown requested!");
            cancellationTokenSource.Cancel();

            args.Cancel = true;
        }

        public static void UnregisterEffects(OSCEffectBase[] effects, OSCReceiver receiver)
        {
            foreach (OSCEffectBase effect in effects)
            {
                effect.UnregisterCallbacks(receiver);
            }
        }

        public static void RegisterEffects(OSCEffectBase[] effects, OSCReceiver receiver)
        {
            foreach (OSCEffectBase effect in effects)
            {
                effect.RegisterCallbacks(receiver);
            }
        }
    }
}
