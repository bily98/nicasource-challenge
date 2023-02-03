using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.Web.Controllers;

public class SystemController : Controller
{
    private readonly ILogger<SystemController> _logger;
    private readonly IKTTheme _theme;

    public SystemController(ILogger<SystemController> logger, IKTTheme theme)
    {
        _logger = logger;
        _theme = theme;
    }

    [HttpGet("/not-found")]
    public IActionResult PageNotFound()
    {
        return View(_theme.GetPageView("System", "NotFound.cshtml"));
    }

    [HttpGet("/error")]
    public IActionResult Error()
    {
        return View(_theme.GetPageView("System", "Error.cshtml"));
    }
}