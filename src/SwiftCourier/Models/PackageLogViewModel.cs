using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public class PackageLogViewModel
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LoggedAt { get; set; }
    }

    public class PackageLogDetailsViewModel
    {
        public int PackageId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LoggedAt { get; set; }
    }


    public class PackageLogListItemViewModel
    {
        public int PackageId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LoggedAt { get; set; }
    }
}
