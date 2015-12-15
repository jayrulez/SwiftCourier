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

        public virtual ICollection<PackageLog> PackageLogs { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
