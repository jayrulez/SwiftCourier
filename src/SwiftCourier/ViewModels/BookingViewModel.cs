using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class BookingViewModel
    {
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string PickupAddress { get; set; }
        public string PickupContactNumber { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public BillingMode BillingMode { get; set; }

        public InvoiceViewModel Invoice { get; set; }
    }

    public class InvoiceViewModel
    {
        public decimal ServiceCost { get; set; }
        public decimal GCT { get; set; }
        public decimal Total { get; set; }
    }
}
