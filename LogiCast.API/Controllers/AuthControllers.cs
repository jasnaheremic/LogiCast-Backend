using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace LogiCast.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthControllers : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        var properties = new AuthenticationProperties { RedirectUri = "http://localhost:5173/dashboard" };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("http://localhost:5173/login");
    }
}