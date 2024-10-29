using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ім'я користувача не може перевищувати 50 символів.")]
        public required string Username { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "ФІО не може перевищувати 500 символів.")]
        public required string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль має бути від 8 до 16 символів.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "Пароль має містити щонайменше 1 цифру, 1 знак, 1 велику літеру.")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Телефон має бути у форматі Україна.")]
        public required string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
    }
}
