using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using SwiftCourier.Models;

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

        [Route("api/customer/{id}")]
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
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