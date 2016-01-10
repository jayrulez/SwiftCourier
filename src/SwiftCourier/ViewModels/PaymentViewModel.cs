using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class PaymentViewModel
    {
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountDue { get; set; }
    }
}
