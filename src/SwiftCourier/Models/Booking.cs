using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int BillingMode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }

        public virtual Delivery Deliveries { get; set; }
        public virtual Invoice Invoices { get; set; }
        public virtual Package Packages { get; set; }
        public virtual Pickup Pickups { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
