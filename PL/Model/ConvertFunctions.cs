using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ConvertFunctions
    {


        public static Enums.WeightCategories BOEnumWeightCategoriesToPO(BO.Enums.WeightCategories weight)
        {
            return (Enums.WeightCategories)weight;
        }

        public static Enums.Priorities BOEnumPrioritiesToPO(BO.Enums.Priorities prioruty)
        {
            return (Enums.Priorities)prioruty;
        }

        public static Enums.ParcelStatuses BOEnumParcelStatusToPO(BO.Enums.ParcelStatuses parcelStatuses)
        {
            return (Enums.ParcelStatuses)parcelStatuses;
        }

        public static Location BOLocationToPO(BO.Location location)
        {
            return new Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        public static DroneInParcel BODroneParceToPO(BO.DroneInParcel droneParcel)
        {
            return new DroneInParcel
            {
                Id = droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = BOLocationToPO(droneParcel.Location)
            };
        }
        public static CustomerDelivery BOCustomerDeliveryToPO(BO.CustomerDelivery customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new CustomerDelivery
            {
                Id = customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }
        public static Parcel BOParcelToPO(BO.Parcel parcel)
        {
            return new Parcel
            {
                Id = parcel.Id,
                CustomerSender = BOCustomerDeliveryToPO(parcel.CustomerSender),
                CustomerReceives = BOCustomerDeliveryToPO(parcel.CustomerReceives),
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                DroneParcel = BODroneParceToPO(parcel.DroneParcel),
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered
            };
        }

        public static IEnumerable<ParcelToCustomer> BOParcelToCustomerToPO(IEnumerable<BO.ParcelToCustomer> parcelsToCustomer)
        {
            if (parcelsToCustomer == null)
                return Enumerable.Empty<ParcelToCustomer>();
            return parcelsToCustomer.Select(parcel => new ParcelToCustomer()
            {
                Id = parcel.Id,
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                Status = BOEnumParcelStatusToPO(parcel.Status),
                Customer = BOCustomerDeliveryToPO(parcel.Customer)
            });
        }
        public static Customer BOCustomerToPO(BO.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = BOLocationToPO(customer.Location),
                FromCustomer = BOParcelToCustomerToPO(customer.FromCustomer),
                ToCustomer = BOParcelToCustomerToPO(customer.ToCustomer)
            };
        }


        public static ObservableCollection<CustomerForList> BOCustomerForListToPO(IEnumerable<BO.CustomerForList> customersForList)
        {
            ObservableCollection<PO.CustomerForList> c = new ObservableCollection<CustomerForList>();

            foreach (var customer in customersForList)
            {
                c.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = customer.NumParcelSentDelivered,
                    NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                    NumParcelReceived = customer.NumParcelReceived,
                    NumParcelWayToCustomer = customer.NumParcelWayToCustomer
                });
            }

            //if (customersForList == null)
            //    return ObservableCollection.Empty<CustomerForList>();
            return c;
        }

        public static BO.CustomerForList POCustomerForListToBO(CustomerForList customer)
        {
            return new BO.CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                NumParcelSentDelivered = customer.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                NumParcelReceived = customer.NumParcelReceived,
                NumParcelWayToCustomer = customer.NumParcelWayToCustomer
            };
        }
    }
}
