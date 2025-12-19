using BuildSoft.OscCore;
using Serilog;
using VRC.OSCQuery;

namespace OWOVRC.Classes.OSC
{
    public partial class OSCReceiver: IDisposable
    {
        public bool IsRunning { get; private set; }
        private bool disposed;

        private const string OSC_ADDRESS = "/avatar/parameters/";
        private readonly OscServer receiver;

        public int Port = 9001;
        private readonly OSCQueryHelper? oscQueryHelper;

        public OSCReceiver(int port = 9001, bool oscQuery = false, string serviceName = "OWOVRC")
        {
            Port = port;
            receiver = OscServer.GetOrCreate(Port);

            if (oscQuery)
            {
                oscQueryHelper = new OSCQueryHelper(port, serviceName);
            }
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            oscQueryHelper?.Dispose();

            receiver.Dispose();
            disposed = true;
        }

        public bool TryAddMessageCallback(string path, Action<OscMessageValues> callback)
        {
            string fullPath = $"{OSC_ADDRESS}{path}";
            oscQueryHelper?.AddEndpoint(fullPath, "float"); // "float" seems to work for all types (VRC does not seem to care and we convert the received value anyways)
            return receiver.TryAddMethod(fullPath, callback);
        }

        public bool TryRemoveMessageCallback(string path, Action<OscMessageValues> callback)
        {
            string fullPath = $"{OSC_ADDRESS}{path}";
            oscQueryHelper?.RemoveEndpoint(fullPath);
            return receiver.RemoveMethod(fullPath, callback);
        }

        public async Task<bool> WaitForVRChatClientConnected(int maxwait, int refreshInterval, CancellationToken cancellationToken = default)
        {
            if (oscQueryHelper == null)
            {
                return IsRunning; // Unable to detect the client -> Everything ok, as long as the receiver is running
            }
            return (await oscQueryHelper.WaitForVRChat(maxwait, refreshInterval, cancellationToken)).Any();
        }

        public void Dispose()
        {
            IsRunning = false;

            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
