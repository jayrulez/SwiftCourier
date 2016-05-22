using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Helpers
{
    public class PermissionGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static List<PermissionGroup> GetList()
        {
            var groups = new List<PermissionGroup>() {
                new PermissionGroup() { Id = "administration", Name = "Administration" },
                new PermissionGroup() { Id = "user_management", Name = "User Management" },
                new PermissionGroup() { Id = "bookings", Name = "Bookings" },
                new PermissionGroup() { Id = "customer_management", Name = "Customer Management" },
                new PermissionGroup() { Id = "settings", Name = "Settings" },
                new PermissionGroup() { Id = "general", Name = "General" }
            };

            return groups;
        }
    }
}
