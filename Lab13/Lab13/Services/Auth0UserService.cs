using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

using Lab13.Models;

namespace Lab13.Services;

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

    public async Task<AccessTokenResponse> AuthenticateUser(UserLoginDto model)
    {
        using var httpClient = new HttpClient();
        var requestBody = new
        {
            grant_type = "password",
            username = model.Email,
            password = model.Password,
            scope = "openid profile email",
            client_id = _clientId,
            client_secret = _clientSecret
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_domain}/oauth/token")
        {
            Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
        };

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseContent);

        if (tokenResponse == null)
        {
            throw new Exception("Unknown error occurred.");
        }

        return tokenResponse;
    }

    public async Task CreateUser(UserRegisterDto model)
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

    public async Task<UserProfileDto> GetUser(string accessToken)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var userInfoResponse = await httpClient.GetAsync($"https://{_domain}/userinfo");
        userInfoResponse.EnsureSuccessStatusCode();

        var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
        var userInfo = JObject.Parse(userInfoContent);

        var userId = userInfo["sub"]?.ToString();
        if (string.IsNullOrEmpty(userId))
        {
            throw new Exception("Failed to retrieve user ID from /userinfo.");
        }

        AuthenticationApiClient tokenClient = new(new Uri($"https://{_domain}"));
        AccessTokenResponse tokenResponse = await tokenClient.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            Audience = _audience
        });

        ManagementApiClient managementClient = new(tokenResponse.AccessToken, new Uri($"https://{_domain}/api/v2"));
        var user = await managementClient.Users.GetAsync(userId);

        var userProfile = new UserProfileDto
        {
            Id = user.UserId,
            Email = user.Email,
            FullName = user.UserMetadata?["FullName"]?.ToString() ?? userInfo["name"]?.ToString() ?? string.Empty,
            PhoneNumber = user.UserMetadata?["PhoneNumber"]?.ToString() ?? userInfo["phone_number"]?.ToString() ?? string.Empty,
            ProfileImage = user.Picture ?? userInfo["picture"]?.ToString() ?? string.Empty,
            Username = user.NickName ?? userInfo["nickname"]?.ToString() ?? string.Empty
        };

        return userProfile;
    }
}