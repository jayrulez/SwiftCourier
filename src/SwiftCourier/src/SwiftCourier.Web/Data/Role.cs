using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SwiftCourier.Web.Data
{
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
