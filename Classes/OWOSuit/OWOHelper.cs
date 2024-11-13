using OWOGame;
using Serilog;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper: IDisposable
    {
        public bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;
        public readonly OWOSensations Sensations = new();

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

            //NOTE: Baked sensations are registered in OWOSensations.cs!
            GameAuth auth = GameAuth.Create(Sensations.FallDmg, Sensations.Wind);

            OWO.Configure(auth);

            await OWO.AutoConnect();
            Log.Information("Connected to OWO!");
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
            OWO.Stop();
            OWO.Disconnect();
            GC.SuppressFinalize(this);
        }
    }
}
