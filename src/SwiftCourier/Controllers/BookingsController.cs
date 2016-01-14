using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using System;
using System.Security.Claims;

namespace SwiftCourier.Controllers
{
    public class BookingsController : BaseController
    {
        private ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Invoice)
                .Include(b => b.CreatedBy)
                .Include(b => b.Service).ToListAsync();

            return View(bookings.ToListViewModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Service)
                .Include(b => b.CreatedBy)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking.ToDetailsViewModel());
        }

        public async Task<IActionResult> Invoice(int id, string print = "")
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Invoice.Payments)
                .ThenInclude(p => p.PaymentMethod)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Package)
                .Include(b => b.Service)
                .Include(b => b.CreatedBy)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            if (print.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return View("Invoice_Print", booking.ToDetailsViewModel());
            }

            return View(booking.ToDetailsViewModel());
        }

        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = model.ToEntity();

                booking.CreatedAt = DateTime.Now;

                int userId;

                if (!int.TryParse(HttpContext.User.GetUserId(), out userId))
                {
                    //XXX:TODO Gracefully handle this
                    throw new Exception("Unable to get logged in User Id.");
                }

                booking.CreatedByUserId = userId;

                if (booking.Invoice != null)
                {
                    booking.Invoice.Status = InvoiceStatus.Pending;
                    booking.Invoice.AmountDue = booking.Invoice.Total;
                    booking.Invoice.AmountPaid = 0;
                }

                if (booking.Package != null)
                {
                    booking.Package.TrackingNumber = Guid.NewGuid().ToString();

                    if (booking.PickupRequired)
                    {
                        booking.Package.Status = PackageStatus.PendingPickup;
                    }
                    else
                    {
                        booking.Package.Status = PackageStatus.ReceivedByLocation;
                    }
                }

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                if (booking.Invoice.BillingMode == BillingMode.PayNow)
                {
                    return RedirectToAction("Invoice", "Bookings", new { id = booking.Id });
                }

                return RedirectToAction("Details", "Bookings", new { id = booking.Id });
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", model.CustomerId);
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            var model = booking.ToViewModel();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", model.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {

                var booking = await _context.Bookings
                    .Include(b => b.Invoice)
                    .Include(b => b.Package)
                    .Include(b => b.Package.PackageType)
                    .SingleAsync(m => m.Id == model.Id);

                booking = model.UpdateEntity(booking);

                if (booking.Invoice.AmountPaid >= booking.Invoice.Total)
                {
                    booking.Invoice.Status = InvoiceStatus.Paid;
                    booking.Invoice.AmountDue = 0;
                }

                _context.Update(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", model.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings
                            .Include(b => b.Customer)
                            .Include(b => b.Invoice)
                            .Include(b => b.Package)
                            .Include(b => b.Package.PackageType)
                            .Include(b => b.Service)
                            .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking.ToDetailsViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
