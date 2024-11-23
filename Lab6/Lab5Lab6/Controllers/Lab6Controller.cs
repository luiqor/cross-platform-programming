using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5Lab6.Services;
using System.Text.Json;

namespace Lab5Lab6.Controllers;


[Authorize]
public class Lab6Controller : Controller
{
    private readonly IExternalApiService _externalApiService;

    public Lab6Controller(IExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    [HttpGet]
    public async Task<IActionResult> Addresses()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Addresses");
            var data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(result);
            return View("Addresses", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Address(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Addresses/{id}");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
            return View("Address", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Orders()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Orders");
            var data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(result);
            return View("Orders", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Order(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Orders/{id}");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
            return View("Order", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Products()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Products");
            var data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(result);
            return View("Products", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Product(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Products/{id}");
            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(result);
            return View("Product", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult SearchOrders()
    {
        return View("SearchOrders");
    }

    [HttpPost]
    public async Task<IActionResult> SearchOrders(SearchCriteriaViewModel criteria)
    {
        try
        {
            var result = await _externalApiService.PostDataToApiAsync("/api/Search/orders", criteria);
            var data = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(result);
            return View("SearchResults", data);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }
}
