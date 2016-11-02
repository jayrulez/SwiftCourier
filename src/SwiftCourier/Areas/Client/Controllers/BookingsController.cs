using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftCourier.Data;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using SwiftCourier.Models.Enums;
using SwiftCourier.Areas.Client.Models;

namespace SwiftCourier.Areas.Client.Controllers
{
    [Area("Client")]
    public class BookingsController : BaseController
    {
        public BookingsController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<IActionResult> Index()
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            var bookings = await _dbContext.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Invoice)
                .Include(b => b.CreatedBy)
                .Include(b => b.Service)
                .Where(b => b.CustomerId == customer.Id)
                .ToListAsync();

            return View(bookings.ToListViewModel());
        }
        public IActionResult DetailsAction()
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { area = "Client" });
            }
            return View();
        }

        public IActionResult Create()
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            ViewData["LocationId"] = new SelectList(_dbContext.Locations, "Id", "Name");
            ViewData["PackageTypeId"] = new SelectList(_dbContext.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_dbContext.Services, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel model)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            if (ModelState.IsValid)
            {
                var booking = new Booking();

                booking.CustomerId = customer.Id;
                booking.ServiceId = model.ServiceId;
                booking.OriginLocationId = model.OriginLocationId;
                booking.DestinationLocationId = model.DestinationLocationId;
                booking.RequestDate = model.RequestDate;
                booking.PickupRequired = model.PickupRequired;
                booking.PickupAddress = model.PickupAddress;
                booking.PickupContactNumber = model.PickupContactNumber;
                booking.ConsigneeName = model.ConsigneeName;
                booking.ConsigneeContactNumber = model.ConsigneeContactNumber;
                booking.ConsigneeAddress = model.ConsigneeAddress;
                booking.CreatedAt = DateTime.Now;

                booking.Package = new Package()
                {
                    Description = model.PackageDescription,
                    Pieces = model.Pieces,
                    Weight = model.Weight,
                    TrackingNumber = DateTime.Now.Ticks.ToString(),
                    Status = booking.PickupRequired ? PackageStatus.PendingPickup : PackageStatus.ReceivedByLocation,
                    SpecialInstructions = model.SpecialInstructions,
                    ReferenceNumber = model.ReferenceNumber,
                    PackageTypeId = model.PackageTypeId
                };

                booking.Invoice = new Invoice()
                {
                    BillingMode = BillingMode.BillToAccount,
                    AmountDue = 0,
                    AmountPaid = 0,
                    DiscountType = DiscountType.None,
                    DiscountValue = 0,
                    GCT = 0,
                    DiscountAmount = 0,
                    ServiceCost = 0,
                    Total = 0,
                    Status = InvoiceStatus.NotIssued

                };

                _dbContext.Bookings.Add(booking);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Details", "Bookings", new { area = "Client", id = booking.Id });
            }

            ViewData["LocationId"] = new SelectList(_dbContext.Locations, "Id", "Name");
            ViewData["PackageTypeId"] = new SelectList(_dbContext.PackageTypes, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_dbContext.Services, "Id", "Name", model.ServiceId);

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            if (id == null)
            {
                return NotFound();
            }

            Booking booking = await _dbContext.Bookings
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .SingleAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            if (booking.CustomerId != customer.Id)
            {
                return Unauthorized();
            }

            if(booking.Invoice.Status != InvoiceStatus.NotIssued)
            {
                return Unauthorized();
            }

            var model = new EditBookingViewModel()
            {
                Id = booking.Id,
                ServiceId = booking.ServiceId,
                RequestDate = booking.RequestDate,
                OriginLocationId = booking.OriginLocationId,
                DestinationLocationId = booking.DestinationLocationId,
                PickupRequired = booking.PickupRequired,
                PickupAddress = booking.PickupAddress,
                PickupContactNumber = booking.PickupContactNumber,
                ConsigneeName = booking.ConsigneeName,
                ConsigneeAddress = booking.ConsigneeAddress,
                ConsigneeContactNumber = booking.ConsigneeContactNumber,
                PackageDescription = booking.Package.Description,
                Pieces = booking.Package.Pieces,
                Weight = booking.Package.Weight,
                SpecialInstructions = booking.Package.SpecialInstructions,
                ReferenceNumber = booking.Package.ReferenceNumber,
                PackageTypeId = booking.Package.PackageTypeId
            };

            ViewData["LocationId"] = new SelectList(_dbContext.Locations, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_dbContext.Services, "Id", "Name", model.ServiceId);
            ViewData["PackageTypeId"] = new SelectList(_dbContext.PackageTypes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookingViewModel model)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            if (ModelState.IsValid)
            {

                var booking = await _dbContext.Bookings
                    .Include(b => b.Invoice)
                    .Include(b => b.Package)
                    .Include(b => b.Package.PackageType)
                    .SingleAsync(m => m.Id == model.Id);

                if (booking == null)
                {
                    return NotFound();
                }

                if (booking.CustomerId != customer.Id)
                {
                    return Unauthorized();
                }

                if (booking.Invoice.Status != InvoiceStatus.NotIssued)
                {
                    return Unauthorized();
                }

                booking.ServiceId = model.ServiceId;
                booking.RequestDate = model.RequestDate;
                booking.OriginLocationId= model.OriginLocationId;
                booking.DestinationLocationId= model.DestinationLocationId;
                booking.PickupRequired = model.PickupRequired;
                booking.PickupAddress = model.PickupAddress;
                booking.PickupContactNumber = model.PickupContactNumber;
                booking.ConsigneeName = model.ConsigneeName;
                booking.ConsigneeAddress = model.ConsigneeAddress;
                booking.ConsigneeContactNumber = model.ConsigneeContactNumber;
                booking.Package.Description = model.PackageDescription;
                booking.Package.Pieces = model.Pieces;
                booking.Package.Weight = model.Weight;
                booking.Package.SpecialInstructions = model.SpecialInstructions;
                booking.Package.ReferenceNumber = model.ReferenceNumber;
                booking.Package.PackageTypeId = model.PackageTypeId;

                _dbContext.Update(booking);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewData["LocationId"] = new SelectList(_dbContext.Locations, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_dbContext.Services, "Id", "Name", model.ServiceId);
            ViewData["PackageTypeId"] = new SelectList(_dbContext.PackageTypes, "Id", "Name");

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            var booking = await _dbContext.Bookings
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

            if (booking.CustomerId != customer.Id)
            {
                return Unauthorized();
            }

            return View(booking.ToDetailsViewModel());
        }

        public async Task<IActionResult> BillOfLading(int id)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            var booking = await _dbContext.Bookings
                .Include(b => b.Origin)
                .Include(b => b.Destination)
                .Include(b => b.Customer)
                .Include(b => b.Invoice)
                .Include(b => b.Package)
                .Include(b => b.Package.PackageType)
                .Include(b => b.Service)
                .Include(b => b.CreatedBy)
                .SingleAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            if (booking.CustomerId != customer.Id)
            {
                return Unauthorized();
            }

            ViewData["Services"] = _dbContext.Services.ToList();

            return View(booking.ToDetailsViewModel());
        }
    }
}
