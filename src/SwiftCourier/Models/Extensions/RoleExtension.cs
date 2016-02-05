using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static Role ToEntity(this RoleViewModel source)
        {
            var destination = new Role();

            destination.Name = source.Name;

            if(source.PermissionIds != null)
            {
                foreach(var permissionId in source.PermissionIds)
                {
                    var rolePermission = new RolePermission();

                    rolePermission.PermissionId = permissionId;

                    destination.RolePermissions.Add(rolePermission);
                }
            }

            return destination;
        }

        public static RoleViewModel ToViewModel(this Role source)
        {
            var destination = new RoleViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.ConcurrencyStamp = source.ConcurrencyStamp;

            if(source.RolePermissions != null)
            {
                foreach(var rolePermission in source.RolePermissions)
                {
                    destination.PermissionIds.Add(rolePermission.PermissionId);
                }
            }

            return destination;
        }

        public static List<RoleViewModel> ToListViewModel(this List<Role> source)
        {
            var destination = new List<RoleViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToViewModel());
                }
            }

            return destination;
        }

        public static Role UpdateEntity(this RoleViewModel source, Role destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.ConcurrencyStamp = source.ConcurrencyStamp;

            var oldPermissions = new List<RolePermission>();

            foreach(var rolePermission in destination.RolePermissions)
            {
                oldPermissions.Add(rolePermission);
            }

            foreach(var oldPermission in oldPermissions)
            {
                if(!source.PermissionIds.Contains(oldPermission.PermissionId))
                {
                    destination.RolePermissions.Remove(oldPermission);
                }
                else
                {
                    source.PermissionIds.Remove(oldPermission.PermissionId);
                }
            }

            if(source.PermissionIds != null)
            {
                foreach (var permissionId in source.PermissionIds)
                {
                    var rolePermission = new RolePermission();

                    rolePermission.PermissionId = permissionId;

                    destination.RolePermissions.Add(rolePermission);
                }
            }

            return destination;
        }
    }
}
