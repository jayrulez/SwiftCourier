﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum BillingMode
    {

        [Display(Name = "Pay Now")]
        PayNow,

        [Display(Name = "Pay On Delivery")]
        PayOnDelivery,

        [Display(Name = "Bill To Account")]
        BillToAccount
    }
}