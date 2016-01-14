using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static Payment ToEntity(this PaymentViewModel source)
        {
            var destination = new Payment();

            destination.Amount = source.Amount;
            destination.PaymentMethodId = source.PaymentMethodId;

            return destination;
        }

        public static PaymentDetailsViewModel ToDetailsViewModel(this Payment source)
        {
            var destination = new PaymentDetailsViewModel();

            destination.Id = source.Id;
            destination.InvoiceId = source.InvoiceId;
            destination.PaymentMethod = source.PaymentMethod.Name;
            destination.Amount = source.Amount;
            destination.ProcessedAt = source.ProcessedAt;
            destination.ProcessedBy = source.User.UserName;

            return destination;
        }

        public static PaymentListItemViewModel ToListItemViewModel(this Payment source)
        {
            var destination = new PaymentListItemViewModel();

            destination.Id = source.Id;
            destination.InvoiceId = source.InvoiceId;
            destination.PaymentMethod = source.PaymentMethod.Name;
            destination.Amount = source.Amount;
            destination.ProcessedAt = source.ProcessedAt;
            destination.ProcessedBy = source.User.UserName;

            return destination;
        }

        public static List<PaymentListItemViewModel> ToListViewModel(this List<Payment> source)
        {
            var destination = new List<PaymentListItemViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }
    }
}
