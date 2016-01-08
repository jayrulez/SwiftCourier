using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using System;

namespace SwiftCourier.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;    
        }
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service);
            return View(await applicationDbContext.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Service)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
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

                booking.PickupStatus = PickupStatus.Pending;
                
                booking.DeliveryStatus = DeliveryStatus.Pending;
                
                if(booking.Invoice != null)
                {
                    booking.Invoice.Status = InvoiceStatus.PendingPayment;
                }
                
                if(booking.Package != null)
                {
                    booking.Package.Status = PackageStatus.Default;
                }

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", model.CustomerId);
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
                    .SingleAsync(m => m.Id == model.Id);

                model.UpdateEntity(booking);

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
