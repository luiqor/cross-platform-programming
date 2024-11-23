using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Supplier
{
    [Key]
    [MaxLength(10)]
    public required string SupplierCode { get; set; }

    [MaxLength(100)]
    public required string SupplierName { get; set; }

    [MaxLength(250)]
    public string? OtherSupplierDetails { get; set; }
    public ICollection<Product>? Products { get; set; }
}