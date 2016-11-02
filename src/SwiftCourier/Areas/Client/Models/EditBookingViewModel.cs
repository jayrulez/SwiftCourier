using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Areas.Client.Models
{
    public class EditBookingViewModel
    {
        public int Id { get; set; }

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

        [Display(Name = "Package Description")]
        public string PackageDescription { get; set; }

        [Display(Name = "# Of Pieces")]
        public int Pieces { get; set; }

        [Display(Name = "Weight (Pounds)")]
        public Decimal Weight { get; set; }

        [Display(Name = "Special Instructions")]
        public string SpecialInstructions { get; set; }

        [Display(Name = "Reference #")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Package Type")]
        public int PackageTypeId { get; set; }
    }
}
