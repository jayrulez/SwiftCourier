using System.Linq;
using System.Threading.Tasks;
using SwiftCourier.Models;
using Microsoft.AspNetCore.Identity;
using SwiftCourier.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;

namespace SwiftCourier.Controllers
{
    public class LocationsController : BaseController
    {
        public LocationsController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }
        
        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_LOCATIONS"))
            {
                return Unauthorized();
            }

            var locations = await _context.Locations.ToListAsync();

            return View(locations.ToListViewModel());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (!HasPermission("VIEW_LOCATIONS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            if (!HasPermission("CREATE_LOCATIONS"))
            {
                return Unauthorized();
            }

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocationViewModel model)
        {
            if (!HasPermission("CREATE_LOCATIONS"))
            {
                return Unauthorized();
            }

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
            if (!HasPermission("EDIT_LOCATIONS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LocationViewModel model)
        {
            if (!HasPermission("EDIT_LOCATIONS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var location = await _context.Locations.SingleAsync(m => m.Id == model.Id);
                if (location == null)
                {
                    return NotFound();
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
            if (!HasPermission("DELETE_LOCATIONS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.SingleAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location.ToDetailsViewModel());
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_LOCATIONS"))
            {
                return Unauthorized();
            }

            Location location = await _context.Locations.SingleAsync(m => m.Id == id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
