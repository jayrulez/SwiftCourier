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
    public class PackageTypesController : BaseController
    {
        private ApplicationDbContext _context;

        public PackageTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var packageTypes = await _context.PackageTypes.ToListAsync();

            return View(packageTypes.ToListViewModel());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return HttpNotFound();
            }

            return View(packageType.ToDetailsViewModel());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PackageTypeViewModel model)
        {
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
            if (id == null)
            {
                return HttpNotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return HttpNotFound();
            }

            return View(packageType.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PackageTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == model.Id);
                if (packageType == null)
                {
                    return HttpNotFound();
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
            if (id == null)
            {
                return HttpNotFound();
            }

            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            if (packageType == null)
            {
                return HttpNotFound();
            }

            return View(packageType.ToDetailsViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packageType = await _context.PackageTypes.SingleAsync(m => m.Id == id);
            _context.PackageTypes.Remove(packageType);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
