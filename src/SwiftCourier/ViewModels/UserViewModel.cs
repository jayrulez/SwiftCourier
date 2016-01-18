using SwiftCourier.Models;
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
        [Display(Name ="User Tye")]
        public UserType UserType { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class EditUserViewModel
    {
        [Display(Name = "User Tye")]
        public UserType UserType { get; set; }

        public int Id { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }
    }
}
