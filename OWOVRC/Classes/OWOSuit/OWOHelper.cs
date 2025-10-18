using OWOGame;
using Serilog;
using OwoAdvancedSensationBuilder.manager;
using static OwoAdvancedSensationBuilder.manager.AdvancedSensationStreamInstance;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper : IDisposable
    {
        protected const int INTERVAL = 100;
        private readonly System.Timers.Timer timer;

        public EventHandler? OnCalculationCycle;
        public static bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;

        public string Address { get; set; }

        private readonly AdvancedSensationManager sensationManager = AdvancedSensationManager.getInstance();

        // Events
        public EventHandler<AdvancedSensationStreamInstance>? OnSensationChange;

        public OWOHelper(string ip = "127.0.0.1")
        {
            Address = ip;
            timer = timer = new System.Timers.Timer()
            {
                Interval = INTERVAL,
                AutoReset = true
            };
            timer.Elapsed += OnTimerElapsed;

            timer.Start();
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            OnCalculationCycle?.Invoke(this, EventArgs.Empty);
        }

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

            GameAuth auth = GameAuth.Create();

            OWO.Configure(auth);

            try
            {
                await OWO.Connect(Address)
                    .ConfigureAwait(false);
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
            //instance.OnFirstSensationTick += HandleSensationFirstTick;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
            OnSensationChange?.Invoke(this, instance);
        }

        public void AddLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            instance.setLoop(true);
            //instance.OnFirstSensationTick += HandleSensationFirstTick;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
            OnSensationChange?.Invoke(this, instance);
        }

        public void UpdateLoopedSensation(string name, Sensation sensation, Muscle[] muscles)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation.WithMuscles(muscles));
            //instance.OnFirstSensationTick += HandleSensationFirstTick;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.updateSensation(instance.sensation, name);
            OnSensationChange?.Invoke(this, instance);
        }

        public void AddSensation(string name, Sensation sensation)
        {
            AdvancedSensationStreamInstance instance = new(name, sensation);
            //instance.OnFirstSensationTick += HandleSensationFirstTick;
            instance.AfterUpdate += HandleSensationUpdate;
            instance.AfterRemove += HandleSensationRemove;

            sensationManager.play(instance);
            OnSensationChange?.Invoke(this, instance);
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
            Log.Debug("Sensation {Name} stopped!", name);
        }

        private void StopSensationAfterPlay(string name)
        {
            Dictionary<string, AdvancedSensationStreamInstance> sensations = GetRunningSensations();

            AdvancedSensationStreamInstance? selectedSensation = sensations.GetValueOrDefault(name);
            if (selectedSensation == null)
            {
                Log.Debug("The sensation {0} is not running!", name);
                return;
            }

            selectedSensation.LastCalculationOfCycle += StopSensationInstance;

            Log.Debug("Marked sensation {0} to stop on the next loop", name);
        }

        private void StopSensationInstance(AdvancedSensationStreamInstance instance)
        {
            instance.LastCalculationOfCycle -= StopSensationInstance;

            sensationManager.stopSensation(instance.name);
            Log.Debug("Sensation {Name} stopped!", instance.name);
        }

        //private void HandleSensationFirstTick(AdvancedSensationStreamInstance instance, bool firstCycle)
        //{
        //    OnSensationChange?.Invoke(this, instance);
        //}

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
