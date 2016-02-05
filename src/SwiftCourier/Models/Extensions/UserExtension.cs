using Microsoft.AspNet.Identity.EntityFramework;
using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static User UpdateEntity(this UserViewModel source, User destination)
        {
            destination.UserType = source.UserType;

            var oldRoles = new List<IdentityUserRole<int>>();

            foreach (var userRole in destination.Roles)
            {
                oldRoles.Add(userRole);
            }

            foreach (var oldRole in oldRoles)
            {
                if(!source.RoleIds.Contains(oldRole.RoleId))
                {
                    destination.Roles.Remove(oldRole);
                }
                else
                {
                    source.RoleIds.Remove(oldRole.RoleId);
                }
            }

            if (source.RoleIds != null)
            {
                foreach (var roleId in source.RoleIds)
                {
                    var userRole = new IdentityUserRole<int>();

                    userRole.RoleId = roleId;

                    destination.Roles.Add(userRole);
                }
            }

            return destination;
        }

        public static User ToEntity(this UserViewModel source)
        {
            var destination = new User();

            destination.Email = source.Email;
            destination.UserName = source.UserName;
            destination.UserType = source.UserType;

            if(source.RoleIds != null)
            {
                foreach(var roleId in source.RoleIds)
                {
                    var userRole = new IdentityUserRole<int>() { RoleId = roleId };

                    destination.Roles.Add(userRole);
                }
            }

            return destination;
        }

        public static UserViewModel ToViewModel(this User source)
        {
            var destination = new UserViewModel();

            destination.Id = source.Id;
            destination.Email = source.Email;
            destination.UserName = source.UserName;
            destination.UserType = source.UserType;

            if(source.Roles != null)
            {
                foreach(var userRole in source.Roles)
                {
                    destination.RoleIds.Add(userRole.RoleId);
                }
            }

            return destination;
        }
    }
}
