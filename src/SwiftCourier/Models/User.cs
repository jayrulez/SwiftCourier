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
            PackageLogs = new HashSet<PackageLog>();
            Payments = new HashSet<Payment>();
            UserPermissions = new HashSet<UserPermission>();
        }

        /*
        public virtual new ICollection<UserClaim> Claims { get; set; }
        public virtual new ICollection<UserRole> Roles { get; set; }
        public virtual new ICollection<UserLogin> Logins { get; set; }
        */
        public virtual ICollection<PackageLog> PackageLogs { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
