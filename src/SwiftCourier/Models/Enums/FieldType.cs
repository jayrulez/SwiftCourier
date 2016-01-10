using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public enum FieldType
    {
        [Display(Name = "Number")]
        Number,

        [Display(Name = "Text")]
        Text
    }
}
