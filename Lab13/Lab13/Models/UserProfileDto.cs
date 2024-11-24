namespace Lab13.Models;

public class UserProfileDto
{
    public required string Id { get; set; }
    public required string Email { get; set; }

    public required string ProfileImage { get; set; }

    public required string Username { get; set; }

    public required string FullName { get; set; }

    public required string PhoneNumber { get; set; }

}
