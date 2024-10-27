using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.ViewModels
{
    public class UserProfileViewModel
    {
        public required string Email { get; set; }

        public required string ProfileImage { get; set; }

        public required string Username { get; set; }

        public required string FullName { get; set; }

        public required string PhoneNumber { get; set; }

    }
}
