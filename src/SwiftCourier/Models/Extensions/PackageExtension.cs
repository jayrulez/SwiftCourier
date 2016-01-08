using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static PackageViewModel ToViewModel(this Package source)
        {
            var destination = new PackageViewModel();

            destination.BookingId = source.BookingId;

            destination.Description = source.Description;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.DeliveredByUserId = source.DeliveredByUserId;
            destination.Status = source.Status;

            return destination;
        }

        public static PackageDetailsViewModel ToDetailsViewModel(this Package source)
        {
            var destination = new PackageDetailsViewModel();

            destination.BookingId = source.BookingId;

            destination.Description = source.Description;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.DeliveredByUser = source.DeliveredBy != null ? source.DeliveredBy.UserName : string.Empty;
            destination.Status = source.Status.ToString();

            return destination;
        }

        public static Package ToEntity(this PackageViewModel source)
        {
            var destination = new Package();

            if(destination.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.Description = source.Description;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.DeliveredByUserId = source.DeliveredByUserId;
            destination.Status = source.Status;

            return destination;
        }

        public static void UpdateEntity(this PackageViewModel source, Package destination)
        {
            if(destination == null)
            {
                destination = new Package();
            }

            if (destination.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.Description = source.Description;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.DeliveredByUserId = source.DeliveredByUserId;
            destination.Status = source.Status;
        }
    }
}
