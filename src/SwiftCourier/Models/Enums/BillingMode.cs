using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum BillingMode
    {
        [Display(Name = "Cash on delivery")]
        CashOnDelivery,

        [Display(Name = "Cash on pickup")]
        CashOnPickup,

        [Display(Name = "Bill to account")]
        BillToAccount
    }
}
