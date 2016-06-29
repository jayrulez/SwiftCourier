using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Web.Data
{
    public partial class PackageLog
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string LogMessage { get; set; }
        public DateTime LoggedAt { get; set; }

        public virtual Package Package { get; set; }
    }
}
