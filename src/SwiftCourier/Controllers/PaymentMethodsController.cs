using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;

namespace SwiftCourier.Controllers
{
    public class PaymentMethodsController : BaseController
    {
        private ApplicationDbContext _context;

        public PaymentMethodsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentMethods.ToListAsync());
        }

        // GET: PaymentMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PaymentMethod paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _context.PaymentMethods.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PaymentMethod paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Update(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PaymentMethod paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return HttpNotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            PaymentMethod paymentMethod = await _context.PaymentMethods.SingleAsync(m => m.Id == id);
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
