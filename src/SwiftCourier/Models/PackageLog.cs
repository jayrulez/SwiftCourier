using System;
using System.Collections.Generic;

namespace SwiftCourier.Models
{
    public partial class PackageLog
    {
        public int Id { get; set; }
        public string LogMessage { get; set; }
        public DateTime DispatchedAt { get; set; }
        public DateTime? ReceivedAt { get; set; }
        public int PackageId { get; set; }
        public int DispatchedToUserId { get; set; }
        public int DispatchedByUserId { get; set; }
        public PackageLogStatus Status { get; set; }

        public virtual Package Package { get; set; }
        public virtual User DispatchedToUser { get; set; }
        public virtual User DispatchedByUser { get; set; }
    }
}
