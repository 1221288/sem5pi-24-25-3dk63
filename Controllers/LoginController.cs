using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("api/login")]
        public async Task<IActionResult> Login()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
            return new EmptyResult();
        }

        [HttpGet("api/google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                Type = GetClaimType(claim.Type),
                claim.Value
            });

            return View("UserInfo", claims);
        }

        [HttpGet("api/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private string GetClaimType(string type)
        {
            return type switch
            {
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" => "ID",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" => "Name",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname" => "Given Name",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname" => "Surname",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress" => "Email",
                _ => type
            };
        }
    }
}
