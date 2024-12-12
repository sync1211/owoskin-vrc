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
        }

        public void Disconnect()
        {
            StopAllSensations();
            OWO.Disconnect();
            Log.Information("Disconnected from OWO!");
        }

        public void AddSensation(Sensation sensation, Muscle[] muscles, string name)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles), false);
            //instance.LastCalculationOfCycle += HandleSensationEnd;

            sensationManager.play(instance);

            //OnSensationChange?.Invoke(this, instance);
        }

        public void AddLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles), true);
            //instance.LastCalculationOfCycle += HandleSensationEnd;

            sensationManager.play(instance);

            //OnSensationChange?.Invoke(this, instance);
        }

        public void UpdateLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            //instance.LastCalculationOfCycle += HandleSensationEnd;

            sensationManager.updateSensation(instance.sensation, name);

            //OnSensationChange?.Invoke(this, instance);
        }

        public void AddSensation(Sensation sensation, string name)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation, false);
            //instance.LastCalculationOfCycle += HandleSensationEnd;

            sensationManager.play(instance);

            //OnSensationChange?.Invoke(this, instance);
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

        //private void HandleSensationEnd(AdvancedSensationStreamInstance instance)
        //{
        //    OnSensationChange?.Invoke(this, instance);
        //}

        public void Dispose()
        {
            StopAllSensations();
            OWO.Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}
