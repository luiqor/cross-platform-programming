using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

using Lab13.Models;
using Lab13.Services;

namespace Lab13.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(Auth0UserService auth0UserService) : ControllerBase
{
    private readonly Auth0UserService _auth0UserService = auth0UserService;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var tokenResponse = await _auth0UserService.AuthenticateUser(model);

            if (string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                return Unauthorized(new { error = "Invalid credentials" });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true if using HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            };
            Response.Cookies.Append("AuthToken", tokenResponse.AccessToken, cookieOptions);

            return Ok(new
            {
                message = "Login successful"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = $"Error authenticating user: {ex.Message}" });
        }
    }
}
