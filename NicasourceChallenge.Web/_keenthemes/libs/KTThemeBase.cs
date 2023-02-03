namespace NicasourceChallenge.Web._keenthemes.libs;

// Base type class for theme settings
internal class KTThemeBase
{
    public string Name { get; set; } = "";

    public string LayoutDir { get; set; } = "";

    public string Direction { get; set; } = "";

    public bool ModeSwitchEnabled { get; set; } = false;

    public string ModeDefault { get; set; } = "";

    public string AssetsDir { get; set; } = "";

    public KTThemeAssets Assets { get; set; } = new();

    public SortedDictionary<string, SortedDictionary<string, string[]>> Vendors { get; set; } = new();
}