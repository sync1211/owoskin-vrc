using System.ComponentModel;

namespace OWOVRC.UI.Classes.Proxies
{
    internal class OWISensationListEntry
    {
        public string Sensation { get; }
        public int Intensity
        {
            get
            {
                return intensity;
            }
            set
            {
                intensity = Math.Clamp(value, 0, 200);
                Items[Sensation] = intensity;
            }
        }
        [Browsable(false)]
        private int intensity;

        [Browsable(false)]
        private readonly Dictionary<string, int> Items;

        public OWISensationListEntry(Dictionary<string, int> items, string sensation, int intensity)
        {
            Items = items;
            Sensation = sensation;
            Intensity = intensity;
        }
    }
}
