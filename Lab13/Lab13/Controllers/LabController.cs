using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab13.Controllers;

[Route("api/lab")]
[ApiController]
[Authorize]
public class LabController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var response = new { message = "Hello from LabController!" };
        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok($"Hello from LabController! You requested ID: {id}");
    }

    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return Ok($"Received value: {value}");
    }
}
