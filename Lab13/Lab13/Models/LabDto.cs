using System.ComponentModel.DataAnnotations;

namespace Lab13.Models;

public class LabDto
{
    [Required(ErrorMessage = "InputData is required.")]
    public string InputData { get; set; } = string.Empty;
}
