using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;
using System.Net;

public class CustomerAddress
{
    [Key]
    public DateTime DateFrom { get; set; }
    public int CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public int AddressId { get; set; }
    public required Address Address { get; set; }

    [MaxLength(10)]
    public string? AddressTypeCode { get; set; }
    public DateTime DateTo { get; set; }
}