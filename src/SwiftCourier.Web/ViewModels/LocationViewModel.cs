using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.ViewModels
{
    public class LocationViewModel
    {
        [Display(Name="Location")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string Name { get; set; }
    }

    public class LocationDetailsViewModel
    {
        [Display(Name = "Location")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string Name { get; set; }
    }

    public class LocationListItemViewModel
    {
        [Display(Name = "Location")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string Name { get; set; }
    }
}
