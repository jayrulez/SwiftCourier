using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Controllers
{
    public class SettingsController : BaseController
    {
        public SettingsController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }

        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_SETTINGS"))
            {
                return Unauthorized();
            }

            var settings = await _context.Settings.ToListAsync();

            return View(settings.ToListViewModel());
        }

        public async Task<IActionResult> Edit(string name)
        {
            if (!HasPermission("EDIT_SETTINGS"))
            {
                return Unauthorized();
            }

            if (name == null)
            {
                return HttpNotFound();
            }

            var setting = await _context.Settings.SingleAsync(m => m.Name == name);
            if (setting == null)
            {
                return HttpNotFound();
            }
            return View(setting.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SettingViewModel model)
        {
            if (!HasPermission("EDIT_SETTINGS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var setting = await _context.Settings.SingleAsync(m => m.Name == model.Name);
                if (setting == null)
                {
                    return HttpNotFound();
                }

                setting = model.UpdateEntity(setting);

                _context.Update(setting);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
