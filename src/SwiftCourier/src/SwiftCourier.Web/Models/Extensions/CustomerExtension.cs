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
        public static CustomerViewModel ToViewModel(this Customer source)
        {
            var destination = new CustomerViewModel();

            destination.Id = source.Id;
            destination.Address = source.Address;
            destination.ContactNumber = source.ContactNumber;
            destination.EmailAddress = source.EmailAddress;
            destination.Name = source.Name;
            destination.TaxExempted = source.TaxExempted;

            return destination;
        }

        public static CustomerDetailsViewModel ToDetailsViewModel(this Customer source)
        {
            var destination = new CustomerDetailsViewModel();

            destination.Id = source.Id;
            destination.Address = source.Address;
            destination.ContactNumber = source.ContactNumber;
            destination.EmailAddress = source.EmailAddress;
            destination.Name = source.Name;
            destination.TaxExempted = source.TaxExempted ? "Yes" : "No";

            return destination;
        }

        public static CustomerListItemViewModel ToListItemViewModel(this Customer source)
        {
            var destination = new CustomerListItemViewModel();

            destination.Id = source.Id;
            destination.Address = source.Address;
            destination.ContactNumber = source.ContactNumber;
            destination.EmailAddress = source.EmailAddress;
            destination.Name = source.Name;
            destination.TaxExempted = source.TaxExempted ? "Yes" : "No";

            return destination;
        }

        public static List<CustomerListItemViewModel> ToListViewModel(this List<Customer> source)
        {
            var destination = new List<CustomerListItemViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }

        public static Customer ToEntity(this CustomerViewModel source)
        {
            var destination = new Customer();

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Address = source.Address;
            destination.ContactNumber = source.ContactNumber;
            destination.EmailAddress = source.EmailAddress;
            destination.Name = source.Name;
            destination.TaxExempted = source.TaxExempted;

            return destination;
        }

        public static Customer UpdateEntity(this CustomerViewModel source, Customer destination)
        {
            if(destination == null)
            {
                destination = new Customer();
            }

            if (source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Address = source.Address;
            destination.ContactNumber = source.ContactNumber;
            destination.EmailAddress = source.EmailAddress;
            destination.Name = source.Name;
            destination.TaxExempted = source.TaxExempted;

            return destination;
        }
    }
}
