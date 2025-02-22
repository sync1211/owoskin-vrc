using OWOGame;
using Serilog;
using OwoAdvancedSensationBuilder.manager;
using static OwoAdvancedSensationBuilder.manager.AdvancedSensationStreamInstance;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper : IDisposable
    {
        public static bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;

        public string Address { get; set; }

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

            GameAuth auth = GameAuth.Create();

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

        public void StopSensation(string name, bool interrupt = true)
        {
            if (interrupt)
            {
                InterruptSensation(name);
            }
            else
            {
                StopSensationAfterPlay(name);
            }
        }

        private void InterruptSensation(string name)
        {
            sensationManager.stopSensation(name);
            Log.Debug("Sensation {name} stopped!", name);
        }

        private void StopSensationAfterPlay(string name)
        {
            Dictionary<string, AdvancedSensationStreamInstance> sensations = GetRunningSensations();

            AdvancedSensationStreamInstance? selectedSensation = sensations.GetValueOrDefault(name);
            if (selectedSensation == null)
            {
                Log.Warning("The sensation {0} could not be found!", name);
                return;
            }

            selectedSensation.LastCalculationOfCycle += StopSensationInstance;

            Log.Information("Marked sensation {0} to stop on the next loop", name);
        }

        private void StopSensationInstance(AdvancedSensationStreamInstance instance)
        {
            instance.LastCalculationOfCycle -= StopSensationInstance;

            sensationManager.stopSensation(instance.name);
            Log.Debug("Sensation {name} stopped!", instance.name);
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
