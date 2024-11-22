using System.ComponentModel.DataAnnotations;

namespace Lab5Lab6.Models;

public class LabViewModel
{
    [Required(ErrorMessage = "InputData is required.")]
    public string InputData { get; set; } = string.Empty;
}
