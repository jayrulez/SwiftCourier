using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Data
{
    public partial class Booking
    {
        public Booking()
        {
        }

        public int Id { get; set; }
        public int? CreatedByUserId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public string RequestDate { get; set; }

        public bool PickupRequired { get; set; }
        public string PickupAddress { get; set; }
        public string PickupContactNumber { get; set; }

        public string ConsigneeAddress { get; set; }
        public string ConsigneeContactNumber { get; set; }
        public string ConsigneeName { get; set; }

        public DateTime CreatedAt { get; set; }

        public int OriginLocationId { get; set; }
        public int DestinationLocationId { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Package Package { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Location Origin { get; set; }
        public virtual Location Destination { get; set; }
    }
}
