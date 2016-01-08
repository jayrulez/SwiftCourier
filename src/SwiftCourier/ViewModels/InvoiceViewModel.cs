using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class InvoiceViewModel
    {
        public int BookingId { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal GCT { get; set; }
        public decimal Total { get; set; }
        public InvoiceStatus Status { get; set; }
        public BillingMode BillingMode { get; set; }
    }

    public class InvoiceDetailsViewModel
    {
        public int BookingId { get; set; }
        public decimal ServiceCost { get; set; }
        public decimal GCT { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public string BillingMode { get; set; }
    }
}
