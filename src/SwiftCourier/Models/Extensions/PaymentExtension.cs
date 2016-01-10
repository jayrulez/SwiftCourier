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
    }
}
