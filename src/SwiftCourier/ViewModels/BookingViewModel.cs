using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class BookingViewModel
    {
        public BillingMode BillingMode { get; set; }
        public int CustomerId { get; set; }
        public PickupViewModel Pickup { get; set; }
        public DeliveryViewModel Delivery { get; set; }
        public InvoiceViewModel Invoice { get; set; }
    }

    public class PickupViewModel
    {
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }

    public class DeliveryViewModel
    {
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
    }

    public class InvoiceViewModel
    {
        public decimal Total { get; set; }
    }
}
