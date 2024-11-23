using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class CustomerOrdersProducts
{
    public int OrderId { get; set; }
    public required CustomerOrder CustomerOrder { get; set; }
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public int Quantity { get; set; }

    [MaxLength(150)]
    public string? Comments { get; set; }
}