using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.ViewModels
{
    public class DispatchViewModel
    {
        [Display(Name= "Dispatch To")]
        public int UserId { get; set; }
    }
}
