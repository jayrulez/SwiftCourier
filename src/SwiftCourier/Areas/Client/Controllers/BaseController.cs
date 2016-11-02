using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwiftCourier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Areas.Client.Controllers
{
    abstract public class BaseController : Controller
    {
        protected ApplicationDbContext _dbContext;
        public BaseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsLoggedIn()
        {
            var clientId = Request.HttpContext.Session.GetInt32("clientId");

            try
            {
                var client = _dbContext.Customers.FirstOrDefault(c => c.Id == clientId);
                return true;
            }catch(Exception)
            {

            }

            return false;
        }

        public Customer GetCurrentClient()
        {
            var clientId = Request.HttpContext.Session.GetInt32("clientId");
            var client = _dbContext.Customers.FirstOrDefault(c => c.Id == clientId);

            return client;
        }
    }
}
