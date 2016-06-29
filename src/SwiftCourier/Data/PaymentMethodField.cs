using SwiftCourier.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Data
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
        public FieldType Type { get; set; }

        public virtual ICollection<PaymentMethodFieldValue> PaymentMethodFieldValues { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
