using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
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
            IDAL.DO.Customer customer = new IDAL.DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            try
            {
                dal.InsertCustomer(customer);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
        private Customer mapCustomer(IDAL.DO.Customer customer)
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
        private ParcelToCustomer ParcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelToCustomer newParcel = new ParcelToCustomer
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Status = parcel.Scheduled == default ? default: parcel.PickedUp == default ? ParcelStatuses.Associated : parcel.Scheduled == default ? ParcelStatuses.Collected : ParcelStatuses.Provided
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
            List<IDAL.DO.Parcel> parcels = (List<IDAL.DO.Parcel>)dal.GetParcels();
            foreach (var customer in dal.GetCustomers())
            {
                CustomerForList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default(DateTime))),
                    NumParcelWayToCustomer = dal.GetParcels()
                                        .Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime)) && !parcel.PickedUp.Equals(default(DateTime)))
                });
            }
            return CustomerForList;
        }



        public void UpdateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer tempCustomer = dal.GetCustomer(id);
            dal.DeleteCustomer(id);
            IDAL.DO.Customer customer = new IDAL.DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Latitude);
            dal.InsertCustomer(customer);
        }


        private Customer findCustomer(int id)
        {
            foreach (IDAL.DO.Customer item in dal.GetCustomers())
            {
                if (item.Id == id)
                {
                    return new Customer(item.Id,item.Name,item.Phone,item.Longitude,item.Latitude);
                }
            }
            return null;
        }

        
    }
}
