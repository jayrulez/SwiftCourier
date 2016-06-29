using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Data
{
    public partial class Location
    {
        public Location()
        {
            OriginBookings = new HashSet<Booking>();
            DestinationBookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Booking> OriginBookings { get; set; }
        public virtual ICollection<Booking> DestinationBookings { get; set; }
    }
}
