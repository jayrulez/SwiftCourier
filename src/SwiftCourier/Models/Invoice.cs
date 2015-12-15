using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Payments = new HashSet<Payment>();
        }

        public int BookingId { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
