using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Data
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
