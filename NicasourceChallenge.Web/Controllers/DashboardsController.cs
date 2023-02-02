using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.Web.Controllers;

[Authorize]
public class DashboardsController : Controller
{
    private readonly ILogger<DashboardsController> _logger;
    private readonly IKTTheme _theme;

    public DashboardsController(ILogger<DashboardsController> logger, IKTTheme theme)
    {
        _logger = logger;
        _theme = theme;
    }

    [HttpGet("/")]
    [HttpGet("/dashboards")]
    public IActionResult Index()
    {
        return View(_theme.GetPageView("Dashboards", "Index.cshtml"));
    }
}
