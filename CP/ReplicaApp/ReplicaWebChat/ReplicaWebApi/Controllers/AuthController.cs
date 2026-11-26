using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ReplicaWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [AllowAnonymous]
        [HttpGet(Name = "Login")]
        public async Task<IActionResult> LoginAsync()
        {
            Response.Cookies.Append(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Name,)}))
                );
        }

        [AllowAnonymous]
        [HttpPost(Name = "Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            //await Response.Cookies.Delete(CookieA)
            re
        }

    }
}
