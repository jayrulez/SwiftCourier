using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static PackageType ToEntity(this PackageTypeViewModel source)
        {
            var destination = new PackageType();

            destination.Name = source.Name;

            return destination;
        }

        public static PackageType UpdateEntity(this PackageTypeViewModel source, PackageType destination)
        {
            if(destination == null)
            {
                destination = new PackageType();
            }

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Name = source.Name;

            return destination;
        }

        public static PackageTypeViewModel ToViewModel(this PackageType source)
        {
            var destination = new PackageTypeViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static PackageTypeDetailsViewModel ToDetailsViewModel(this PackageType source)
        {
            var destination = new PackageTypeDetailsViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static PackageTypeListItemViewModel ToListItemViewModel(this PackageType source)
        {
            var destination = new PackageTypeListItemViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static List<PackageTypeListItemViewModel> ToListViewModel(this List<PackageType> source)
        {
            var destination = new List<PackageTypeListItemViewModel>();

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
