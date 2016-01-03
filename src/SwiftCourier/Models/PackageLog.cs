using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class PackageLog
    {
        public int Id { get; set; }
        public DateTime LoggedAt { get; set; }
        public LogMode Mode { get; set; }
        public int PackageId { get; set; }
        public int UserId { get; set; }

        public virtual Package Package { get; set; }
        public virtual User User { get; set; }
    }
}
