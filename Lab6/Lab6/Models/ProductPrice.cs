using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class ProductPrice
{
    [Key]
    public DateTime DateFrom { get; set; }
    public int ProductId { get; set; }
    public required Product Product { get; set; }
    public decimal Price { get; set; }
}