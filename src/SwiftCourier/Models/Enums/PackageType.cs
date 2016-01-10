using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum PackageType
    {
        [Display(Name = "Envelope")]
        Envelope,

        [Display(Name = "Box")]
        Box,

        [Display(Name = "Bag")]
        Bag
    }
}
