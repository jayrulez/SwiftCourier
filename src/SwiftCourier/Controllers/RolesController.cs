using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;

namespace SwiftCourier.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger _logger;
        private ApplicationDbContext _context;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public RolesController(RoleManager<Role> roleManager, ILoggerFactory loggerFactory, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<RolesController>();
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(await roles.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var role = await _roleManager.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            var permissions = _context.Permissions;
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                //role.ConcurrencyStamp = Guid.NewGuid().ToString();
                var result = await _roleManager.CreateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }
            var permissions = _context.Permissions;
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return HttpNotFound();
            }
            var permissions = _context.Permissions;
            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrors(result);
                }
            }
            var permissions = _context.Permissions;
            return View(role);
        }

        // GET: Roles/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
    }
}
