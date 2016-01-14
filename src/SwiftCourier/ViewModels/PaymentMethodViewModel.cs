using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class PaymentMethodViewModel
    {
        [Display(Name="Payment Method")]
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public string Name { get; set; }
    }

    public class PaymentMethodDetailsViewModel
    {
        [Display(Name = "Payment Method")]
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public string Name { get; set; }
    }

    public class PaymentMethodListItemViewModel
    {
        [Display(Name = "Payment Method")]
        public int Id { get; set; }

        [Display(Name = "Payment Method")]
        public string Name { get; set; }
    }
}
