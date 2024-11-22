using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class CustomerOrdersDelivery
{
    [Key]
    public DateTime DateReported { get; set; }
    public int OrderId { get; set; }
    public required CustomerOrder CustomerOrder { get; set; }
    public string? DeliveryStatusCode { get; set; }
}