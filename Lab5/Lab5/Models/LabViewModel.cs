using System.ComponentModel.DataAnnotations;

namespace Lab5.Models;

public class LabViewModel
{
    [Required(ErrorMessage = "InputData is required.")]
    public string InputData { get; set; } = string.Empty;
}
