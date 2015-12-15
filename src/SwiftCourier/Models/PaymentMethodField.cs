using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class PaymentMethodField
    {
        public PaymentMethodField()
        {
            PaymentMethodFieldValues = new HashSet<PaymentMethodFieldValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int PaymentMethodId { get; set; }
        public int Type { get; set; }

        public virtual ICollection<PaymentMethodFieldValue> PaymentMethodFieldValues { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
