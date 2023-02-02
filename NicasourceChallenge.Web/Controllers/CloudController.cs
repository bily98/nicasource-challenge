using Microsoft.AspNetCore.Mvc;
using NicasourceChallenge.Web._keenthemes.libs;

namespace NicasourceChallenge.App.Controllers
{
    public class CloudController : Controller
    {
        private readonly ILogger<CloudController> _logger;
        private readonly IKTTheme _theme;

        public CloudController(ILogger<CloudController> logger, IKTTheme theme)
        {
            _logger = logger;
            _theme = theme;
        }

        [HttpGet("/cloud")]
        public IActionResult Index()
        {
            return View(_theme.GetPageView("Cloud", "Index.cshtml"));
        }
    }
}
