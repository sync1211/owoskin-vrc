using BlobHandles;
using BuildSoft.OscCore;
using Serilog;

namespace OWOVRC.Classes.OSC
{
    public partial class OSCReceiver: IDisposable
    {
        public bool IsRunning { get; private set; }
        private bool disposed;

        private const string OSC_ADDRESS = "/avatar/parameters/";
        private readonly OscServer receiver;

        public EventHandler<OSCMessage>? OnMessageReceived;
        public int Port = 9001;

        public OSCReceiver(int port = 9001)
        {
            Port = port;
            receiver = new OscServer(Port);
        }

        public void Start()
        {
            receiver.AddMonitorCallback(MessageReceived);
            try
            {
                receiver.Start();
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to start OSC listener on port {port}! Make sure the port is not in use!", Port);
                return;
            }
            IsRunning = true;
            Log.Information("OSC listener started on port {port}!", Port);
        }

        private void MessageReceived(BlobString address, OscMessageValues values)
        {
            string addressString = address.ToString();
            if (!addressString.StartsWith(OSC_ADDRESS, StringComparison.CurrentCultureIgnoreCase))
            {
                Log.Verbose("Ignoring non-vrchat message at {address}", addressString);
                return;
            }

            if (values.ElementCount == 0)
            {
                Log.Verbose("Message at {address} does not include any values, ignoring.", addressString);
                return;
            }

            OnMessageReceived?.Invoke(this, new OSCMessage(addressString.Remove(0, OSC_ADDRESS.Length), values));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            receiver.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            IsRunning = false;
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
