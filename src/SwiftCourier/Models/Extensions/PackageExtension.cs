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

            destination.Type = source.Type;
            destination.Description = source.Description;
            destination.Pieces = source.Pieces;
            destination.Weight = source.Weight;
            destination.SpecialInstructions = source.SpecialInstructions;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.AssignedToUserId = source.AssignedToUserId;
            destination.DeliveredByUserId = source.DeliveredByUserId;
            destination.Status = source.Status;

            return destination;
        }

        public static PackageDetailsViewModel ToDetailsViewModel(this Package source)
        {
            var destination = new PackageDetailsViewModel();

            destination.BookingId = source.BookingId;

            destination.Type = source.Type.ToString();
            destination.Description = source.Description;
            destination.Pieces = source.Pieces;
            destination.Weight = source.Weight;
            destination.SpecialInstructions = source.SpecialInstructions;

            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.AssignedTo = source.AssignedTo != null ? source.AssignedTo.UserName : string.Empty;
            destination.DeliveredBy = source.DeliveredBy != null ? source.DeliveredBy.UserName : string.Empty;
            destination.Status = source.Status.ToString();

            if (source.PackageLogs != null)
            {
                destination.PackageLogs = source.PackageLogs.ToList().ToListViewModel();
            }

            return destination;
        }

        public static Package ToEntity(this PackageViewModel source)
        {
            var destination = new Package();

            if(destination.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.Type = source.Type;
            destination.Description = source.Description;
            destination.Pieces = source.Pieces;
            destination.Weight = source.Weight;
            destination.SpecialInstructions = source.SpecialInstructions;
            destination.TrackingNumber = source.TrackingNumber;
            destination.PickedUpAt = source.PickedUpAt;
            destination.DeliveredAt = source.DeliveredAt;
            destination.AssignedToUserId = source.AssignedToUserId;
            destination.DeliveredByUserId = source.DeliveredByUserId;
            destination.Status = source.Status;

            return destination;
        }

        public static Package UpdateEntity(this PackageViewModel source, Package destination)
        {
            if(destination == null)
            {
                destination = new Package();
            }

            if (source.BookingId != 0)
            {
                destination.BookingId = source.BookingId;
            }

            destination.Type = source.Type;
            destination.Description = source.Description;
            destination.Pieces = source.Pieces;
            destination.Weight = source.Weight;
            destination.SpecialInstructions = source.SpecialInstructions;
            //destination.TrackingNumber = source.TrackingNumber;
            //destination.PickedUpAt = source.PickedUpAt;
            //destination.DeliveredAt = source.DeliveredAt;
            //destination.AssignedToUserId = source.AssignedToUserId;
            //destination.DeliveredByUserId = source.DeliveredByUserId;
            //destination.Status = source.Status;

            return destination;
        }
    }
}
