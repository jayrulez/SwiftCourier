using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserPermissions = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
