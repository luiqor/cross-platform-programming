using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;

using Lab13.Models;
using Lab13.Constants;
using Newtonsoft.Json;
using System.Text;

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

}