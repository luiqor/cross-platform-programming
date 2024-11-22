using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public int? ParentProductId { get; set; }
    public Product? ParentProduct { get; set; }

    [MaxLength(10)]
    public required string ProductTypeCode { get; set; }
    public string? SupplierCode { get; set; }
    public Supplier? Supplier { get; set; }
    public decimal ProductPrice { get; set; }

    [MaxLength(50)]
    public required string BookIsbn { get; set; }

    [MaxLength(50)]
    public required string BookAuthor { get; set; }
    public DateTime BookPublicationDate { get; set; }

    [MaxLength(150)]
    public required string BookTitle { get; set; }
    public bool FoodContainsYn { get; set; }

    [MaxLength(50)]
    public string? FoodName { get; set; }

    [MaxLength(150)]
    public string? FoodDescription { get; set; }

    [MaxLength(50)]
    public string? FoodFlavor { get; set; }

    [MaxLength(50)]
    public string? FoodIngredients { get; set; }

    [MaxLength(150)]
    public string? OtherProductDetails { get; set; }
    public required ICollection<ProductPrice> ProductPrices { get; set; }
    public required ICollection<CustomerOrdersProducts> CustomerOrdersProducts { get; set; }
}
