using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Data
{
    public partial class Payment
    {
        public Payment()
        {
            PaymentMethodFieldValues = new HashSet<PaymentMethodFieldValue>();
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentMethodId { get; set; }
        public DateTime ProcessedAt { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<PaymentMethodFieldValue> PaymentMethodFieldValues { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual User User { get; set; }
    }
}
