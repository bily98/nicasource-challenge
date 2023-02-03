namespace NicasourceChallenge.Web._keenthemes.libs;

internal class KTThemeSettings
{
    public static KTThemeBase Config { get; set; } = new();

    public static void init(IConfiguration configuration)
    {
        Config = configuration.GetSection("Theme").Get<KTThemeBase>() ?? Config;
    }
}