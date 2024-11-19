
using Newtonsoft.Json.Linq;
using OWOGame;
using OWOVRC.Classes.Effects.OWI;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Settings;
using Serilog;
using System.IO;

namespace OWOVRC.Classes.Effects
{
    // My own implementation of a client for https://github.com/RevoForge/Vrchat-OWO-Integration
    public class WorldIntegrator: IDisposable
    {
        // Credits to the authors of OWI
        public static readonly string OWI_GITHUB_URL = "https://github.com/RevoForge/Vrchat-OWO-Integration";

        // OWOHelper
        private readonly OWOHelper owo;

        // Strings
        private const string OWI_PREFIX = "VRC_OWO_WorldIntegration:";
        private readonly string VRC_LOG_DIR = $"{Environment.GetEnvironmentVariable("USERPROFILE")}\\AppData\\LocalLow\\VRChat\\VRChat";

        // File watcher
        private FileSystemWatcher? Watcher;
        private int LinesRead;

        // Settings
        private readonly WorldIntegratorSettings Settings;

        public WorldIntegrator(WorldIntegratorSettings settings, OWOHelper owo)
        {
            Settings = settings;
            this.owo = owo;
        }

        public void Start()
        {
            FileInfo? logFile = GetVRCLogFile();
            if (logFile == null)
            {
                throw new FileNotFoundException($"No VRChat log file not found at {VRC_LOG_DIR}!");
            }

            Watcher = CreateFileWatcher(logFile);
            Watcher.Changed += OnLogChange;

            Log.Information("OWO World Integration log watcher started!");
            Log.Information("Based on OWOWorldIntegrator by RevoForge & SonoVr: {0}", OWI_GITHUB_URL);
        }

        public void Stop()
        {
            if (Watcher == null)
            {
                return;
            }

            Log.Information("OWI log watcher stopped!");
            Watcher.Changed -= OnLogChange;

            Watcher.Dispose();
            Watcher = null;
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

            Log.Debug("Found VRChat log at {log}", recentLogFile.FullName);

            return recentLogFile;
        }

        private FileSystemWatcher CreateFileWatcher(FileInfo logFile)
        {
            return new()
            {
                Path = logFile.Directory!.FullName,
                NotifyFilter = NotifyFilters.LastWrite,
                EnableRaisingEvents = true,
                Filter = logFile.Name
            };
        }

        private void OnLogChange(object source, FileSystemEventArgs e)
        {
            if (!Settings.Enabled && e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            string content;
            using (FileStream fileStream = new(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new(fileStream))
                {
                    content = streamReader.ReadToEnd();
                }
            }

            string[] lines = content.Split(Environment.NewLine);
            string[] owoLines = lines.Where(lines => lines.Contains(OWI_PREFIX)).ToArray();

            // Ignore the first lines
            if (LinesRead == 0)
            {
                LinesRead = owoLines.Length;
                return;
            }

            if (owoLines.Length <= LinesRead)
            {
                Log.Debug("No change in effect lines! ({0} <= {1})", lines.Length, LinesRead);
                return;
            }

            ProcessLogLine(owoLines[^1]);
        }

        public void ProcessLogLine(string content)
        {
            /*
             * Examples: (Ignoring the preceeding "2024.11.17 16:43:57 Log        -  ")
             * VRC_OWO_WorldIntegration: [{"priority": 3, "sensation": "PaintBall","frequency": 5,"duration": 0.2,"intensity": 100,"rampup":0,"rampdown":0,"exitdelay":0,"Muscles": { "pectoral_L": 100}},{ "sensation": "PaintBall","frequency": 5,"duration": 0.2,"intensity": 100,"rampup":0,"rampdown":0.1,"exitdelay":0,"Muscles": { "pectoral_R": 50,"abdominal_L": 50,"abdominal_R": 25,"arm_L": 25 }}]
             * VRC_OWO_WorldIntegration: [{ "priority": 2,"sensation": "Recoil","frequency": 1,"duration": 1,"intensity": 70,"rampup":0,"rampdown":1.5,"exitdelay":0,"Muscles": { "arm_R": 100,"pectoral_R": 100}}]
             * VRC_OWO_WorldIntegration:[{"priority": 2,"sensation": "Weight","frequency": 35,"duration": 2,"intensity": 50,"rampup":0,"rampdown":0,"exitdelay":0,"Muscles": {"arm_R": 100,"pectoral_R": 50,"dorsal_R": 25}}]
             * VRC_OWO_WorldIntegration:[{"priority": 2,"sensation": "Weight","frequency": 25,"duration": 2,"intensity": 100,"rampup":0.8,"rampdown":0,"exitdelay":0,"Muscles": {"arm_R": 100,"pectoral_R": 50,"dorsal_R": 25}}]
             * VRC_OWO_WorldIntegration:[{"priority": 1,"sensation": "Front Wind","frequency": 100,"duration": 1,"intensity": 70,"rampup":0.2,"rampdown":0.2,"exitdelay":0,"Muscles": {"frontMuscles": 100}}]
             */

            // Non-OWI message
            if (!content.Contains(OWI_PREFIX))
            {
                return;
            }

            // Remove log prefix
            int owiIndex = content.IndexOf(OWI_PREFIX);
            string contentClean = content.Substring(owiIndex + OWI_PREFIX.Length).Trim();

            // Convert to JSON
            JArray data = JArray.Parse(contentClean);

            OWISensation[] sensations = CreateSensationsFromJSON(data);
            PlaySensations(sensations);
        }

        private OWISensation[] CreateSensationsFromJSON(JArray data)
        {
            List<OWISensation> sensations = new();
            foreach (JObject dataEntry in data)
            {
                OWISensation? message = dataEntry.ToObject<OWISensation>();
                if (message == null)
                {
                    Log.Warning("Unable to deserialize OWI message! (See debug log for message)");
                    Log.Debug("Message: {0}", dataEntry);
                    continue;
                }

                sensations.Add(message);
            }

            return sensations.ToArray();
        }

        private void PlaySensations(OWISensation[] sensations)
        {
            foreach(OWISensation owiSensation in sensations)
            {
                Muscle[] muscles = owiSensation.GetMusclesWithIntensity();
                Sensation sensation = owiSensation.AsSensation();

                owo.AddSensation(sensation, muscles);
            }
        }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Stop();
            GC.SuppressFinalize(this);
        }
    }
}
