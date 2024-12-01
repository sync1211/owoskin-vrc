namespace OWOVRC.UI.Classes
{
    public static class WikiHelper
    {
        // Wiki links
        public static readonly string COLLIDERS_WIKI_URL = "https://github.com/sync1211/owoskin-vrc/wiki/VRChat-Avatar-Setup#setting-up-owosuit-colliders-owovrc";
        public static readonly string OSC_PRESETS_WIKI_URL = "https://github.com/sync1211/owoskin-vrc/wiki/VRChat-Avatar-Setup#setting-up-custom-sensation-presets";

        public static void OpenURL(string url)
        {
            System.Diagnostics.Process.Start("explorer", url);
        }
    }
}
