using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;

namespace SwiftCourier.Controllers
{
    public class CustomersController : BaseController
    {
        private ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;    
        }
        
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();

            return View(customers.ToListViewModel());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(model.ToEntity());
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

            var customer = await _context.Customers.SingleAsync(m => m.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {

            var customer = await _context.Customers.SingleAsync(m => m.Id == model.Id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                customer = model.UpdateEntity(customer);

                _context.Update(customer);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Customers/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer.ToDetailsViewModel());
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
