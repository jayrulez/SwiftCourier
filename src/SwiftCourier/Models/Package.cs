using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Package
    {
        public Package()
        {
            PackageLogs = new HashSet<PackageLog>();
        }

        public int BookingId { get; set; }
        public string Description { get; set; }
        public string TrackingNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public int? AssignedToUserId { get; set; }
        public int? DeliveredByUserId { get; set; }
        public PackageStatus Status { get; set; }
        public Decimal Weight { get; set; }
        public int Pieces { get; set; }
        public string SpecialInstructions { get; set; }
        public int PackageTypeId { get; set; }

        public virtual ICollection<PackageLog> PackageLogs { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual User DeliveredBy { get; set; }
        public virtual User AssignedTo { get; set; }
        public PackageType PackageType { get; set; }
    }
}
