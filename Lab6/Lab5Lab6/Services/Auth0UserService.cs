using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

using Lab5Lab6.Models;
using Lab5Lab6.Models.Constants;

namespace Lab5Lab6.Services;

public class Auth0UserService
{
    private readonly string _domain;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _audience;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Auth0UserService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

        _domain = _configuration["Auth0:Domain"] ?? throw new ArgumentNullException(nameof(configuration));
        _clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException(nameof(configuration));
        _clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException(nameof(configuration));
        _audience = _configuration["Auth0:ManagementApiAudience"] ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task CreateUser(UserRegisterViewModel model)
    {

        AuthenticationApiClient tokenClient = new(new Uri($"https://{_domain}"));
        AccessTokenResponse tokenResponse = await tokenClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Audience = _audience
        });

        ManagementApiClient managementClient = new(tokenResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

        UserCreateRequest userCreateRequest = new()
        {
            Email = model.Email,
            UserName = model.Username,
            EmailVerified = false,
            Password = model.Password,
            Connection = "Username-Password-Authentication",
            UserMetadata = new
            {
                model.FullName,
                model.PhoneNumber,
            }
        };

        await managementClient.Users.CreateAsync(userCreateRequest);
    }

    public async Task<UserProfileViewModel> GetUser(UserLoginViewModel model)
    {
        AuthenticationApiClient authClient = new(new Uri($"https://{_domain}"));
        AccessTokenResponse authResponse = await authClient.GetTokenAsync(new ResourceOwnerTokenRequest
        {
            Audience = _audience,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Realm = "Username-Password-Authentication",
            Username = model.Email,
            Password = model.Password,
            Scope = "openid profile email"
        });
        SetToken(authResponse.AccessToken);

        ManagementApiClient managementClient = new(authResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));
        UserInfo userInfo = await authClient.GetUserInfoAsync(authResponse.AccessToken);
        User user = await managementClient.Users.GetAsync(userInfo.UserId);

        return new UserProfileViewModel
        {
            Email = user.Email,
            Username = user.UserName,
            FullName = user.UserMetadata?["FullName"]?.ToString() ?? DataValue.Alternative,
            PhoneNumber = user.UserMetadata?["PhoneNumber"]?.ToString() ?? DataValue.Alternative,
            ProfileImage = user.Picture.ToString(),
        };
    }

    public void SetToken(string token)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        var options = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddHours(1)
        };
        httpContext.Response.Cookies.Append("accessToken", token, options);
    }

    public string GetToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new UnauthorizedAccessException("HttpContext is not available.");
        }

        string token = httpContext.Request.Cookies["accessToken"]!;
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("Access token is missing or expired.");
        }

        return token;
    }
}