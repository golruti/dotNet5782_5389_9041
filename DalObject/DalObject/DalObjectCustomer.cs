using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a customer to the array of existing customers
        /// </summary>
        /// <param name="customer">struct of customer</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            if (!GetCustomer(customer.Id).Equals(default(Customer)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a customer - DAL");
            customer.IsDeleted = false;
            DataSource.customers.Add(customer);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int idCustomer)
        {
            var customer = DataSource.customers.FirstOrDefault(c => c.Id == idCustomer && !(c.IsDeleted));
            return customer;
        }

        /// <summary>
        /// The function accepts a condition in the predicate and returns the customer who satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            Customer customer = DataSource.customers.FirstOrDefault(customer => predicate(customer) && !(customer.IsDeleted));
            return customer;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers = new List<Customer>();
            customers = DataSource.customers;
            return customers;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of customers that maintain the predicate</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            IEnumerable<Customer> customers = new List<Customer>();
            customers = DataSource.customers.Where(customer => predicate(customer));
            return customers;
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific customer
        /// </summary>
        /// <param name="id">customer ID</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            Customer deletedCustomer = GetCustomer(id);
            if (deletedCustomer.Equals(default(Customer)))
                throw new KeyNotFoundException("Delete customer -DAL: There is no suitable customer in data");
            else
            {
                DataSource.customers.Remove(deletedCustomer);
                deletedCustomer.IsDeleted = true;
                DataSource.customers.Add(deletedCustomer);
            }
        }
    }

}
