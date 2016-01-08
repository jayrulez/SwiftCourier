using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;

namespace SwiftCourier.Controllers
{
    public class BookingsController : Controller
    {
        private ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bookings.Include(b => b.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");

            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var booking = new Booking();

                booking.CustomerId = model.CustomerId;

                booking.ServiceId = model.ServiceId;
                booking.BillingMode = model.BillingMode;

                booking.PickupAddress = model.PickupAddress;
                booking.PickupContactNumber = model.PickupContactNumber;
                booking.PickupStatus = PickupStatus.Pending;

                booking.ConsigneeName = model.ConsigneeName;
                booking.ConsigneeAddress = model.ConsigneeAddress;
                booking.ConsigneeContactNumber = model.ConsigneeContactNumber;
                booking.DeliveryStatus = DeliveryStatus.Pending;

                var invoice = new Invoice();

                invoice.ServiceCost = model.Invoice.ServiceCost;
                invoice.GCT = model.Invoice.GCT;
                invoice.Total = model.Invoice.Total;

                booking.Invoice = invoice;

                //var package = new Package();
                //booking.Package = package;

                _context.Bookings.Add(booking);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", model.CustomerId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Customer", booking.CustomerId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Customer", booking.CustomerId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
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
