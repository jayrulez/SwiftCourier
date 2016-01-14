using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public class PackageType
    {
        public PackageType()
        {
            Packages = new HashSet<Package>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Package> Packages { get; set; }
    }
}
