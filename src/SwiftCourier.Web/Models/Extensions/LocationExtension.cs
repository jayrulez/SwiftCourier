using SwiftCourier.Web.ViewModels;
using SwiftCourier.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models.Extensions
{
    public static partial class Extensions
    {
        public static Location ToEntity(this LocationViewModel source)
        {
            var destination = new Location();

            destination.Name = source.Name;

            return destination;
        }

        public static Location UpdateEntity(this LocationViewModel source, Location destination)
        {
            if(destination == null)
            {
                destination = new Location();
            }

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Name = source.Name;

            return destination;
        }

        public static LocationViewModel ToViewModel(this Location source)
        {
            var destination = new LocationViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static LocationDetailsViewModel ToDetailsViewModel(this Location source)
        {
            var destination = new LocationDetailsViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static LocationListItemViewModel ToListItemViewModel(this Location source)
        {
            var destination = new LocationListItemViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static List<LocationListItemViewModel> ToListViewModel(this List<Location> source)
        {
            var destination = new List<LocationListItemViewModel>();

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
