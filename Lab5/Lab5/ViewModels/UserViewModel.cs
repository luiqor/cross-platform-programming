using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ім'я користувача не може перевищувати 50 символів.")]
        public string Username { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "ФІО не може перевищувати 500 символів.")]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Пароль має бути від 8 до 16 символів.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$", ErrorMessage = "Пароль має містити щонайменше 1 цифру, 1 знак, 1 велику літеру.")]
        public string Password { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Телефон має бути у форматі Україна.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
