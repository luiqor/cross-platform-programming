using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

using Lab5.ViewModels;

namespace Lab5.Services
{
    public class Auth0UserService
    {
        private readonly string _domain;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _audience;
        private readonly IConfiguration _configuration;

        public Auth0UserService(IConfiguration configuration)
        {
            _configuration = configuration;
            _domain = _configuration["Auth0:Domain"] ?? throw new ArgumentNullException(nameof(configuration));
            _clientId = _configuration["Auth0:ClientId"] ?? throw new ArgumentNullException(nameof(configuration));
            _clientSecret = _configuration["Auth0:ClientSecret"] ?? throw new ArgumentNullException(nameof(configuration));
            _audience = _configuration["Auth0:ManagementApiAudience"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateUserAsync(UserViewModel model)
        {

            AuthenticationApiClient tokenClient = new(new Uri($"https://{_domain}"));
            AccessTokenResponse tokenResponse = await tokenClient.GetTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _clientSecret,
                ClientSecret = _clientSecret,
                Audience = _audience
            });

            ManagementApiClient managementClient = new(tokenResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

            UserCreateRequest userCreateRequest = new()
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
            string alternativeValue = "N/A";
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

            ManagementApiClient managementClient = new(authResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));

            UserInfo userInfo = await authClient.GetUserInfoAsync(authResponse.AccessToken);
            User user = await managementClient.Users.GetAsync(userInfo.UserId);


            return new UserProfileViewModel
            {
                Email = user.Email,
                FullName = user.UserMetadata?["FullName"]?.ToString() ?? alternativeValue,
                PhoneNumber = user.UserMetadata?["PhoneNumber"]?.ToString() ?? alternativeValue,
                Username = user.UserMetadata?["Username"]?.ToString() ?? alternativeValue,
                ProfileImage = user.Picture.ToString(),
            };
        }
    }
}