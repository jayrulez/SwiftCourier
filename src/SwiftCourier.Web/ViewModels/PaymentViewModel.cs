using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.ViewModels
{
    public class PaymentViewModel
    {
        [Display(Name = "Payment Method")]
        public int PaymentMethodId { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Amount Due")]
        public decimal AmountDue { get; set; }
    }

    public class PaymentDetailsViewModel
    {
        [Display(Name = "Payment Id")]
        public int Id { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal Amount { get; set; }

        [Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

        [Display(Name = "Payment Id")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Processed At")]
        public DateTime ProcessedAt { get; set; }

        [Display(Name = "Processed By")]
        public string ProcessedBy { get; set; }
    }

    public class PaymentListItemViewModel
    {
        [Display(Name = "Payment Id")]
        public int Id { get; set; }

        [Display(Name = "Amount Paid")]
        public decimal Amount { get; set; }

        [Display(Name = "Invoice Id")]
        public int InvoiceId { get; set; }

        [Display(Name = "Payment Id")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Processed At")]
        public DateTime ProcessedAt { get; set; }

        [Display(Name = "Processed By")]
        public string ProcessedBy { get; set; }
    }
}
