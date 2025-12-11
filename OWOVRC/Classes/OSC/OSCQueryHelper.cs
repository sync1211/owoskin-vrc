using Serilog;
using VRC.OSCQuery;

namespace OWOVRC.Classes.OSC
{
    public class OSCQueryHelper : IDisposable
    {
        public readonly string Name;
        private readonly OSCQueryService service;

        //WARNING: DO NOT put any "." in the service name or else VRChat will ignore it!
        public OSCQueryHelper(int udpPort, string name = "OWOVRC")
        {
            Random random = new();
            int suffix = random.Next(1000, 9999);
            this.Name = $"{name}-{suffix}";

            int tcpPort = Extensions.GetAvailableTcpPort();
            Log.Debug("OSCQuery TCP port: {Port}", tcpPort);
            Log.Information("OSCQuery started with name: {Name}", this.Name);

            service = new OSCQueryServiceBuilder()
                .WithTcpPort(tcpPort)
                .WithUdpPort(udpPort)
                .WithServiceName(name)
                .WithDefaults()
                .Build();
        }

        public HashSet<OSCQueryServiceProfile> GetServices()
        {
            service.RefreshServices();
            return service.GetOSCQueryServices();
        }

        private static bool IsVRChatService(OSCQueryServiceProfile profile)
        {
            return profile.name.StartsWith("VRChat")
                && profile.serviceType == OSCQueryServiceProfile.ServiceType.OSCQuery;
        }

        public IEnumerable<OSCQueryServiceProfile> GetVRChatClients()
        {
            return GetServices().Where(IsVRChatService);
        }

        public async Task<bool> ConnectToService(OSCQueryServiceProfile profile)
        {
            Log.Information("Connecting to service {ServiceName}...", profile.name);

            string vrcURL = $"http://{profile.address}:{profile.port}/";
            Log.Debug("Connecting to {URL}...", vrcURL);

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.GetAsync(vrcURL);

                if (!response.IsSuccessStatusCode)
                {
                    Log.Warning("Unable to connect! Status code: {Status} - ", response.StatusCode, await response.Content.ReadAsStringAsync());
                    return false;
                }

                Log.Debug("OSCQuery connected!");
                return true;
            }
        }

        public async Task<IEnumerable<OSCQueryServiceProfile>> WaitForVRChat(int maxwait, int refreshInterval, CancellationToken cancellationToken = default)
        {
            Log.Information("Searching for VRChat client... (Max: {Maxwait} seconds)", maxwait / 1000);

            DateTime startTime = DateTime.Now;

            string[] spinner = ["/", "-", "\\", "|"];
            int i = 0;

            while ((DateTime.Now - startTime).TotalMilliseconds <= maxwait)
            {
                IEnumerable<OSCQueryServiceProfile> services = GetVRChatClients();

                if (services.Any())
                {
                    Log.Information("VRChat client(s) found!");
                    return services;
                }


                try
                {
                    await Task.Delay(refreshInterval, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }

            Log.Warning("Failed to detect VRChat client!");

            return [];
        }

        public void AddEndpoint(string path, string type)
        {
            service.AddEndpoint(path, type, Attributes.AccessValues.WriteOnly);
        }

        public void RemoveEndpoint(string path)
        {
            service.RemoveEndpoint(path);
        }

        public void AdvertiseService()
        {
            service.AdvertiseOSCService(this.Name);
        }

        public void Dispose()
        {
            service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}