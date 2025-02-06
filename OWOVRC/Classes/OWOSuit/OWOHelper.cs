using OWOGame;
using Serilog;
using OwoAdvancedSensationBuilder.manager;
using static OwoAdvancedSensationBuilder.manager.AdvancedSensationManager;
using static OwoAdvancedSensationBuilder.manager.AdvancedSensationStreamInstance;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper : IDisposable
    {
        public static bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;

        public string Address { get; set; }
        private readonly List<BakedSensation> Sensations = [];

        private readonly AdvancedSensationManager sensationManager = AdvancedSensationManager.getInstance();

        // Events
        public EventHandler<AdvancedSensationStreamInstance>? OnSensationChange;

        public OWOHelper(string ip = "127.0.0.1")
        {
            Address = ip;
        }

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

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
            else
            {
                Log.Information("Connection to OWO failed!");
            }
        }

        public void Disconnect()
        {
            StopAllSensations();
            OWO.Disconnect();
            Log.Information("Disconnected from OWO!");
        }

        public void AddSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            instance.AfterAdd += HandleSensationAdd;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
        }

        public void AddLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            instance.setLoop(true);
            instance.AfterAdd += HandleSensationAdd;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
        }

        public void UpdateLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            instance.AfterAdd += HandleSensationAdd;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.updateSensation(instance.sensation, name);
        }

        public void AddSensation(string name, Sensation sensation)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation);
            instance.AfterAdd += HandleSensationAdd;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
        }

        public Dictionary<string, AdvancedSensationStreamInstance> GetRunningSensations()
        {
            return sensationManager.getPlayingSensationInstances();
        }

        public void StopAllSensations()
        {
            sensationManager.stopAll();
            Log.Debug("All sensations stopped!");
        }

        public void StopSensation(string name)
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

        private void HandleSensationAdd(AdvancedSensationStreamInstance instance, AddInfo addInfo)
        {
            OnSensationChange?.Invoke(this, instance);
        }

        private void HandleSensationUpdate(AdvancedSensationStreamInstance instance)
        {
            OnSensationChange?.Invoke(this, instance);
        }

        private void HandleSensationRemove(AdvancedSensationStreamInstance instance, RemoveInfo removeInfo)
        {
            OnSensationChange?.Invoke(this, instance);
        }

        public void Dispose()
        {
            StopAllSensations();
            OWO.Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}
