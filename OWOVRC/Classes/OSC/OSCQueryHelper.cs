using Serilog;
using VRC.OSCQuery;

public class OSCQueryHelper: IDisposable
{
    private readonly OSCQueryService oscQueryService;
    private readonly string name;
    public OSCQueryHelper(string name = "OWOVRC")
    {
        this.name = name;
        int udpPort = Extensions.GetAvailableUdpPort();

        oscQueryService = new OSCQueryServiceBuilder()
                .WithUdpPort(udpPort)
                .WithServiceName(this.name)
                .WithDefaults()
                .Build();
    }

    public HashSet<OSCQueryServiceProfile> GetServices()
    {
        oscQueryService.RefreshServices();
        return oscQueryService.GetOSCQueryServices();
    }

    public OSCQueryServiceProfile? GetFirstVRChatService()
    {
        Log.Warning("OSCQuery service initialized with {IP}:{Port} ({A}, {B})", oscQueryService.HostInfo.oscIP, oscQueryService.TcpPort, oscQueryService.HostInfo.oscTransport, oscQueryService.HostInfo.name);
        var services = GetServices();

        foreach (OSCQueryServiceProfile profile in services)
        {
            Log.Debug("Found OSC service: {Name} ({ServiceType}): {IP}:{Port}", profile.name, profile.serviceType, profile.address, profile.port);

            if (profile.name.StartsWith("VRChat") && profile.serviceType == OSCQueryServiceProfile.ServiceType.OSCQuery)
            {
                return profile;
            }
        }

        Log.Warning("VRChat client not found!");

        return null;
    }

    public void RegisterHandlers()
    {
        oscQueryService.AddEndpoint("/avatar/Test", "int", Attributes.AccessValues.ReadWrite);
    }

    public void AdvertiseService()
    {
        oscQueryService.AdvertiseOSCService(this.name);
    }

    public void Dispose()
    {
        oscQueryService.Dispose();
        GC.SuppressFinalize(this);
    }
}