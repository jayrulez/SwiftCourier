using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Data
{
    public partial class PaymentMethodFieldValue
    {
        public int PaymentId { get; set; }
        public int PaymentMethodFieldId { get; set; }
        public string Value { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual PaymentMethodField PaymentMethodField { get; set; }
    }
}
