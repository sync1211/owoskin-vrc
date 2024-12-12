using OWOGame;
using Serilog;
using OwoAdvancedSensationBuilder.manager;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper : IDisposable
    {
        public static bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;

        public string Address { get; set; }
        private readonly List<BakedSensation> Sensations = [];

        private readonly AdvancedSensationManager sensationManager = AdvancedSensationManager.getInstance();

        public OWOHelper(string ip = "127.0.0.1")
        {
            Address = ip;
        }

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

            //NOTE: Baked sensations are registered in OWOSensations.cs!

            GameAuth auth = GameAuth.Create([.. Sensations]);

            OWO.Configure(auth);

            try
            {
                await OWO.Connect(Address);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to start OWO connection!");
                return;
            }

            if (IsConnected)
            {
                Log.Information("Connected to OWO!");
            }
        }

        public void Disconnect()
        {
            StopAllSensations();
            OWO.Disconnect();
            Log.Information("Disconnected from OWO!");
        }

        public void AddSensation(Sensation sensation, Muscle[] muscles)
        {
            sensationManager.playOnce(sensation.WithMuscles(muscles));
        }

        public void AddSensation(Sensation sensation, Muscle[] muscles, string name)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles), false);
            sensationManager.play(instance);
        }

        public void AddLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles), true);
            sensationManager.play(instance);
        }

        public void UpdateLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            sensationManager.updateSensation(instance.sensation, name);
        }

        public Dictionary<string, AdvancedSensationStreamInstance> GetRunningSensations()
        {
            return sensationManager.getPlayingSensationInstances();
        }

        public void AddSensation(Sensation sensation)
        {
            sensationManager.playOnce(sensation);
        }

        public void AddSensation(Sensation sensation, string name)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation, false);
            sensationManager.play(instance);
        }

        public void StopAllSensations()
        {
            sensationManager.stopAll();
            Log.Debug("All sensations stopped!");
        }

        public void StopLoopedSensation(string name)
        {
            sensationManager.stopSensation(name);
            Log.Debug("Looped sensation {name} stopped!", name);
        }

        public void AddBakedSensation(BakedSensation sensation)
        {
            Log.Verbose("Registering baked sensation {sensation}", sensation);
            Sensations.Add(sensation);
        }

        public void ClearBakedSensations()
        {
            Sensations.Clear();
        }

        public void Dispose()
        {
            StopAllSensations();
            OWO.Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}
