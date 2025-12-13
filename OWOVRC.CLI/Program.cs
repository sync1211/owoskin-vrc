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
                .LoadSettingsFromFile("connection.json", "connection", SettingsHelper.ConnectionSettingsJsonContext.Default.ConnectionSettings) ?? new();
            VelocityEffectSettings velocitySettings = SettingsHelper
                .LoadSettingsFromFile("velocity.json", "colliders effect", SettingsHelper.VelocityEffectSettingsContext.Default.VelocityEffectSettings) ?? new();
            CollidersEffectSettings collisionSettings = SettingsHelper
                .LoadSettingsFromFile("colliders.json", "velocity effect", SettingsHelper.CollidersEffectSettingsContext.Default.CollidersEffectSettings) ?? new();
            OSCPresetsSettings presetsSettings = SettingsHelper
                .LoadSettingsFromFile("oscPresets.json", "OSC presets", SettingsHelper.OSCPresetsSettingsContext.Default.OSCPresetsSettings) ?? new();
            WorldIntegratorSettings owiSettings = SettingsHelper
                .LoadSettingsFromFile("owi.json", "OWI integration", SettingsHelper.WorldIntegratorSettingsContext.Default.WorldIntegratorSettings) ?? new();
#if !TRIMMING_ENABLED && !TARGET_LINUX
            AudioEffectSettings audioSettings = SettingsHelper
                .LoadSettingsFromFile("audio.json", "Audio", SettingsHelper.AudioEffectSettingsContext.Default.AudioEffectSettings) ?? new();
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
            OSCQueryHelper? oscQueryHelper = null;
            if (settings.UseOSCQuery)
            {
                oscPort = Extensions.GetAvailableUdpPort();
                oscQueryHelper = new OSCQueryHelper(oscPort, "OWOVRC-CLI");
            }

            // Start OSC listener
            Log.Information("Starting OSC receiver...");
            OSCReceiver receiver = new(oscPort);
            receiver.Start();

            // Register OSC effects
            RegisterEffects(effects, receiver);

            // Set up World Integrator
            Log.Debug("Preparing OWI...");
            WorldIntegrator owi = new(owiSettings, owo);

            // Set up Audio effect
            AudioEffect? audio = null;

#if TARGET_LINUX
#warning Audio effect is disabled for Linux targets!
Log.Information("Audio effect is not yet supported on Linux!");
#elif TRIMMING_ENABLED
#warning Audio effect is disabled due to trimming being enabled!
Log.Information("Audio effect is disabled as OWOVRC.CLI has been compiled with trimming enabled!");
#else
            // Set up audio effects
            Log.Debug("Preparing audio effects...");
            audio = new(owo, audioSettings);
#endif


            try
            {
                // Start OSCQuery if enabled
                if (oscQueryHelper != null)
                {
                    Task<bool> task = ConnectToVRChat(oscQueryHelper, settings.OSCQuery_MaxWait, settings.OSCQuery_RefreshInterval);
                    task.Wait();

                    if (!task.Result)
                    {
                        return;
                    }
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
#if !TRIMMING_ENABLED && !TARGET_LINUX
                if (audioSettings.Enabled)
                {
                    audio?.Start();
                }
#endif

                // Start main task
                Log.Information("Starting MainLoop...");
                Task.Run(() => MainLoop(owo)).Wait();
            }
            finally
            {
                // Unregister osc effects
                UnregisterEffects(effects, receiver);

                // Stop everything
                owi.Stop();
#if !TRIMMING_ENABLED && !TARGET_LINUX
                //audio.Stop();
#endif

                // Clean up
                receiver.Dispose();
                owo.Dispose();
                owi.Dispose();
#if !TRIMMING_ENABLED && !TARGET_LINUX
                audio.Dispose();
#endif

                // Dispose effects
                for (int i = 0; i < effects.Length; i++)
                {
                    effects[i].Dispose();
                }
            }
        }

        public static async Task<bool> ConnectToVRChat(OSCQueryHelper helper, int maxwait, int refreshInterval)
        {
            IEnumerable<OSCQueryServiceProfile> clients = await helper.WaitForVRChat(maxwait, refreshInterval);

            OSCQueryServiceProfile? chosenClient = clients.FirstOrDefault();
            if (chosenClient == null)
            {
                Log.Error("Unable to find any VRChat clients via OSCQuery!");
                return false;
            }

            Log.Information("Connected to VRChat!");
            return true;
        }

        public static async Task MainLoop(OWOHelper owo)
        {
            await owo.Connect()
                .ConfigureAwait(false);
            try
            {
                await Task.Delay(-1)
                    .ConfigureAwait(false);
            }
            finally
            {
                Log.Information("Quit");
            }
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
