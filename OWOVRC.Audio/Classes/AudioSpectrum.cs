using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWOVRC.Audio.Classes
{
    public static class AudioSpectrum
    {
        public static readonly FrequencyRange SubBass = new(16, 60);
        public static readonly FrequencyRange Bass = new(60, 250);
        public static readonly FrequencyRange LowMid = new(250, 500);
        public static readonly FrequencyRange Mid = new(500, 2000);
        public static readonly FrequencyRange HighMid = new(2000, 4000);
        public static readonly FrequencyRange Presence = new(4000, 6000);
        public static readonly FrequencyRange Brilliance = new(6000, 20_000);
    }
}
