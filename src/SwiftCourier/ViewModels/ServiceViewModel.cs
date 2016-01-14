using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class ServiceViewModel
    {
        [Display(Name="Service")]
        public int Id { get; set; }

        [Display(Name = "Service")]
        public string Name { get; set; }

        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
    }

    public class ServiceDetailsViewModel
    {
        [Display(Name = "Service")]
        public int Id { get; set; }

        [Display(Name = "Service")]
        public string Name { get; set; }

        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
    }

    public class ServiceListItemViewModel
    {
        [Display(Name = "Service")]
        public int Id { get; set; }

        [Display(Name = "Service")]
        public string Name { get; set; }

        [Display(Name = "Cost")]
        public decimal Cost { get; set; }
    }
}
