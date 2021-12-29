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
            DeleteBLCustomer(id);
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
            DO.Customer customer = new DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Latitude);
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

        public CustomerForList GetCustomerForList(string name)
        {
            return GetCustomerForList().FirstOrDefault(customer => customer.Name==name);
        }

        //--------------------------------------------Initialize the parcel list--------------------------------------------------------
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
