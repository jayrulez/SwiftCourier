using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftCourier.Areas.Client.Models;
using SwiftCourier.Data;
using SwiftCourier.Models;
using SwiftCourier.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Areas.Client.Controllers
{
    [Area("Client")]
    public class AuthController : BaseController
    {
        public AuthController(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => model.Username.Equals(c.Username, StringComparison.OrdinalIgnoreCase));

                if (customer == null)
                {
                    ModelState.AddModelError("Username", "Invalid Username");
                }
                else if (!customer.Password.Equals(MD5Helper.Encode(model.Password)))
                {
                    ModelState.AddModelError("Password", "Invalid Password");
                }
                else
                {
                    Request.HttpContext.Session.SetInt32("clientId", customer.Id);

                    return RedirectToAction("Index", "Bookings", new { area = "Client" });
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            Request.HttpContext.Session.Remove("clientId");

            return RedirectToAction("Login", "Auth", new { area = "Client" });
        }
    }
}
