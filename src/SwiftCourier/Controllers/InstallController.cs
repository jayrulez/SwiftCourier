using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using SwiftCourier.ViewModels;
using System.Collections.Generic;

namespace SwiftCourier.Controllers
{
    public class InstallController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        private ApplicationDbContext _context;

        public InstallController(
            ApplicationDbContext context,
            UserManager<User> userManager,
            ILoggerFactory loggerFactory)
        {
            _context = context;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger<InstallController>();
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(InstallationViewModel model)
        {
            if(ModelState.IsValid)
            {
                var paymentMethods = new List<PaymentMethod>() {
                    new PaymentMethod() { Name = "Cash" },
                    new PaymentMethod() { Name = "Credit Card" },
                    new PaymentMethod() { Name = "Debit Card" },
                    new PaymentMethod() { Name = "Cheque" }
                };

                foreach(var paymentMethod in paymentMethods)
                {
                    _context.PaymentMethods.Add(paymentMethod);
                }

                var services = new List<Service>() {
                    new Service() { Name = "Same Day", Cost = 500 },
                    new Service() { Name = "Next Day", Cost = 400 },
                    new Service() { Name = "Round Town", Cost = 300 }
                };

                foreach(var service in services)
                {
                    _context.Services.Add(service);
                }

                var settings = new List<Setting>() {
                    new Setting() { Name = "base_weight", DisplayName ="Base Weight", Description="Base Weight", Value="10" },
                    new Setting() { Name = "cost_per_unit", DisplayName ="Cost Per Pound", Description="Cost Per Pound", Value="15" }
                };

                foreach (var setting in settings)
                {
                    _context.Settings.Add(setting);
                }

                _context.SaveChanges();

                var user = new User
                {
                    UserName = model.UserName,
                    UserType = UserType.NORMAL_USER
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            return View(model);
        }
    }
}
