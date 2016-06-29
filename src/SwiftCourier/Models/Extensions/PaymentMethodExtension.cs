using SwiftCourier.Models;
using SwiftCourier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models.Extensions
{
    public static partial class Extensions
    {
        public static PaymentMethod ToEntity(this PaymentMethodViewModel source)
        {
            var destination = new PaymentMethod();

            destination.Name = source.Name;

            return destination;
        }

        public static PaymentMethod UpdateEntity(this PaymentMethodViewModel source, PaymentMethod destination)
        {
            if(destination == null)
            {
                destination = new PaymentMethod();
            }

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Name = source.Name;

            return destination;
        }

        public static PaymentMethodViewModel ToViewModel(this PaymentMethod source)
        {
            var destination = new PaymentMethodViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static PaymentMethodDetailsViewModel ToDetailsViewModel(this PaymentMethod source)
        {
            var destination = new PaymentMethodDetailsViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static PaymentMethodListItemViewModel ToListItemViewModel(this PaymentMethod source)
        {
            var destination = new PaymentMethodListItemViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static List<PaymentMethodListItemViewModel> ToListViewModel(this List<PaymentMethod> source)
        {
            var destination = new List<PaymentMethodListItemViewModel>();

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
