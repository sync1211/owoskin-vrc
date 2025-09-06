using OWOGame;
using OWOVRC.Classes.Effects.OWI;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OWOVRC.Classes.Effects
{
    // My own implementation of a client for https://github.com/RevoForge/Vrchat-OWO-Integration
    public partial class WorldIntegrator: IDisposable
    {
        // Required for deserialization via System.Text.Json
        [JsonSerializable(typeof(OWISensation[]))]
        public partial class OWISensationJsonContext : JsonSerializerContext;

        // Credits to the authors of OWI
        public static readonly string OWI_GITHUB_URL = "https://github.com/RevoForge/Vrchat-OWO-Integration";

        // Prefix to avoid collisions with other sensations
        private const string OWI_NAME_PREFIX = "World: ";

        // OWOHelper
        private readonly OWOHelper owo;

        // Strings
        private const string OWI_PREFIX = "VRC_OWO_WorldIntegration:";
        private readonly string VRC_LOG_DIR = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\AppData\\LocalLow\\VRChat\\VRChat";

        // Log watcher
        private readonly LogWatcher logWatcher;

        // Settings
        private readonly WorldIntegratorSettings Settings;

        // Status
        public bool IsRunning => logWatcher.IsRunning;

        public WorldIntegrator(WorldIntegratorSettings settings, OWOHelper owo)
        {
            Settings = settings;
            this.owo = owo;
            logWatcher = new("", settings.UpdateInterval);
            logWatcher.OnLogLineRead += HandleNewLogLine;
        }

        public void Start()
        {
            FileInfo? logFile = GetVRCLogFile();
            if (logFile == null)
            {
                throw new FileNotFoundException($"No VRChat log file not found at {VRC_LOG_DIR}!");
            }

            // Update log watcher parameters
            logWatcher.LogPath = logFile.FullName;
            logWatcher.SleepMillis = Settings.UpdateInterval;

            logWatcher.Start();

            Log.Information("OWO World Integration log watcher started!");
            Log.Information("Based on OWOWorldIntegrator by RevoForge & SonoVr: {0}", OWI_GITHUB_URL);
        }

        public void Stop()
        {
            if (logWatcher == null)
            {
                return;
            }

            if (!logWatcher.IsRunning)
            {
                Log.Debug("Not stopping OWI log watcher as it's not running!");
                return;
            }

            logWatcher.Stop();
            Log.Information("OWI log watcher stopped!");
        }

        private FileInfo? GetVRCLogFile()
        {
            // Log directory
            if (!Directory.Exists(VRC_LOG_DIR))
            {
                Log.Error("OWI: VRChat log directory not found!");
                return null;
            }

            // Get latest log file
            DirectoryInfo logDir = new(VRC_LOG_DIR);
            FileInfo[] logFiles = logDir.GetFiles("output_log_*.txt");
            FileInfo? recentLogFile = logFiles.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();

            if (recentLogFile == null)
            {
                Log.Error("OWI: No log files found! Please make sure debug logging is enabled!");
                return null;
            }

            string expectedFileName = $"output_log_{DateTime.Now:yyyy-MM-dd}";
            if (!recentLogFile.Name.StartsWith(expectedFileName))
            {
                Log.Warning("The most recent log file does not match today's date! Make sure to start VRChat BEFORE connecting to OWO!");
            }

            Log.Debug("Found VRChat log at {Log}", recentLogFile.FullName);

            return recentLogFile;
        }

        private void HandleNewLogLine(object? source, string content)
        {
            if (!Settings.Enabled)
            {
                return;
            }

            /*
             * Examples: (Ignoring the preceeding "2024.11.17 16:43:57 Log        -  ")
             * VRC_OWO_WorldIntegration: [{"priority": 3, "sensation": "PaintBall","frequency": 5,"duration": 0.2,"intensity": 100,"rampup":0,"rampdown":0,"exitdelay":0,"Muscles": { "pectoral_L": 100}},{ "sensation": "PaintBall","frequency": 5,"duration": 0.2,"intensity": 100,"rampup":0,"rampdown":0.1,"exitdelay":0,"Muscles": { "pectoral_R": 50,"abdominal_L": 50,"abdominal_R": 25,"arm_L": 25 }}]
             * VRC_OWO_WorldIntegration: [{ "priority": 2,"sensation": "Recoil","frequency": 1,"duration": 1,"intensity": 70,"rampup":0,"rampdown":1.5,"exitdelay":0,"Muscles": { "arm_R": 100,"pectoral_R": 100}}]
             * VRC_OWO_WorldIntegration:[{"priority": 2,"sensation": "Weight","frequency": 35,"duration": 2,"intensity": 50,"rampup":0,"rampdown":0,"exitdelay":0,"Muscles": {"arm_R": 100,"pectoral_R": 50,"dorsal_R": 25}}]
             * VRC_OWO_WorldIntegration:[{"priority": 2,"sensation": "Weight","frequency": 25,"duration": 2,"intensity": 100,"rampup":0.8,"rampdown":0,"exitdelay":0,"Muscles": {"arm_R": 100,"pectoral_R": 50,"dorsal_R": 25}}]
             * VRC_OWO_WorldIntegration:[{"priority": 1,"sensation": "Front Wind","frequency": 100,"duration": 1,"intensity": 70,"rampup":0.2,"rampdown":0.2,"exitdelay":0,"Muscles": {"frontMuscles": 100}}]
             */

            // Non-OWI message
            int owiIndex = content.IndexOf(OWI_PREFIX);
            if (owiIndex == -1)
            {
                return;
            }

            // Remove log prefix
            string contentClean = content[(owiIndex + OWI_PREFIX.Length)..].Trim();

            // Convert to JSON
            OWISensation[]? sensations = JsonSerializer.Deserialize(contentClean, OWISensationJsonContext.Default.OWISensationArray);
            if (sensations == null)
            {
                Log.Warning("Unable to deserialize OWI message! (See debug log for message)");
                Log.Debug("Message: {0}", contentClean);
                return;
            }

            PlaySensations(sensations);
        }

        private int GetSensationIntensity(string sensationName)
        {
            sensationName = sensationName.Trim();
            if (!Settings.EnabledSensations.TryGetValue(sensationName, out int intensity))
            {
                Settings.EnabledSensations.Add(sensationName, 100);
                return 100;
            }

            return intensity;
        }

        private void PlaySensations(OWISensation[] sensations)
        {
            for (int i = 0; i < sensations.Length; i++)
            {
                OWISensation owiSensation = sensations[i];

                if (owiSensation.Sensation.Equals("STOP", StringComparison.CurrentCultureIgnoreCase))
                {
                    StopAllOWISensations();
                    return;
                }

                // Consult blacklist
                int sensationIntensity = GetSensationIntensity(owiSensation.Sensation);
                if (sensationIntensity == 0)
                {
                    Log.Verbose("Ignoring blacklisted sensation {Sensation}", owiSensation.Sensation);
                    continue;
                }

                // Play sensation
                int intensityMultiplier = (int) (Settings.Intensity / 100f) * sensationIntensity;
                Muscle[] muscles = owiSensation.GetMusclesWithIntensity(intensityMultiplier / 100f);
                Sensation sensation = owiSensation.AsSensation();

                owo.AddSensation($"{OWI_NAME_PREFIX}{owiSensation.Sensation}", sensation, muscles);
            }
        }

        private void StopAllOWISensations()
        {
            IEnumerable<string> sensationNames = owo.GetRunningSensations().Keys
                .Where(key => key.StartsWith(OWI_NAME_PREFIX));

            foreach (string sensationName in sensationNames)
            {
                owo.StopSensation(sensationName);
            }
        }

        public void Dispose()
        {
            Stop();

            logWatcher.OnLogLineRead -= HandleNewLogLine;

            GC.SuppressFinalize(this);
        }
    }
}
