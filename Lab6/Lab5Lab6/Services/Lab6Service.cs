using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;

using Newtonsoft.Json;

namespace Lab5Lab6.Services;

public interface IExternalApiService
{
    Task<string> GetDataFromApiAsync(string endpoint);
    Task<string> PostDataToApiAsync(string endpoint, object data);
}

public class Lab6Service : IExternalApiService
{
    private readonly HttpClient _httpClient;
    private readonly Auth0UserService _auth0UserService;

    public Lab6Service(HttpClient httpClient, Auth0UserService Auth0UserService)
    {
        _httpClient = httpClient;
        _auth0UserService = Auth0UserService;
    }


    public async Task<string> GetDataFromApiAsync(string endpoint)
    {
        if (string.IsNullOrEmpty(_auth0UserService.GetToken()))
        {
            throw new InvalidOperationException("User is not authorized.");
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth0UserService.GetToken());

        var response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Failed to fetch data from {endpoint}. Status code: {response.StatusCode}");
    }

    public async Task<string> PostDataToApiAsync(string endpoint, object data)
    {
        if (string.IsNullOrEmpty(_auth0UserService.GetToken()))
        {
            throw new InvalidOperationException("User is not authorized.");
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth0UserService.GetToken());

        string jsonData = JsonConvert.SerializeObject(data);

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Failed to post data to {endpoint}. Status code: {response.StatusCode}");
    }
}