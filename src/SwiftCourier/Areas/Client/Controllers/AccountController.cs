using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SwiftCourier.Data;
using Microsoft.AspNetCore.Mvc;
using SwiftCourier.Models;
using SwiftCourier.Utilities;
using Microsoft.EntityFrameworkCore;

namespace SwiftCourier.Areas.Client.Controllers
{
    [Area("Client")]
    public class AccountController : BaseController
    {
        public AccountController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult EditPassword()
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            return View(new CustomerPasswordViewModel() { Id = customer.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(CustomerPasswordViewModel model)
        {
            var customer = GetCurrentClient();

            if (customer == null)
            {
                return RedirectToAction("Login", "Auth", new { Area = "Client" });
            }

            if (ModelState.IsValid)
            {
                customer.Password = MD5Helper.Encode(model.Password);

                _dbContext.Entry(customer).State = EntityState.Modified;

                _dbContext.Update(customer);

                await _dbContext.SaveChangesAsync();

                ViewData["Message"] = "Password updated successfully.";

                //return RedirectToAction("EditPassword");
            }

            return View(model);
        }
    }
}
