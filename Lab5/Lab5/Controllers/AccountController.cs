using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lab5.Controllers
{
    public class AccountController(Auth0UserService auth0UserService) : Controller
    {
        private readonly Auth0UserService _auth0UserService = auth0UserService;

        [HttpGet]
        public IActionResult Register()
        {
            return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
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

        [HttpGet]
        public IActionResult Login()
        {
            return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userProfile = await _auth0UserService.GetUser(model);
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.NameIdentifier, userProfile.Email),
                        new (ClaimTypes.Name, userProfile.FullName),
                        new (ClaimTypes.Email, userProfile.Email),
                        new ("ProfileImage", userProfile.ProfileImage),
                        new(ClaimTypes.MobilePhone , userProfile.PhoneNumber),
                        new("Username", userProfile.Username)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync("AuthScheme", claimsPrincipal);

                    return RedirectToAction("Profile", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error authenticating user: {ex.Message}");
                }
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = HttpContext.User;

            var profileViewModel = new UserProfileViewModel
            {
                Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "Not specified",
                FullName = user.FindFirst(ClaimTypes.Name)?.Value ?? "Not specified",
                PhoneNumber = user.FindFirst(ClaimTypes.MobilePhone)?.Value ?? "Not specified",
                ProfileImage = user.FindFirst("ProfileImage")?.Value ?? "Not specified",
                Username = user.FindFirst("Username")?.Value ?? "Not specified"
            };

            return View(profileViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
