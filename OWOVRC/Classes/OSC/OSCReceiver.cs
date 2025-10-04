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
            receiver = OscServer.GetOrCreate(Port);
            receiver.AddMonitorCallback(MessageReceived);
        }

        public void Start()
        {
            try
            {
                receiver.Start();
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to start OSC listener on port {Port}! Make sure the port is not in use!", Port);
                return;
            }
            IsRunning = true;
            Log.Information("OSC listener started on port {Port}!", Port);
        }

        private void MessageReceived(BlobString address, OscMessageValues values)
        {
            string addressString = address.ToString();
            if (!addressString.StartsWith(OSC_ADDRESS, StringComparison.CurrentCultureIgnoreCase))
            {
                Log.Verbose("Ignoring non-vrchat message at {Address}", addressString);
                return;
            }

            // Remove OSC prefix
            addressString = addressString[OSC_ADDRESS.Length..];

            if (values.ElementCount == 0)
            {
                Log.Verbose("Message at {Address} does not include any values, ignoring.", addressString);
                return;
            }

            OnMessageReceived?.Invoke(this, new OSCMessage(addressString, values));
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

        public bool TryAddMessageCallback(string path, Action<OscMessageValues> callback)
        {
            return receiver.TryAddMethod($"{OSC_ADDRESS}{path}", callback);
        }

        public bool TryRemoveMessageCallback(string path, Action<OscMessageValues> callback)
        {
            return receiver.RemoveMethod($"{OSC_ADDRESS}{path}", callback);
        }

        public void Dispose()
        {
            IsRunning = false;
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
