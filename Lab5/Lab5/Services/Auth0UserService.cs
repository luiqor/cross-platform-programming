using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;

using Lab5.ViewModels;

using Microsoft.Extensions.Configuration;

using System.Threading.Tasks;

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
}