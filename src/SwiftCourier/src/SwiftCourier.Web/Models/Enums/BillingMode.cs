using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Models.Enums
{
    public enum BillingMode
    {

        [Display(Name = "Pay Now")]
        PayNow,

        [Display(Name = "Cash On Delivery")]
        CashOnDelivery,

        [Display(Name = "Bill To Account")]
        BillToAccount
    }
}
