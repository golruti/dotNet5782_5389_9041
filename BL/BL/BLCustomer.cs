using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using static BO.Enums;

namespace BL
{
    partial class BL
    {

        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a customer to the list of customers
        /// </summary>
        /// <param name="tempCustomer">The customer for Adding</param>
        public void AddCustomer(Customer tempCustomer)
        {
            DO.Customer customer = new DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            try
            {
                dal.AddCustomer(customer);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //--------------------------------------------Update----------------------------------------------------------------------------------------
        /// <summary>
        /// update customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        public void UpdateCustomer(int id, string name = "-1", string phone = "-1")
        {
            DO.Customer tempCustomer = dal.GetCustomer(id);
            dal.DeleteCustomer(id);
            if (name == "-1")
            {
                name = tempCustomer.Name;
            }
            if (phone == "-1")
            {
                phone = tempCustomer.Phone;
            }
           DO.Customer customer = new DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Latitude);
            dal.AddCustomer(customer);
        }

        //---------------------------------------------Show item---------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested customer from the data and converts it to BL customer
        /// </summary>
        /// <param name="id">The requested customer</param>
        /// <returns>A Bl customer to print</returns>
        public Customer GetBLCustomer(int id)
        {
            try
            {
                return mapCustomer(dal.GetCustomer(id));
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// Convert a DAL customer to BL customer
        /// </summary>
        /// <param name="customer">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private Customer mapCustomer(DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location(customer.Latitude, customer.Longitude),
                Phone = customer.Phone,
            };
        }

        /// <summary>
        /// Convert a BL parcel to Parcel At Customer
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <param name="type">The type of the customer</param>
        /// <returns>The converted parcel</returns>
        private ParcelToCustomer parcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelToCustomer newParcel = new ParcelToCustomer
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Status = parcel.Scheduled == default ? default : parcel.PickedUp == default ? ParcelStatuses.Associated : parcel.Scheduled == default ?
                                                                                                                 ParcelStatuses.Collected : ParcelStatuses.Provided
            };

            if (type == "sender")
            {
                newParcel.Customer = new CustomerDelivery()
                {
                    Id = parcel.CustomerReceives.Id,
                    Name = parcel.CustomerReceives.Name
                };
            }
            else
            {
                newParcel.Customer = new CustomerDelivery()
                {
                    Id = parcel.CustomerSender.Id,
                    Name = parcel.CustomerSender.Name
                };
            }
            return newParcel;
        }

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the customer list from DAL to the CustomerForList list
        /// </summary>
        /// <returns>The list of CustomerForList customers</returns>
        public IEnumerable<CustomerForList> GetCustomerForList()
        {
            List<CustomerForList> CustomerForList = new List<CustomerForList>();
            List<DO.Parcel> parcels = (List<DO.Parcel>)dal.GetParcels();
            foreach (var customer in dal.GetCustomers())
            {
                CustomerForList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(null)),
                    NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(null)),
                    NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(null)),
                    NumParcelWayToCustomer = dal.GetParcels()
                                        .Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(null) && !parcel.PickedUp.Equals(null))
                });
            }
            return CustomerForList;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of CustomerForList that maintain the predicate</returns>
        public IEnumerable<CustomerForList> GetCustomerForList(Predicate<CustomerForList> predicate)
        {
            return GetCustomerForList().Where(customer => predicate(customer));
        }
    }
}
