using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DAL
{
    internal partial class DalXml
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

            AddItem(customersPath, customer);
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
            return GetItem<Customer>(customersPath, idCustomer);
        }

        /// <summary>
        /// The function accepts a condition in the predicate and returns the customer who satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            return GetCustomers().FirstOrDefault(item => predicate(item));
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of customer</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            return GetList<Customer>(customersPath);
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of customers that maintain the predicate</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            return GetCustomers().Where(item => predicate(item));
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific customer
        /// </summary>
        /// <param name="id">customer ID</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            if (GetCustomer(id).Equals(default(Customer)))
                throw new KeyNotFoundException("Delete customer -DAL: There is no suitable customer in data");

            UpdateItem(customersPath, id, nameof(Customer.IsDeleted), true);
        }
    }
}
