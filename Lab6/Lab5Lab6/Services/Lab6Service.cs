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

    public Lab6Service(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5050");
    }

    public async Task<string> GetDataFromApiAsync(string endpoint)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth0UserService._accessToken);

        var response = await _httpClient.GetAsync(endpoint);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }

        throw new Exception($"Failed to fetch data from {endpoint}. Status code: {response.StatusCode}");
    }

    public async Task<string> PostDataToApiAsync(string endpoint, object data)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Auth0UserService._accessToken);

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
