using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Identity;
using SwiftCourier.Data;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using SwiftCourier.Models.Enums;
using SwiftCourier.Models;
using System.Linq;

namespace SwiftCourier.Controllers
{
    public class BookingsController : BaseController
    {
        public BookingsController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }

        public IActionResult Index(int? page)
        {
            if (!HasPermission("VIEW_BOOKINGS"))
            {
                return Unauthorized();
            }

            var pageNumber = page ?? 1;

            var pageSize = 50;

            var bookings = _context.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Invoice)
                .Include(b => b.CreatedBy)
                .Include(b => b.Service)
                .OrderByDescending(b => b.CreatedAt);

            var bookingsPage = bookings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItemCount"] = bookings.Count();

            return View(bookingsPage.ToListViewModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!HasPermission("VIEW_BOOKINGS"))
            {
                return Unauthorized();
            }

            var booking = await _context.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Invoice.Payments)
                .ThenInclude(p => p.PaymentMethod)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Package.PackageLogs)
                .Include(b => b.Service)
                .Include(b => b.CreatedBy)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking.ToDetailsViewModel());
        }

        public async Task<IActionResult> Invoice(int id)
        {
            if (!HasPermission("VIEW_BOOKINGS"))
            {
                return Unauthorized();
            }

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
                return NotFound();
            }

            return View(booking.ToDetailsViewModel());
        }

        public IActionResult Create()
        {
            if (!HasPermission("CREATE_BOOKINGS"))
            {
                return Unauthorized();
            }

            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!HasPermission("CREATE_BOOKINGS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var booking = model.ToEntity();

                booking.CreatedAt = DateTime.Now;

                int userId = GetCurrentUserId();

                booking.CreatedByUserId = userId;

                if (booking.Invoice != null)
                {
                    booking.Invoice.Status = InvoiceStatus.Pending;
                    booking.Invoice.AmountDue = booking.Invoice.Total;
                    booking.Invoice.AmountPaid = 0;
                }

                if (booking.Package != null)
                {
                    booking.Package.TrackingNumber = DateTime.Now.Ticks.ToString();

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
                    return RedirectToAction("Create", "Payments", new { id = booking.Id });
                }

                return RedirectToAction("Details", "Bookings", new { id = booking.Id });
            }

            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!HasPermission("EDIT_BOOKINGS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            Booking booking = await _context.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            var model = booking.ToViewModel();

            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingViewModel model)
        {
            if (!HasPermission("EDIT_BOOKINGS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {

                var booking = await _context.Bookings
                    .Include(b => b.Invoice)
                    .Include(b => b.Package)
                    .Include(b => b.Package.PackageType)
                    .SingleAsync(m => m.Id == model.Id);

                booking = model.UpdateEntity(booking);

                if (booking.Invoice.Status == InvoiceStatus.NotIssued)
                {
                    booking.Invoice.Status = InvoiceStatus.Pending;
                }

                if (booking.Invoice.AmountPaid >= booking.Invoice.Total)
                {
                    booking.Invoice.Status = InvoiceStatus.Paid;
                    booking.Invoice.AmountDue = 0;
                }

                _context.Update(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);
            ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");

            return View(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HasPermission("DELETE_BOOKINGS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Service)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking.ToDetailsViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_BOOKINGS"))
            {
                return Unauthorized();
            }

            Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
