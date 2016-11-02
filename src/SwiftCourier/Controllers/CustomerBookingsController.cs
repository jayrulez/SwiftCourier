using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SwiftCourier.Controllers
{
    public class CustomerBookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}