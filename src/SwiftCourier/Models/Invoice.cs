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
        public BillingMode BillingMode { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal GCT { get; set; }
        public decimal Total { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountAmount { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
