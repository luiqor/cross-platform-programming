using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace Lab13.Controllers;

[Route("api/home")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var connectionStatus = new { message = "Підключення успішне" };
        var jsonResponse = JsonSerializer.Serialize(connectionStatus);

        return Content(jsonResponse, "application/json");
    }
}
