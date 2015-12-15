using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;

namespace SwiftCourier.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Policy ="HasPermission")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
