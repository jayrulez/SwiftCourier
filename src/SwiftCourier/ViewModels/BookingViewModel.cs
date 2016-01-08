using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string PickupAddress { get; set; }
        public string PickupContactNumber { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }

        public InvoiceViewModel Invoice { get; set; }
        public PackageViewModel Package { get; set; }
    }

    public class BookingDetailsViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public string PickupAddress { get; set; }
        public string PickupContactNumber { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public InvoiceDetailsViewModel Invoice { get; set; }
        public PackageDetailsViewModel Package { get; set; }
    }
}
