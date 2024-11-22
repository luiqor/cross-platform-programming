using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Lab6.Data;

namespace Lab6.Controllers;

public class SearchCriteria
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int>? ProductIds { get; set; }
    public string? OrderStatusStartsWith { get; set; }
    public string? OrderStatusEndsWith { get; set; }
}


[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("orders")]
    public IActionResult SearchOrders([FromBody] SearchCriteria criteria)
    {
        var query = _context.CustomerOrders
            .Include(o => o.Customer)
            .Include(o => o.CustomerOrdersProducts)
            .ThenInclude(cop => cop.Product)
            .AsQueryable();

        if (criteria.StartDate.HasValue)
        {
            query = query.Where(o => o.DateOrderPlaced >= criteria.StartDate.Value);
        }

        if (criteria.EndDate.HasValue)
        {
            query = query.Where(o => o.DateOrderPlaced <= criteria.EndDate.Value);
        }

        if (criteria.ProductIds != null && criteria.ProductIds.Any())
        {
            query = query.Where(o => o.CustomerOrdersProducts.Any(cop => criteria.ProductIds.Contains(cop.ProductId)));
        }

        if (!string.IsNullOrEmpty(criteria.OrderStatusStartsWith))
        {
            query = query.Where(o => o.OrderStatusCode.StartsWith(criteria.OrderStatusStartsWith));
        }

        if (!string.IsNullOrEmpty(criteria.OrderStatusEndsWith))
        {
            query = query.Where(o => o.OrderStatusCode.EndsWith(criteria.OrderStatusEndsWith));
        }

        var results = query.ToList();

        return Ok(results);
    }
}
