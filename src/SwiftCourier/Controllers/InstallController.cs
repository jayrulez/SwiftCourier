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
        
        public IActionResult InsertPermissions()
        {
            var permissions = new List<Permission>()
                {
                    new Permission() { Group = "bookings", Name = "CREATE_BOOKINGS", Description = "Create Bookings" },
                    new Permission() { Group = "bookings", Name = "EDIT_BOOKINGS", Description = "Edit Bookings" },
                    new Permission() { Group = "bookings", Name = "VIEW_BOOKINGS", Description = "View Bookings" },
                    new Permission() { Group = "bookings", Name = "DELETE_BOOKINGS", Description = "Delete Bookings" },
                    new Permission() { Group = "bookings", Name = "PROCESS_PAYMENTS", Description = "Process Payments" },

                    new Permission() { Group = "user_management", Name = "CREATE_USERS", Description = "Create Users" },
                    new Permission() { Group = "user_management", Name = "EDIT_USERS", Description = "Edit Users" },
                    new Permission() { Group = "user_management", Name = "VIEW_USERS", Description = "View Users" },
                    new Permission() { Group = "user_management", Name = "DELETE_USERS", Description = "Delete Users" },

                    new Permission() { Group = "customer_management", Name = "CREATE_CUSTOMERS", Description = "Create Customers" },
                    new Permission() { Group = "customer_management", Name = "EDIT_CUSTOMERS", Description = "Edit Customers" },
                    new Permission() { Group = "customer_management", Name = "VIEW_CUSTOMERS", Description = "View Customers" },
                    new Permission() { Group = "customer_management", Name = "DELETE_CUSTOMERS", Description = "Delete Customers" },

                    new Permission() { Group = "administration", Name = "CREATE_LOCATIONS", Description = "Create Locations" },
                    new Permission() { Group = "administration", Name = "EDIT_LOCATIONS", Description = "Edit Locations" },
                    new Permission() { Group = "administration", Name = "VIEW_LOCATIONS", Description = "View Locations" },
                    new Permission() { Group = "administration", Name = "DELETE_LOCATIONS", Description = "Delete Locations" },

                    //new Permission() { Group = "administration", Name = "CREATE_USERS", Description = "Create Users" },
                    //new Permission() { Group = "administration", Name = "EDIT_USERS", Description = "Edit Users" },
                    //new Permission() { Group = "administration", Name = "VIEW_USERS", Description = "View Users" },
                    //new Permission() { Group = "administration", Name = "DELETE_USERS", Description = "Delete Users" },

                    new Permission() { Group = "settings", Name = "CREATE_SERVICES", Description = "Create Services" },
                    new Permission() { Group = "settings", Name = "EDIT_SERVICES", Description = "Edit Services" },
                    new Permission() { Group = "settings", Name = "VIEW_SERVICES", Description = "View Services" },
                    new Permission() { Group = "settings", Name = "DELETE_SERVICES", Description = "Delete Services" },

                    new Permission() { Group = "settings", Name = "CREATE_PACKAGE_TYPES", Description = "Create Package Types" },
                    new Permission() { Group = "settings", Name = "EDIT_PACKAGE_TYPES", Description = "Edit Package Types" },
                    new Permission() { Group = "settings", Name = "VIEW_PACKAGE_TYPES", Description = "View Package Types" },
                    new Permission() { Group = "settings", Name = "DELETE_PACKAGE_TYPES", Description = "Delete Package Types" },

                    new Permission() { Group = "settings", Name = "CREATE_PAYMENT_METHODS", Description = "Create Payment Methods" },
                    new Permission() { Group = "settings", Name = "EDIT_PAYMENT_METHODS", Description = "Edit Payment Methods" },
                    new Permission() { Group = "settings", Name = "VIEW_PAYMENT_METHODS", Description = "View Payment Methods" },
                    new Permission() { Group = "settings", Name = "DELETE_PAYMENT_METHODS", Description = "Delete Payment Methods" },

                    new Permission() { Group = "settings", Name = "CREATE_ROLES", Description = "Create Roles" },
                    new Permission() { Group = "settings", Name = "EDIT_ROLES", Description = "Edit Roles" },
                    new Permission() { Group = "settings", Name = "VIEW_ROLES", Description = "View Roles" },
                    new Permission() { Group = "settings", Name = "DELETE_ROLES", Description = "Delete Roles" },

                    new Permission() { Group = "general", Name = "DISPATCH", Description = "Dispatch Package" },
                    new Permission() { Group = "general", Name = "DELIVER", Description = "Deliver Package" },
                    new Permission() { Group = "general", Name = "VIEW_SETTINGS", Description = "View General Settings" },
                    new Permission() { Group = "general", Name = "EDIT_SETTINGS", Description = "Change General Settings" }
                };

            foreach (var permission in permissions)
            {
                _context.Permissions.Add(permission);
            }

            _context.SaveChanges();

            return Content("Inserted.");
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

                var permissions = new List<Permission>()
                {
                    new Permission() { Group = "bookings", Name = "CREATE_BOOKINGS", Description = "Create Bookings" },
                    new Permission() { Group = "bookings", Name = "EDIT_BOOKINGS", Description = "Edit Bookings" },
                    new Permission() { Group = "bookings", Name = "VIEW_BOOKINGS", Description = "View Bookings" },
                    new Permission() { Group = "bookings", Name = "DELETE_BOOKINGS", Description = "Delete Bookings" },
                    new Permission() { Group = "bookings", Name = "PROCESS_PAYMENTS", Description = "Process Payments" },

                    new Permission() { Group = "user_management", Name = "CREATE_USERS", Description = "Create Users" },
                    new Permission() { Group = "user_management", Name = "EDIT_USERS", Description = "Edit Users" },
                    new Permission() { Group = "user_management", Name = "VIEW_USERS", Description = "View Users" },
                    new Permission() { Group = "user_management", Name = "DELETE_USERS", Description = "Delete Users" },

                    new Permission() { Group = "customer_management", Name = "CREATE_CUSTOMERS", Description = "Create Customers" },
                    new Permission() { Group = "customer_management", Name = "EDIT_CUSTOMERS", Description = "Edit Customers" },
                    new Permission() { Group = "customer_management", Name = "VIEW_CUSTOMERS", Description = "View Customers" },
                    new Permission() { Group = "customer_management", Name = "DELETE_CUSTOMERS", Description = "Delete Customers" },

                    new Permission() { Group = "administration", Name = "CREATE_LOCATIONS", Description = "Create Locations" },
                    new Permission() { Group = "administration", Name = "EDIT_LOCATIONS", Description = "Edit Locations" },
                    new Permission() { Group = "administration", Name = "VIEW_LOCATIONS", Description = "View Locations" },
                    new Permission() { Group = "administration", Name = "DELETE_LOCATIONS", Description = "Delete Locations" },

                    //new Permission() { Group = "administration", Name = "CREATE_USERS", Description = "Create Users" },
                    //new Permission() { Group = "administration", Name = "EDIT_USERS", Description = "Edit Users" },
                    //new Permission() { Group = "administration", Name = "VIEW_USERS", Description = "View Users" },
                    //new Permission() { Group = "administration", Name = "DELETE_USERS", Description = "Delete Users" },

                    new Permission() { Group = "settings", Name = "CREATE_SERVICES", Description = "Create Services" },
                    new Permission() { Group = "settings", Name = "EDIT_SERVICES", Description = "Edit Services" },
                    new Permission() { Group = "settings", Name = "VIEW_SERVICES", Description = "View Services" },
                    new Permission() { Group = "settings", Name = "DELETE_SERVICES", Description = "Delete Services" },

                    new Permission() { Group = "settings", Name = "CREATE_PACKAGE_TYPES", Description = "Create Package Types" },
                    new Permission() { Group = "settings", Name = "EDIT_PACKAGE_TYPES", Description = "Edit Package Types" },
                    new Permission() { Group = "settings", Name = "VIEW_PACKAGE_TYPES", Description = "View Package Types" },
                    new Permission() { Group = "settings", Name = "DELETE_PACKAGE_TYPES", Description = "Delete Package Types" },

                    new Permission() { Group = "settings", Name = "CREATE_PAYMENT_METHODS", Description = "Create Payment Methods" },
                    new Permission() { Group = "settings", Name = "EDIT_PAYMENT_METHODS", Description = "Edit Payment Methods" },
                    new Permission() { Group = "settings", Name = "VIEW_PAYMENT_METHODS", Description = "View Payment Methods" },
                    new Permission() { Group = "settings", Name = "DELETE_PAYMENT_METHODS", Description = "Delete Payment Methods" },

                    new Permission() { Group = "settings", Name = "CREATE_ROLES", Description = "Create Roles" },
                    new Permission() { Group = "settings", Name = "EDIT_ROLES", Description = "Edit Roles" },
                    new Permission() { Group = "settings", Name = "VIEW_ROLES", Description = "View Roles" },
                    new Permission() { Group = "settings", Name = "DELETE_ROLES", Description = "Delete Roles" },

                    new Permission() { Group = "general", Name = "DISPATCH", Description = "Dispatch Package" },
                    new Permission() { Group = "general", Name = "DELIVER", Description = "Deliver Package" },
                    new Permission() { Group = "general", Name = "VIEW_SETTINGS", Description = "View General Settings" },
                    new Permission() { Group = "general", Name = "EDIT_SETTINGS", Description = "Change General Settings" }
                };

                foreach(var permission in permissions)
                {
                    _context.Permissions.Add(permission);
                }

                _context.SaveChanges();

                //var roles = new List<Role>()
                //{
                //    new Role() { Name = "Administrator" }
                //};

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
