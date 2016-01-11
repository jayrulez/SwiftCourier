using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class CustomerViewModel
    {
        [Display(Name = "Customer Id")]
        public int Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Tax Exempted")]
        public bool TaxExempted { get; set; }
    }

    public class CustomerListItemViewModel
    {
        [Display(Name = "Customer Id")]
        public int Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Customer")]
        public string Name { get; set; }

        [Display(Name = "Tax Exempted")]
        public string TaxExempted { get; set; }
    }

    public class CustomerDetailsViewModel
    {
        [Display(Name = "Customer Id")]
        public int Id { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Customer")]
        public string Name { get; set; }

        [Display(Name = "Tax Exempted")]
        public string TaxExempted { get; set; }
    }
}
