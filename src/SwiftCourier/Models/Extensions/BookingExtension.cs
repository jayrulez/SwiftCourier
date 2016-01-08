using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static BookingViewModel ToViewModel(this Booking source)
        {
            var destination = new BookingViewModel();

            destination.Id = source.Id;

            destination.CustomerId = source.CustomerId;
            destination.ServiceId = source.ServiceId;
            destination.PickupAddress = source.PickupAddress;
            destination.PickupContactNumber = source.PickupContactNumber;
            destination.ConsigneeName = source.ConsigneeName;
            destination.ConsigneeContactNumber = source.ConsigneeContactNumber;
            destination.ConsigneeAddress = source.ConsigneeAddress;

            if (source.Invoice != null)
            {
                destination.Invoice = source.Invoice.ToViewModel();
            }
            if (source.Package != null)
            {
                destination.Package = source.Package.ToViewModel();
            }

            return destination;
        }

        public static BookingDetailsViewModel ToDetailsViewModel(this Booking source)
        {
            var destination = new BookingDetailsViewModel();

            destination.Id = source.Id;

            destination.CustomerName = source.Customer.Name;
            destination.ServiceName = source.Service.Name;
            destination.PickupAddress = source.PickupAddress;
            destination.PickupContactNumber = source.PickupContactNumber;
            destination.ConsigneeName = source.ConsigneeName;
            destination.ConsigneeContactNumber = source.ConsigneeContactNumber;
            destination.ConsigneeAddress = source.ConsigneeAddress;
            destination.CreatedAt = source.CreatedAt;

            if (source.Invoice != null)
            {
                destination.Invoice = source.Invoice.ToDetailsViewModel();
            }
            if (source.Package != null)
            {
                destination.Package = source.Package.ToDetailsViewModel();
            }

            return destination;
        }

        public static Booking ToEntity(this BookingViewModel source)
        {
            var destination = new Booking();

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.CustomerId = source.CustomerId;
            destination.ServiceId = source.ServiceId;
            destination.PickupAddress = source.PickupAddress;
            destination.PickupContactNumber = source.PickupContactNumber;
            destination.ConsigneeName = source.ConsigneeName;
            destination.ConsigneeContactNumber = source.ConsigneeContactNumber;
            destination.ConsigneeAddress = source.ConsigneeAddress;

            if(source.Invoice != null)
            {
                destination.Invoice = source.Invoice.ToEntity();
            }
            if (source.Package != null)
            {
                destination.Package = source.Package.ToEntity();
            }

            return destination;
        }

        public static void UpdateEntity(this BookingViewModel source, Booking destination)
        {
            destination.CustomerId = source.CustomerId;
            destination.ServiceId = source.ServiceId;
            destination.PickupAddress = source.PickupAddress;
            destination.PickupContactNumber = source.PickupContactNumber;
            destination.ConsigneeName = source.ConsigneeName;
            destination.ConsigneeContactNumber = source.ConsigneeContactNumber;
            destination.ConsigneeAddress = source.ConsigneeAddress;

            if (source.Invoice != null)
            {
                 source.Invoice.UpdateEntity(destination.Invoice);
            }
            if (source.Package != null)
            {
                 source.Package.UpdateEntity(destination.Package);
            }
        }
    }
}
