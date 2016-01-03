using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Delivery
    {
        public int BookingId { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public string ConsigneeName { get; set; }
        public DeliveryStatus Status { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
