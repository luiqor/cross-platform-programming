using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lab5.ViewModels;
using System.Linq;
using System.Security.Claims;
using Auth0.AspNetCore.Authentication;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        private readonly Auth0UserService _auth0UserService;

        public AccountController(Auth0UserService auth0UserService)
        {
            _auth0UserService = auth0UserService;
        }

        public async Task Login(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task SignUp(string returnUrl = "/")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .WithParameter("screen_hint", "signup")
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _auth0UserService.CreateUserAsync(model);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
                }
            }

            return View(model);
        }


        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in 
                .WithRedirectUri(Url.Action("Index", "Home") ?? "/")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserProfileViewModel()
            {
                FullName = User.Claims.FirstOrDefault(c => c.Type == "fullname")?.Value ?? "Not specified",
                Email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "Not specified",
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value ?? "Not specified",
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value ?? string.Empty,
                Username = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value ?? "Not specified"
            });
        }


        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
