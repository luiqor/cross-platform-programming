using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMvcApp.ViewModels
{
    public class UserProfileViewModel
    {
        public required string EmailAddress { get; set; }

        public required string Name { get; set; }

        public required string ProfileImage { get; set; }
    }
}
