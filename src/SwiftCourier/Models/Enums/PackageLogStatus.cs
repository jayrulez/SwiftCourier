using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum PackageLogStatus
    {
        [Display(Name="Dispatched To Courier")]
        Dispatched,

        [Display(Name = "Received By Courier")]
        Received,

        [Display(Name = "Delivered To Consignee")]
        Delivered
    }
}
