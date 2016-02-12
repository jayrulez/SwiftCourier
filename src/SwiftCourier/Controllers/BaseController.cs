using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwiftCourier.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        protected readonly UserManager<User> _userManager;
        protected ApplicationDbContext _context;

        protected async Task<User> GetCurrentUserAsync()
        {
            return await _userManager.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id.ToString() == HttpContext.User.GetUserId());
        }

        protected int GetCurrentUserId()
        {
            var task = GetCurrentUserAsync();

            var user = task.Result;

            if(user == null)
            {
                throw new Exception("Unable to get id of current user.");
            }

            return user.Id;
        }

        protected bool HasPermission(string permissionName)
        {
            var task = GetCurrentUserAsync();

            var user = task.Result;

            if(user == null)
            {
                return false;
            }

            var permission = _context.Permissions.Include(p => p.RolePermissions).FirstOrDefault(p => p.Name == permissionName);

            if (permission == null)
            {
                return false;
            }

            var roleIds = new List<int>();

            foreach(var rolePermission in permission.RolePermissions)
            {
                roleIds.Add(rolePermission.RoleId);
            }

            var userRoles = user.Roles;

            foreach(var userRole in userRoles)
            {
                if(roleIds.Contains(userRole.RoleId))
                {
                    return true;
                }
            }

            return false;
        }

        protected IActionResult Unauthorized()
        {
            return View("Unauthorized");
        }
    }
}
