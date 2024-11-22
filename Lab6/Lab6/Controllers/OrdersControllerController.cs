using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Lab6.Data;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetOrders()
    {
        var orders = _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.CustomerPaymentMethod)
            .ToList();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetOrder(int id)
    {
        var order = _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.CustomerPaymentMethod)
            .FirstOrDefault(o => o.OrderId == id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }
}
