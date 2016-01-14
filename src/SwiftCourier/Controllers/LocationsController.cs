using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;

namespace SwiftCourier.Controllers
{
    public class LocationsController : BaseController
    {
        private ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context; 
        }
        
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();

            return View(locations.ToListViewModel());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var location = model.ToEntity();

                _context.Locations.Add(location);
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

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var location = await _context.Locations.SingleAsync(m => m.Id == model.Id);
                if (location == null)
                {
                    return HttpNotFound();
                }

                location = model.UpdateEntity(location);

                _context.Update(location);
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

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location.ToDetailsViewModel());
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Location location = await _context.Locations.SingleAsync(m => m.Id == id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
