using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class PackageViewModel
    {
        public int BookingId { get; set; }
        public string Description { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public int? DeliveredByUserId { get; set; }
        public PackageStatus Status { get; set; }
    }

    public class PackageDetailsViewModel
    {
        public int BookingId { get; set; }
        public string Description { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public string DeliveredByUser { get; set; }
        public string Status { get; set; }
    }
}
