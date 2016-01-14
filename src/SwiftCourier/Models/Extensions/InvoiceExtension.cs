using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static InvoiceViewModel ToViewModel(this Invoice source)
        {
            var destination = new InvoiceViewModel();

            destination.BookingId = source.BookingId;

            destination.ServiceCost = source.ServiceCost;
            destination.GCT = source.GCT;
            destination.Total = source.Total;
            destination.Status = source.Status;
            destination.BillingMode = source.BillingMode;
            destination.AmountPaid = source.AmountPaid;
            destination.AmountDue = source.AmountDue;

            return destination;
        }

        public static InvoiceDetailsViewModel ToDetailsViewModel(this Invoice source)
        {
            var destination = new InvoiceDetailsViewModel();

            destination.BookingId = source.BookingId;

            destination.ServiceCost = source.ServiceCost;
            destination.GCT = source.GCT;
            destination.Total = source.Total;
            destination.Status = source.Status.ToString();
            destination.BillingMode = source.BillingMode.ToString();
            destination.AmountPaid = source.AmountPaid;
            destination.AmountDue = source.AmountDue;

            if(source.Payments != null)
            {
                destination.Payments = source.Payments.ToList().ToListViewModel();
            }

            return destination;
        }

        public static Invoice ToEntity(this InvoiceViewModel source)
        {
            var destination = new Invoice();

            if(source.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.ServiceCost = source.ServiceCost;
            destination.GCT = source.GCT;
            destination.Total = source.Total;
            destination.BillingMode = source.BillingMode;

            return destination;
        }

        public static Invoice UpdateEntity(this InvoiceViewModel source, Invoice destination)
        {
            if(destination == null)
            {
                destination = new Invoice();
            }

            if (source.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.ServiceCost = source.ServiceCost;
            destination.GCT = source.GCT;
            destination.Total = source.Total;
            destination.BillingMode = source.BillingMode;

            return destination;
        }
    }
}
