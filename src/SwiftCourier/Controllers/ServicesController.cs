using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using Microsoft.AspNet.Identity;

namespace SwiftCourier.Controllers
{
    public class ServicesController : BaseController
    {
        public ServicesController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {   
        }
        
        public async Task<IActionResult> Index()
        {
            var services = await _context.Services.ToListAsync();

            return View(services.ToListViewModel());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var service = await _context.Services.SingleAsync(m => m.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = model.ToEntity();

                _context.Services.Add(service);
                await _context.SaveChangesAsync();
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

            var service = await _context.Services.SingleAsync(m => m.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var service = await _context.Services.SingleAsync(m => m.Id == model.Id);
                if (service == null)
                {
                    return HttpNotFound();
                }

                service = model.UpdateEntity(service);

                _context.Update(service);
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

            var service = await _context.Services.SingleAsync(m => m.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service.ToDetailsViewModel());
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.SingleAsync(m => m.Id == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
