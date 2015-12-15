using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;

namespace SwiftCourier.Controllers
{
    public class LocationsController : Controller
    {
        private ApplicationDbContext _context;

        public LocationsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Locations.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Location location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(location);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Location location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Update(location);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(location);
        }
        
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Location location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
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
