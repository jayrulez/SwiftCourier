using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Models.Enums
{
    public enum UserType
    {
        [Display(Name = "Courier")]
        COURIER,

        [Display(Name = "Dispatcher")]
        DISPATCHER,

        [Display(Name = "Accountant")]
        ACCOUNTANT,

        [Display(Name = "Normal User")]
        NORMAL_USER
    }
}
