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
            try
            {
                dal.AddCustomer(new DO.Customer()
                {
                    Id = tempCustomer.Id,
                    Name = tempCustomer.Name,
                    Phone = tempCustomer.Phone,
                    Longitude = Math.Round(tempCustomer.Location.Longitude),
                    Latitude = Math.Round(tempCustomer.Location.Latitude),
                    IsDeleted = false
                });
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //---------------------------------------------Delete ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function gets a customer ID and tries to delete it
        /// </summary>
        /// <param name="station"></param>
        public void DeleteBLCustomer(int customerId)
        {
            try
            {
                dal.DeleteCustomer(customerId);

            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete customer -BL-" + ex.Message);
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
            DO.Customer tempCustomer;

            try
            {
                tempCustomer = dal.GetCustomer(id);
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException("Get customer by id -BL-" + ex.Message);
            }

            if (name == "-1")
                name = tempCustomer.Name;
            if (phone == "-1")
                phone = tempCustomer.Phone;
            DeleteBLCustomer(id);

            DO.Customer customer = new DO.Customer()
            {
                Id = id,
                Name = name,
                Phone = phone,
                Longitude = tempCustomer.Longitude,
                Latitude = tempCustomer.Latitude,
                IsDeleted = false
            };
            try
            {
                dal.AddCustomer(customer);

            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
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
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get customer by id -BL-" + ex.Message);
            }
        }

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the customer list from DAL to the CustomerForList list
        /// </summary>
        /// <returns>The list of CustomerForList customers</returns>
        public IEnumerable<CustomerForList> GetCustomerForList()
        {
            List<CustomerForList> CustomerForList = new List<CustomerForList>();
            IEnumerable<DO.Parcel> parcels = dal.GetParcels();
            foreach (var customer in dal.GetCustomers())
            {
                CustomerForList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(null)),
                    NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && !(parcel.Delivered.Equals(null))),
                    NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(null)),
                    NumParcelWayToCustomer = dal.GetParcels()
                                        .Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(null) && !parcel.PickedUp.Equals(null))
                });
            }
            if (CustomerForList.Count() == 0)
                return Enumerable.Empty<CustomerForList>();
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

        public CustomerForList GetCustomerForList(string name)
        {
            return GetCustomerForList().FirstOrDefault(customer => customer.Name == name);
        }

        //--------------------------------------------Initialize the parcel list--------------------------------------------------------
        /// <summary>
        /// Convert a DAL customer to BL customer
        /// </summary>
        /// <param name="customer">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private Customer mapCustomer(DO.Customer customer)
        {
            IEnumerable<ParcelToCustomer> sendedList = new List<ParcelToCustomer>();
            IEnumerable<ParcelToCustomer> targetedList = new List<ParcelToCustomer>();
            sendedList = dal.GetParcels().Where(p => p.SenderId == customer.Id).Select(p => mapParcelToParcelToCustomer(p));
            targetedList = dal.GetParcels().Where(p => p.TargetId == customer.Id).Select(p => mapParcelToParcelToCustomer(p));

            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location() { Latitude = Math.Round(customer.Latitude), Longitude = Math.Round(customer.Longitude) },
                Phone = customer.Phone,
                FromCustomer = sendedList,
                ToCustomer = targetedList,
            };
        }


        /// <summary>
        /// Convert a DAL ParcelToCustomer to BL ParcelToCustomer
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The converted ParcelToCustomer</returns>
        private ParcelToCustomer mapParcelToParcelToCustomer(DO.Parcel parcel)
        {
            ParcelToCustomer newParcel = new ParcelToCustomer();
            newParcel.Id = parcel.Id;
            newParcel.Weight = (WeightCategories)parcel.Weight;
            newParcel.Priority = (Priorities)parcel.Priority;
            newParcel.Status = getParcelStatus(parcel);

            CustomerDelivery target = mapCustomerInParcel(dal.GetCustomer(parcel.TargetId));
            CustomerDelivery sender = mapCustomerInParcel(dal.GetCustomer(parcel.SenderId)); ;

            if (target == null || sender == null)
                throw new KeyNotFoundException("parcel to customer, not found customer customer -BL-");

            if (parcel.Delivered == null)
            {
                newParcel.Customer = target;
            }
            else
            {
                newParcel.Customer = sender;
            }
            return newParcel;
        }

        /// <summary>
        /// Convert a DAL customer to BL Customer In Parcel
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerDelivery mapCustomerInParcel(DO.Customer customer)
        {
            return new CustomerDelivery()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }
    }
}
