using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class UserViewModel
    {
    }

    public class CreateUserViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
