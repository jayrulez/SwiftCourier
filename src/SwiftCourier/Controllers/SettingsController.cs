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
        private ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _context.Settings.ToListAsync();

            return View(settings.ToListViewModel());
        }

        public async Task<IActionResult> Edit(string name)
        {
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
