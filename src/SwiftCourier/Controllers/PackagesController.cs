using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SwiftCourier.Controllers
{
    public class PackagesController : BaseController
    {
        private ApplicationDbContext _context;

        public PackagesController(ApplicationDbContext context)
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

        public async Task<IActionResult> Dispatch(int id)
        {
            var booking = await _context.Bookings
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

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dispatch(int id, DispatchViewModel model)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Package.PackageLogs)
                .Include(b => b.Service)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            int userId;

            if (!int.TryParse(HttpContext.User.GetUserId(), out userId))
            {
                //XXX:TODO Gracefully handle this
                throw new Exception("Unable to get logged in User Id.");
            }

            if (ModelState.IsValid)
            {
                booking.Package.AssignedToUserId = model.UserId;

                //XXX:TODO Fix when couriers are able to receive packages to their personal inventory
                // Update status to OutForDelivery when the courier has confirmed receipt of the package
                // For now, set as out for delivery when package is dispatched to courier
                //booking.Package.Status = PackageStatus.DispatchedToCourier;
                booking.Package.Status = PackageStatus.OutForDelivery;

                var packageLog = new PackageLog()
                {
                    PackageId = booking.Package.BookingId,
                    //XXX:TODO See comment above
                    //LogMessage = "Dispatched To Courier",
                    LogMessage = string.Format("Received By Courier {0}.", model.UserId),
                    LoggedAt = DateTime.Now
                };

                booking.Package.PackageLogs.Add(packageLog);

                _context.Update(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Packages", new { id = booking.Id });
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");

            return View(model);
        }

        public async Task<IActionResult> Deliver(int id)
        {
            var booking = await _context.Bookings
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

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deliver(int id, DeliverViewModel model)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Package.PackageLogs)
                .Include(b => b.Service)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return HttpNotFound();
            }

            int userId;

            if (!int.TryParse(HttpContext.User.GetUserId(), out userId))
            {
                //XXX:TODO Gracefully handle this
                throw new Exception("Unable to get logged in User Id.");
            }

            if (ModelState.IsValid)
            {
                booking.Package.DeliveredByUserId = model.UserId;
                booking.Package.DeliveredAt = DateTime.Now;
                booking.Package.Status = PackageStatus.Delivered;

                var packageLog = new PackageLog()
                {
                    PackageId = booking.Package.BookingId,
                    LogMessage = string.Format("Delivered To Consignee by Courier {0}.", model.UserId),
                    LoggedAt = DateTime.Now
                };

                booking.Package.PackageLogs.Add(packageLog);

                _context.Update(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Packages", new { id = booking.Id });
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");

            return View(model);
        }

        public async Task<IActionResult> BillOfLading(int id, string print = "")
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

            ViewData["Services"] = _context.Services.ToList();

            if (print.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return View("BillOfLading_Print", booking.ToDetailsViewModel());
            }

            return View(booking.ToDetailsViewModel());
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Package.PackageLogs)
                .Include(b => b.Service)
                .Include(b => b.CreatedBy)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking.ToDetailsViewModel());
        }
    }
}
