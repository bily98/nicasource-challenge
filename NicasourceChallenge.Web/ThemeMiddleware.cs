using NicasourceChallenge.Web._keenthemes;
using NicasourceChallenge.Web._keenthemes.libs;

public class ThemeMiddleware
{
    private readonly ILogger<ThemeMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ThemeMiddleware(RequestDelegate next, ILogger<ThemeMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IKTTheme theme, IKTBootstrapBase ktBootstrapBase)
    {
        ktBootstrapBase.Init(theme);

        await _next(context);
    }
}

public static class ThemeMiddlewareExtensions
{
    public static IApplicationBuilder UseThemeMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ThemeMiddleware>();
    }
}