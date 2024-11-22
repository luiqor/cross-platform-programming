using System.ComponentModel.DataAnnotations;

namespace Lab13.Models;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
