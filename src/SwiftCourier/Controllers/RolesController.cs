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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public RolesController(RoleManager<Role> roleManager, ILoggerFactory loggerFactory)
        {
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<RolesController>();    
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Role role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
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
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Role role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return HttpNotFound();
            }
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

            Role role = await _roleManager.FindByIdAsync(id.ToString());

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
            Role role = await _roleManager.FindByIdAsync(id.ToString());

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }
    }
}
