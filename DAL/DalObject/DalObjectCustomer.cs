using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {

        //------------------------------------------Add------------------------------------------

        /// <summary>
        /// Add a customer to the array of existing customers
        /// </summary>
        /// <param name="customer">struct of customer</param>
        public void InsertCustomer(Customer customer)
        {
            if (! (uniqueIDTaxCheck(DataSource.customers, customer.Id)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a customer - DAL");
            }
            DataSource.customers.Add(customer);
        }


        //------------------------------------------Display------------------------------------------

        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int idCustomer)
        {
            Customer customer = DataSource.customers.First(customer => customer.Id == idCustomer);
            if (customer.GetType().Equals(default))
                throw new KeyNotFoundException("Get customer -DAL-: There is no suitable customer in data");
            return customer;
        }

        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of customer</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return DataSource.customers.Select(customer => customer.Clone()).ToList();
        }


        /// <summary>
        /// The function deletes a specific customer
        /// </summary>
        /// <param name="id">customer ID</param>
        public void DeleteCustomer(int id)
        {
            var customer = DataSource.customers.FirstOrDefault(c => c.Id == id);
            if (customer.Equals(default(Drone)))
                throw new Exception("Delete customer -DAL-: There is no suitable customer in data");
            DataSource.customers.Remove(customer);
        }

        /// <summary>
        /// The function returns the list of customers who have been provided with parcels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomersProvided()
        {
            List<Customer> customerProvided = new List<Customer>();
            foreach (var customer in GetCustomers())
            {
                foreach (var parcel in GetParcelsProvided())
                {
                    if (customer.Id ==parcel.TargetId)
                    {
                        customerProvided.Add(customer);
                    }
                }         
            }
            return customerProvided;
        }

        /// <summary>
        /// The function finds the customer to whom the parcel in the drone should arrive
        /// </summary>
        /// <param name="ParcelDeliveredId"> customer ID</param>
        /// <returns>the customer to whom the parcel</returns>
        public Customer customerByDrone(int ParcelDeliveredId)
        {
            var customer = DataSource.customers.FirstOrDefault(customer => customer.Id == ParcelDeliveredId);
            if (customer.Equals(default(Drone)))
                throw new Exception("Get customer -DAL-: There is no suitable customer in data");

            return customer;
        }

        /// <summary>
        /// The function finds the customer from whom the package that is in the drone came
        /// </summary>
        /// <param name="DroneId">drone ID</param>
        /// <returns>customer from whom the package </returns>
        public Customer FindSenderCustomerByDroneId(int DroneId)
        {
            Customer customer = new IDAL.DO.Customer();
            foreach (var parcel in GetParcels())
            {
                if (parcel.Droneld == DroneId)
                {
                    customer = GetCustomer(parcel.SenderId);
                }
            }
            if (customer.Equals(default(Customer)))
                throw new KeyNotFoundException("Get customer -DAL-: There is no suitable customer in data");
            return customer;
        }
    }
}
