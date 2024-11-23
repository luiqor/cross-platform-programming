using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class CustomerPaymentMethod
{
    [Key]
    public int CustomerPaymentMethodId { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [MaxLength(5)]
    public required string PaymentMethodCode { get; set; }

    [MaxLength(21)]
    public required string CardNumber { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    [MaxLength(150)]
    public required string OtherDetails { get; set; }
    public ICollection<CustomerOrder>? CustomerOrder { get; set; }
}