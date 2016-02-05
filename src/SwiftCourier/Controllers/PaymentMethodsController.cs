using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using SwiftCourier.ViewModels;
using Microsoft.AspNet.Identity;

namespace SwiftCourier.Controllers
{
    public class PaymentMethodsController : BaseController
    {
        public PaymentMethodsController(
            UserManager<User> userManager, ApplicationDbContext context) : base(userManager, context)
        {  
        }
        
        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            var paymentMethods = await _context.PaymentMethods.ToListAsync();

            return View(paymentMethods.ToListViewModel());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (!HasPermission("VIEW_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            return View(paymentMethod.ToDetailsViewModel());
        }
        
        public IActionResult Create()
        {
            if (!HasPermission("CREATE_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethodViewModel model)
        {
            if (!HasPermission("CREATE_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var paymentMethod = model.ToEntity();

                _context.PaymentMethods.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (!HasPermission("EDIT_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethod.ToViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentMethodViewModel model)
        {
            if (!HasPermission("EDIT_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == model.Id);
                if (paymentMethod == null)
                {
                    return HttpNotFound();
                }

                paymentMethod = model.UpdateEntity(paymentMethod);
                _context.Update(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HasPermission("DELETE_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }

            if (id == null)
            {
                return HttpNotFound();
            }

            var paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            return View(paymentMethod.ToDetailsViewModel());
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_PAYMENT_METHODS"))
            {
                return Unauthorized();
            }
            PaymentMethod paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
