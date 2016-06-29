using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public class DeliverViewModel
    {
        [Display(Name="Delivered By")]
        public int UserId { get; set; }
    }
}
