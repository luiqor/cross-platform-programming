using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [MaxLength(50)]
    public required string FirstName { get; set; }

    [MaxLength(50)]
    public string? MiddleName { get; set; }

    [MaxLength(50)]
    public required string LastName { get; set; }

    [MaxLength(12)]
    public required string CustomerPhone { get; set; }

    [MaxLength(50)]
    public string? CustomerEmail { get; set; }

    [MaxLength(250)]
    public string? OtherCustomerDetails { get; set; }
    public required ICollection<CustomerAddress> CustomerAddresses { get; set; }
    public ICollection<CustomerOrder>? CustomerOrders { get; set; }
    public ICollection<CustomerPaymentMethod>? CustomerPaymentMethods { get; set; }
}