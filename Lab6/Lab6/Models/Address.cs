using System.ComponentModel.DataAnnotations;

namespace Lab6.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }

    [MaxLength(90)]
    public required string Line1 { get; set; }

    [MaxLength(90)]
    public string? Line2 { get; set; }

    [MaxLength(90)]
    public string? Line3 { get; set; }

    [MaxLength(30)]
    public required string City { get; set; }

    [MaxLength(10)]
    public required string ZipPostcode { get; set; }

    [MaxLength(50)]
    public string? StateProvinceCounty { get; set; }

    [MaxLength(50)]
    public required string Country { get; set; }

    [MaxLength(150)]
    public string? OtherAddressDetails { get; set; }
    public required ICollection<CustomerAddress> CustomerAddresses { get; set; }
}
