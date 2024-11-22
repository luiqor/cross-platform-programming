using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Lab6.Data;

namespace Lab6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _context.Products.ToList();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _context.Products
            .Include(p => p.Supplier) // Якщо потрібен постачальник
            .FirstOrDefault(p => p.ProductId == id);
        if (product == null)
            return NotFound();
        return Ok(product);
    }
}
