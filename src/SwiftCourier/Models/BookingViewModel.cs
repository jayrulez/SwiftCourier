﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SwiftCourier.Models
{
    public class BookingViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public int? CreatedByUserId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Service Id")]
        public int ServiceId { get; set; }

        [Display(Name = "Request Date")]
        public string RequestDate { get; set; }

        [Display(Name = "Origin")]
        public int OriginLocationId { get; set; }

        [Display(Name = "Destination")]
        public int DestinationLocationId { get; set; }

        [Display(Name = "Pickup Required")]
        public bool PickupRequired { get; set; }

        [Display(Name = "Pickup Address")]
        public string PickupAddress { get; set; }

        [Display(Name = "Pickup Contact Number")]
        public string PickupContactNumber { get; set; }

        [Display(Name = "Consignee")]
        public string ConsigneeName { get; set; }

        [Display(Name = "Consignee Address")]
        public string ConsigneeAddress { get; set; }

        [Display(Name = "Consignee Contact Number")]
        public string ConsigneeContactNumber { get; set; }

        public InvoiceViewModel Invoice { get; set; }
        public PackageViewModel Package { get; set; }
    }

    public class BookingListItemViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Service")]
        public string ServiceName { get; set; }

        [Display(Name = "Request Date")]
        public string RequestDate { get; set; }

        [Display(Name = "Pickup Required")]
        public bool PickupRequired { get; set; }

        [Display(Name = "Pickup Address")]
        public string PickupAddress { get; set; }

        [Display(Name = "Pickup Contact Number")]
        public string PickupContactNumber { get; set; }

        [Display(Name = "Consignee")]
        public string ConsigneeName { get; set; }

        [Display(Name = "Consignee Address")]
        public string ConsigneeAddress { get; set; }

        [Display(Name = "Consignee Contact Number")]
        public string ConsigneeContactNumber { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public LocationViewModel Origin { get; set; }
        public LocationViewModel Destination { get; set; }
        public InvoiceViewModel Invoice { get; set; }
        public PackageViewModel Package { get; set; }
        public CustomerListItemViewModel Customer { get; set; }
    }

    public class BookingDetailsViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Service")]
        public string ServiceName { get; set; }

        [Display(Name = "Request Date")]
        public string RequestDate { get; set; }

        [Display(Name = "Pickup Required")]
        public string PickupRequired { get; set; }

        [Display(Name = "Pickup Address")]
        public string PickupAddress { get; set; }

        [Display(Name = "Pickup Contact Number")]
        public string PickupContactNumber { get; set; }

        [Display(Name = "Consignee")]
        public string ConsigneeName { get; set; }

        [Display(Name = "Consignee Address")]
        public string ConsigneeAddress { get; set; }

        [Display(Name = "Consignee Contact Number")]
        public string ConsigneeContactNumber { get; set; }

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }

        public LocationViewModel Origin { get; set; }
        public LocationViewModel Destination { get; set; }
        public InvoiceDetailsViewModel Invoice { get; set; }
        public PackageDetailsViewModel Package { get; set; }
        public CustomerDetailsViewModel Customer { get; set; }
    }
}
