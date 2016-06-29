using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Models.Enums
{
    public enum DiscountType
    {
        [Display(Name = "None")]
        None,

        [Display(Name = "Percentage")]
        Percentage,

        [Display(Name = "Flat Amount")]
        FlatAmount
    }
}
