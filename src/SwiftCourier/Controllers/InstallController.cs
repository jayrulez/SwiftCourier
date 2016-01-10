using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using SwiftCourier.ViewModels;

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
                var user = new User
                {
                    UserName = model.UserName
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
