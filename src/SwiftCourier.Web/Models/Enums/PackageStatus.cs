using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Models.Enums
{
    public enum PackageStatus
    {
        [Display(Name = "Pending Pickup")]
        PendingPickup,

        [Display(Name = "Received By Location")]
        ReceivedByLocation,

        [Display(Name = "Dispatched To Courier")]
        DispatchedToCourier,

        [Display(Name = "Out For Delivery")]
        OutForDelivery,

        [Display(Name = "Delivered To Consignee")]
        Delivered,

        [Display(Name = "Undeliverable")]
        Undeliverable
    }
}
