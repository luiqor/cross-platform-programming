using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

using Lab5.ViewModels;

public class Auth0UserService(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public async Task CreateUserAsync(UserViewModel model)
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];
        var audience = _configuration["Auth0:ManagementApiAudience"];

        var tokenClient = new Auth0.AuthenticationApi.AuthenticationApiClient(new System.Uri($"https://{domain}"));
        var tokenResponse = await tokenClient.GetTokenAsync(new Auth0.AuthenticationApi.Models.ClientCredentialsTokenRequest
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            Audience = audience
        });

        var managementClient = new ManagementApiClient(tokenResponse.AccessToken, new System.Uri($"https://{domain}/api/v2"));

        var userCreateRequest = new UserCreateRequest
        {
            Email = model.Email,
            EmailVerified = false,
            Password = model.Password,
            Connection = "Username-Password-Authentication",
            UserMetadata = new
            {
                model.FullName,
                model.PhoneNumber,
                model.Username,
            }
        };

        await managementClient.Users.CreateAsync(userCreateRequest);
    }

    public async Task<UserProfileViewModel> GetUser(UserLoginViewModel model)
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];
        var audience = _configuration["Auth0:ManagementApiAudience"];

        var authClient = new AuthenticationApiClient(new System.Uri($"https://{domain}"));
        var authResponse = await authClient.GetTokenAsync(new ResourceOwnerTokenRequest
        {
            Audience = audience,
            ClientId = clientId,
            ClientSecret = clientSecret,
            Realm = "Username-Password-Authentication",
            Username = model.Email,
            Password = model.Password,
            Scope = "openid profile email"
        });

        var managementClient = new ManagementApiClient(authResponse.AccessToken, new System.Uri($"https://{domain}/api/v2"));

        var userInfo = await authClient.GetUserInfoAsync(authResponse.AccessToken);
        var user = await managementClient.Users.GetAsync(userInfo.UserId);

        return new UserProfileViewModel
        {
            Email = user.Email,
            FullName = user.UserMetadata["FullName"]?.ToString() ?? "Not specified",
            PhoneNumber = user.UserMetadata["PhoneNumber"]?.ToString() ?? "Not specified",
            Username = user.UserMetadata["Username"]?.ToString() ?? "Not specified",
            ProfileImage = user.Picture?.ToString() ?? "Not specified",
        };
    }
}