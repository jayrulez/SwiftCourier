using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SwiftCourier.Helpers;
using System;
using SwiftCourier.Data;
using SwiftCourier.Models.Enums;
using SwiftCourier.Models.Extensions;

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

        [Route("api/get_cost_data")]
        [HttpGet]
        public IActionResult GetCostData(int customerId, int serviceId, decimal weight, DiscountType discountType, decimal discountValue)
        {
            var data = "{\"success\": false}";
            var gctRate = new Decimal(0.165);

            var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId);

            if(customer != null && customer.TaxExempted)
            {
                gctRate = 0;
            }

            var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);

            if(service != null)
            {
                var baseWeight = new Decimal(10);
                var baseWeightRecord = _context.Settings.FirstOrDefault(s => s.Name == "base_weight");

                if(baseWeightRecord != null)
                {
                    baseWeight = Decimal.Parse(baseWeightRecord.Value);
                }

                var costPerUnitOverBaseWeight = new Decimal(55);
                var costPerUnitOverBaseWeightRecord = _context.Settings.FirstOrDefault(s => s.Name == "cost_per_unit");

                if(costPerUnitOverBaseWeightRecord != null)
                {
                    costPerUnitOverBaseWeight = Decimal.Parse(costPerUnitOverBaseWeightRecord.Value);
                }

                var serviceCost = service.Cost;
                var overWeight = weight - baseWeight;
                if(overWeight < 0)
                {
                    overWeight = 0;
                }

                var additionalCost = overWeight * costPerUnitOverBaseWeight;

                var additionalCostGCT = additionalCost * gctRate;

                var additionalCostTotal = additionalCost + additionalCostGCT;

                additionalCostTotal = Number.Round(additionalCostTotal);

                serviceCost = Number.Round(serviceCost);

                // Do discount before adding additionalcosttotal to service cost

                var total = serviceCost;

                //Discount
                var discount = new Decimal(0);

                if (discountType == DiscountType.FlatAmount)
                {
                    discount = discountValue;
                }

                if (discountType == DiscountType.Percentage)
                {
                    discount = Number.Round(total * (discountValue / 100));
                }

                total = Number.Round(total - discount);

                //GCT
                var gct = gctRate * total;

                gct = Number.Round(gct);

                total = Number.Round(total + gct);

                total = total + additionalCostGCT;

                data = "{\"service_cost\": {0}, \"gct\": {1}, \"total\": {2}, \"discount_amount\": {3}, \"success\": {4} }"
                    .Replace("{0}", serviceCost.ToString())
                    .Replace("{1}", gct.ToString())
                    .Replace("{2}", total.ToString())
                    .Replace("{3}", discount.ToString())
                    .Replace("{4}", true.ToString().ToLower());
            }

            return Content(data);
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