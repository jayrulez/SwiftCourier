using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Service
    {
        public Service()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
