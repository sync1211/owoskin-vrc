namespace OWOVRC.UI.Classes.Proxies
{
    public class HostEntry
    {
        public string IP { get; }
        public string? HostName { get; }

        public HostEntry(string ip, string? hostName)
        {
            IP = ip;
            HostName = hostName;
        }

        public override string ToString()
        {
            if (HostName != null)
            {
                return $"{HostName} <{IP}>";
            }
            return IP;
        }
    }
}
