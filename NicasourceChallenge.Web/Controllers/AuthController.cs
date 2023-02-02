using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace NicasourceChallenge.Web.Controllers
{
    public class AuthController : Controller
    {
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            var userObjectId = (User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier"))?.Value;
            var callbackUrl = Url.Action("Index", "Dashboards", values: null, protocol: Request.Scheme);

            return RedirectToAction("Index", "Dashboards");
        }
    }
}
