using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Pickup
    {
        public int BookingId { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public PickupStatus Status { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
