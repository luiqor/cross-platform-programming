using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class CustomerOrder
{
    [Key]
    public int OrderId { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int? CustomerPaymentMethodId { get; set; }
    public CustomerPaymentMethod? CustomerPaymentMethod { get; set; }

    [MaxLength(5)]
    public required string OrderStatusCode { get; set; }
    public DateTime DateOrderPlaced { get; set; }
    public DateTime DateOrderPaid { get; set; }
    public decimal OrderTotalPrice { get; set; }
    public string? OtherOrderDetails { get; set; }
    public required ICollection<CustomerOrdersProducts> CustomerOrdersProducts { get; set; }
    public required ICollection<CustomerOrdersDelivery> CustomerOrdersDeliveries { get; set; }
}