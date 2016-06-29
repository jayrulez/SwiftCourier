using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SwiftCourier.Web.Models.Enums;

namespace SwiftCourier.Web.Data
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            DeliveredPackages = new HashSet<Package>();
            AssignedPackages = new HashSet<Package>();
            Payments = new HashSet<Payment>();
            UserPermissions = new HashSet<UserPermission>();
            CreatedBookings = new HashSet<Booking>();
        }

        public UserType UserType { get; set; }

        public virtual ICollection<Package> DeliveredPackages { get; set; }
        public virtual ICollection<Package> AssignedPackages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<Booking> CreatedBookings { get; set; }
    }
}
