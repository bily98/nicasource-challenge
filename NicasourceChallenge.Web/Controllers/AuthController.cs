using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace NicasourceChallenge.Web.Controllers;

public class AuthController : Controller
{
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties(),
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }   
}