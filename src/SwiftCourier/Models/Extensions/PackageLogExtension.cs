using SwiftCourier.Models;
using SwiftCourier.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models.Extensions
{
    public static partial class Extensions
    {
        public static PackageLogDetailsViewModel ToDetailsViewModel(this PackageLog source)
        {
            var destination = new PackageLogDetailsViewModel();

            destination.PackageId = source.PackageId;
            destination.LogMessage = source.LogMessage;
            destination.LoggedAt = source.LoggedAt;

            return destination;
        }

        public static PackageLogListItemViewModel ToListItemViewModel(this PackageLog source)
        {
            var destination = new PackageLogListItemViewModel();

            destination.PackageId = source.PackageId;
            destination.LogMessage = source.LogMessage;
            destination.LoggedAt = source.LoggedAt;

            return destination;
        }

        public static List<PackageLogListItemViewModel> ToListViewModel(this List<PackageLog> source)
        {
            var destination = new List<PackageLogListItemViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }
    }
}
