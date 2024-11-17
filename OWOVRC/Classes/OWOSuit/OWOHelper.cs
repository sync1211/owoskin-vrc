using OWOGame;
using Serilog;
using System.Net;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper: IDisposable
    {
        public bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;
        public readonly OWOSensations Sensations = new();

        public string Address { get; set; }

        public OWOHelper(string? ip=null)
        {
            Address = ip ?? "127.0.0.1";
        }

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

            //NOTE: Baked sensations are registered in OWOSensations.cs!
            GameAuth auth = GameAuth.Create(Sensations.FallDmg, Sensations.Wind);

            OWO.Configure(auth);

            await OWO.Connect(Address);
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

        //void SendDynamicSensation() => OWO.Send(Sensation.Dagger);

        //Task SendBakedSensation()
        //{
        //    OWO.Send("0"); //This ID belongs to the Ball sensation in the GameAuth
        //    return Task.Delay(100); //We wait for the baked sensation to finish
        //}

        public void AddSensation(Sensation sensation, Muscle[] muscles)
        {
            //TODO: Mixing system
            OWO.Send(sensation, muscles);
        }

        public void AddSensation(Sensation sensation)
        {
            //TODO: Mixing system
            OWO.Send(sensation);
        }

        public void StopAllSensations()
        {
            OWO.Stop();
            Log.Debug("All sensations stopped!");
        }

        public void Dispose()
        {
            StopAllSensations();
            OWO.Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}
