using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SwiftCourier.Models.Extensions;
using SwiftCourier.Web.Data;
using SwiftCourier.Web.Models.Enums;
using SwiftCourier.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Controllers
{
    public class PaymentsController : BaseController
    {
        public PaymentsController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {
        }

        public async Task<IActionResult> Create(int id)
        {
            if (!HasPermission("PROCESS_PAYMENTS"))
            {
                return Unauthorized();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Payments)
                .SingleAsync(m => m.BookingId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            if(invoice.AmountDue <= 0)
            {
                return RedirectToAction("Details", "Bookings", new {
                    id = id
                });
            }

            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name");

            return View(new PaymentViewModel() {
                AmountDue = invoice.AmountDue,
                Amount = invoice.AmountDue
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, PaymentViewModel model)
        {
            if (!HasPermission("PROCESS_PAYMENTS"))
            {
                return Unauthorized();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Payments)
                .SingleAsync(m => m.BookingId == id);

            if(invoice == null)
            {
                return NotFound();
            }

            if (invoice.AmountDue <= 0)
            {
                return RedirectToAction("Details", "Bookings", new
                {
                    id = id
                });
            }

            if (ModelState.IsValid)
            {
                var payment = model.ToEntity();
                payment.ProcessedAt = DateTime.Now;

                int userId = GetCurrentUserId();

                payment.UserId = userId;

                invoice.Payments.Add(payment);
                invoice.AmountPaid += payment.Amount;
                invoice.AmountDue -= payment.Amount;

                if(invoice.AmountDue <= 0)
                {
                    invoice.Status = InvoiceStatus.Paid;
                }

                _context.Update(invoice);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Bookings", new
                {
                    id = id
                });
            }

            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "Name");

            return View(model);
        }

        public async Task<IActionResult> Details(int id, string print = "")
        {
            if (!HasPermission("PROCESS_PAYMENTS"))
            {
                return Unauthorized();
            }

            var payment = await _context.Payments
                .Include(p => p.PaymentMethod)
                .Include(p => p.User)
                .SingleAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            if(print.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return View("Details_Print", payment.ToDetailsViewModel());
            }
            return View(payment.ToDetailsViewModel());
        }
    }
}
