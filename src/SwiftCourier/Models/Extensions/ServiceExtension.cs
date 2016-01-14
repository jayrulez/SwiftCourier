using SwiftCourier.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftCourier.Models
{
    public static partial class Extensions
    {
        public static Service ToEntity(this ServiceViewModel source)
        {
            var destination = new Service();

            destination.Name = source.Name;
            destination.Cost = source.Cost;

            return destination;
        }

        public static Service UpdateEntity(this ServiceViewModel source, Service destination)
        {
            if(destination == null)
            {
                destination = new Service();
            }

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Name = source.Name;
            destination.Cost = source.Cost;

            return destination;
        }

        public static ServiceViewModel ToViewModel(this Service source)
        {
            var destination = new ServiceViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Cost = source.Cost;

            return destination;
        }

        public static ServiceDetailsViewModel ToDetailsViewModel(this Service source)
        {
            var destination = new ServiceDetailsViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Cost = source.Cost;

            return destination;
        }

        public static ServiceListItemViewModel ToListItemViewModel(this Service source)
        {
            var destination = new ServiceListItemViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Cost = source.Cost;

            return destination;
        }

        public static List<ServiceListItemViewModel> ToListViewModel(this List<Service> source)
        {
            var destination = new List<ServiceListItemViewModel>();

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
