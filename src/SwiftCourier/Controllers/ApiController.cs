using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using Newtonsoft.Json;

namespace SwiftCourier.Controllers
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [Route("api/services")]
        [HttpGet]
        public IEnumerable<Service> GetServices()
        {
            return _context.Services;
        }

        [Route("api/service/{id}")]
        [HttpGet]
        public Service GetService(int id)
        {
            return _context.Services.FirstOrDefault(s => s.Id == id);
        }

        [Route("api/settings")]
        [HttpGet]
        public Setting GetSetting(string name)
        {
            return _context.Settings.FirstOrDefault(s => s.Name == name);
        }

        [Route("api/customer/{id}")]
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        [Route("api/customers")]
        [HttpGet]
        public IActionResult GetCustomers(string q = "")
        {
            var customers = _context.Customers.Where(c => c.Name.StartsWith(q, System.StringComparison.OrdinalIgnoreCase) || c.Name.Contains(q)).ToList();

            var list = customers.ToListViewModel();

            var data = JsonConvert.SerializeObject(list);

            var result = "{\"total_count\": {count}, \"incomplete_results\": false, \"items\": {data}}";

            var response = result.Replace("{count}", customers.Count.ToString()).Replace("{data}", data);

            return Content(response);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}