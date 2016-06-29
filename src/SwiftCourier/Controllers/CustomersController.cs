using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SwiftCourier.Data;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using SwiftCourier.Models;
using SwiftCourier.Models.Enums;

namespace SwiftCourier.Controllers
{
    public class CustomersController : BaseController
    {
        public CustomersController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }
        
        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_CUSTOMERS"))
            {
                return Unauthorized();
            }

            var customers = await _context.Customers.ToListAsync();

            return View(customers.ToListViewModel());
        }

        public async Task<IActionResult> Statement(int? id)
        {
            if (!HasPermission("VIEW_CUSTOMERS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Invoice)
                .Include(b => b.CreatedBy)
                .Include(b => b.Service)
                .Where(b => b.Invoice.Status != InvoiceStatus.Paid /*&& b.Invoice.BillingMode == BillingMode.BillToAccount*/)
                .ToListAsync();

            ViewData["Customer"] = customer.ToDetailsViewModel();

            return View(bookings.ToListViewModel());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!HasPermission("VIEW_CUSTOMERS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            if (!HasPermission("CREATE_CUSTOMERS"))
            {
                return Unauthorized();
            }

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel model)
        {
            if (!HasPermission("CREATE_CUSTOMERS"))
            {
                return Unauthorized();
            }

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
            if (!HasPermission("EDIT_CUSTOMERS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            if (!HasPermission("EDIT_CUSTOMERS"))
            {
                return Unauthorized();
            }

            var customer = await _context.Customers.SingleAsync(m => m.Id == model.Id);

            if (customer == null)
            {
                return NotFound();
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
        
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HasPermission("DELETE_CUSTOMERS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer.ToDetailsViewModel());
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_CUSTOMERS"))
            {
                return Unauthorized();
            }

            Customer customer = await _context.Customers.SingleAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
