using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public BillingMode BillingMode { get; set; }

        public string PickupAddress { get; set; }
        public string PickupContactNumber { get; set; }
        public PickupStatus PickupStatus { get; set; }

        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public string ConsigneeName { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Package Package { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
    }
}
