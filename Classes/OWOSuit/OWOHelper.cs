using OWOGame;
using Serilog;

namespace OWOVRC.Classes.OWOSuit
{
    public partial class OWOHelper: IDisposable
    {
        public bool IsConnected => OWO.ConnectionState == ConnectionState.Connected;

        public async Task Connect()
        {
            Log.Information("Connecting to OWO...");

            BakedSensation ball = BakedSensation.Parse("0~Ball~100,1,100,0,0,0,Impact|5%100~impact-0~");

            GameAuth auth = GameAuth.Create(ball);
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

        public void StopAllSensations() => OWO.Stop();

        public void Dispose()
        {
            OWO.Stop();
            OWO.Disconnect();
        }
    }
}
