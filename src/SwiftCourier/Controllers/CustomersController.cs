using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SwiftCourier.Data;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using SwiftCourier.Models;
using SwiftCourier.Models.Enums;
using System.Text;
using System;
using SwiftCourier.Utilities;

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

            var intMonth = DateTime.Now.Month;
            var intYear = DateTime.Now.Year;

            var month = Request.Query["month"].ToString();
            if(!string.IsNullOrEmpty(month))
            {
                intMonth = int.Parse(month);
            }

            var year = Request.Query["year"].ToString();
            if(!string.IsNullOrEmpty(year))
            {
                intYear = int.Parse(year);
            }
            
            var monthStart = new DateTime(intYear, intMonth, 1);

            var monthEnd = monthStart.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59);

            var bookings = await _context.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Invoice)
                .Include(b => b.CreatedBy)
                .Include(b => b.Service)
                .Where(b => b.Invoice.Status != InvoiceStatus.Paid 
                && b.CreatedAt >= monthStart
                && b.CreatedAt <= monthEnd
                /*&& b.Invoice.BillingMode == BillingMode.BillToAccount*/)
                .ToListAsync();

            ViewData["Customer"] = customer.ToDetailsViewModel();
            ViewData["Month"] = intMonth;
            ViewData["Year"] = intYear;

            var print = Request.Query["print"].ToString();
            if (!string.IsNullOrEmpty(print) && print.Equals("yes"))
            {
                return View("StatementPrint", bookings.ToListViewModel());
            }

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
                var customer = model.ToEntity();
                customer.Username = customer.Name;
                customer.Password = MD5Helper.Encode("password");

                _context.Customers.Add(customer);
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

        public async Task<IActionResult> EditPassword(int? id)
        {
            if (!HasPermission("EDIT_CUSTOMER_PASSWORD"))
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

            return View(new CustomerPasswordViewModel() { Id = customer.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(CustomerPasswordViewModel model)
        {
            if (!HasPermission("EDIT_CUSTOMER_PASSWORD"))
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
                customer.Password = MD5Helper.Encode(model.Password);

                _context.Update(customer);

                await _context.SaveChangesAsync();

                ViewData["Message"] = "Customer password updated successfully.";
                return RedirectToAction("EditPassword");
            }
            return View(model);
        }
    }
}
