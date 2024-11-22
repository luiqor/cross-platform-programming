using Microsoft.AspNetCore.Mvc;

using Lab5Lab6.Services;

namespace Lab5Lab6.Controllers;

public class SearchCriteria
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int>? ProductIds { get; set; }
    public string? OrderStatusStartsWith { get; set; }
    public string? OrderStatusEndsWith { get; set; }
}

public class ExternalApiController : Controller
{
    private readonly IExternalApiService _externalApiService;

    public ExternalApiController(IExternalApiService externalApiService)
    {
        _externalApiService = externalApiService;
    }

    // Отримати всі адреси
    public async Task<IActionResult> GetAddresses()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Addresses");

            return View("Addresses", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Отримати адресу за ID
    public async Task<IActionResult> GetAddress(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Addresses/{id}");

            return View("AddressDetail", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Отримати всі замовлення
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Orders");

            return View("Orders", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Отримати замовлення за ID
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Orders/{id}");

            return View("OrderDetail", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Отримати всі продукти
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync("/api/Products");

            return View("Products", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Отримати продукт за ID
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var result = await _externalApiService.GetDataFromApiAsync($"/api/Products/{id}");

            return View("ProductDetail", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }

    // Пошук замовлень
    [HttpPost]
    public async Task<IActionResult> SearchOrders(SearchCriteria criteria)
    {
        try
        {
            var result = await _externalApiService.PostDataToApiAsync("/api/Search/orders", criteria);

            return View("SearchResults", result);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = $"Error: {ex.Message}";
            return View("Error");
        }
    }
}
