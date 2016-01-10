using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum InvoiceStatus
    {
        [Display(Name = "Pending Payment")]
        Pending,

        [Display(Name = "Paid")]
        Paid
    }
}
