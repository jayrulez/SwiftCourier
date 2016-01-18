using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            /*
            Claims = new HashSet<UserClaim>();
            Roles = new HashSet<UserRole>();
            Logins = new HashSet<UserLogin>();
            */
            DeliveredPackages = new HashSet<Package>();
            AssignedPackages = new HashSet<Package>();
            Payments = new HashSet<Payment>();
            UserPermissions = new HashSet<UserPermission>();
            CreatedBookings = new HashSet<Booking>();
        }

        public UserType UserType { get; set; }

        /*
        public virtual new ICollection<UserClaim> Claims { get; set; }
        public virtual new ICollection<UserRole> Roles { get; set; }
        public virtual new ICollection<UserLogin> Logins { get; set; }
        */
        public virtual ICollection<Package> DeliveredPackages { get; set; }
        public virtual ICollection<Package> AssignedPackages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<Booking> CreatedBookings { get; set; }
    }
}
