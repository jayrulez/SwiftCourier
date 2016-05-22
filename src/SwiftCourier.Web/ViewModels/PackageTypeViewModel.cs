using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.ViewModels
{
    public class PackageTypeViewModel
    {
        [Display(Name="Package Type")]
        public int Id { get; set; }

        [Display(Name = "Package Type")]
        public string Name { get; set; }
    }

    public class PackageTypeDetailsViewModel
    {
        [Display(Name = "Package Type")]
        public int Id { get; set; }

        [Display(Name = "Package Type")]
        public string Name { get; set; }
    }

    public class PackageTypeListItemViewModel
    {
        [Display(Name = "Package Type")]
        public int Id { get; set; }

        [Display(Name = "Package Type")]
        public string Name { get; set; }
    }
}
