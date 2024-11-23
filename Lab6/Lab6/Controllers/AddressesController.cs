using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Lab6.Data;

namespace Lab6.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AddressesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAddresses()
    {
        var addresses = _context.Addresses.ToList();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public IActionResult GetAddress(int id)
    {
        var address = _context.Addresses.Find(id);
        if (address == null)
            return NotFound();
        return Ok(address);
    }
}
