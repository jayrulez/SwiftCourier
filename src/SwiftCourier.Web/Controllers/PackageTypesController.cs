using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using SwiftCourier.Web.Data;
using SwiftCourier.Web.ViewModels;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Controllers
{
    public class PackageTypesController : BaseController
    {
        public PackageTypesController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }

        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            var packageTypes = await _context.PackageTypes.ToListAsync();

            return View(packageTypes.ToListViewModel());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!HasPermission("VIEW_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return NotFound();
            }

            return View(packageType.ToDetailsViewModel());
        }

        public IActionResult Create()
        {
            if (!HasPermission("CREATE_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PackageTypeViewModel model)
        {
            if (!HasPermission("CREATE_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var packageType = model.ToEntity();

                _context.PackageTypes.Add(packageType);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!HasPermission("EDIT_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return NotFound();
            }

            return View(packageType.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PackageTypeViewModel model)
        {
            if (!HasPermission("EDIT_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == model.Id);
                if (packageType == null)
                {
                    return NotFound();
                }

                packageType = model.UpdateEntity(packageType);
                _context.Update(packageType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HasPermission("DELETE_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return NotFound();
            }

            return View(packageType.ToDetailsViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_PACKAGE_TYPES"))
            {
                return Unauthorized();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            _context.PackageTypes.Remove(packageType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
