using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
