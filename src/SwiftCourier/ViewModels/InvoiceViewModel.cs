using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class InvoiceViewModel
    {
        [Display(Name = "Booking Id")]
        public int BookingId { get; set; }

        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        [Display(Name = "GCT")]
        public decimal GCT { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Invoice Status")]
        public InvoiceStatus Status { get; set; }

        [Display(Name = "Billing Mode")]
        public BillingMode BillingMode { get; set; }
    }

    public class InvoiceDetailsViewModel
    {
        [Display(Name = "Booking Id")]
        public int BookingId { get; set; }

        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        [Display(Name = "GCT")]
        public decimal GCT { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Invoice Status")]
        public string Status { get; set; }

        [Display(Name = "Billing Mode")]
        public string BillingMode { get; set; }
    }
}
