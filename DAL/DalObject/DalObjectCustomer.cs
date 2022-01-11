using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AddCustomer(Customer customer)
        {
            if (!GetCustomer(customer.Id).GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a customer - DAL");
            customer.IsDeleted = false;
            DataSource.customers.Add(customer.Id, customer);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int idCustomer)
        {
            var customer = DataSource.customers.Values.FirstOrDefault(c => c.Id == idCustomer && !(c.IsDeleted));
            if (customer.GetType().Equals(default))
                throw new KeyNotFoundException("Get customer -DAL-: There is no suitable customer in data");
            return DataSource.customers[idCustomer];
        }

        /// <summary>
        /// The function accepts a condition in the predicate and returns the customer who satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            Customer customer = DataSource.customers.Values.FirstOrDefault(customer => predicate(customer) && !(customer.IsDeleted));
            if (customer.GetType().Equals(default))
                throw new KeyNotFoundException("Get customer -DAL-: There is no suitable customer in data");
            return customer;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of customer</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return DataSource.customers.Values.Where(customer => !(customer.IsDeleted)).ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of customers that maintain the predicate</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            return DataSource.customers.Values.Where
                (customer => predicate(customer) && !(customer.IsDeleted)).ToList();
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific customer
        /// </summary>
        /// <param name="id">customer ID</param>
        public void DeleteCustomer(int id)
        {
            if (GetCustomer(id).GetType().Equals(default))
                throw new KeyNotFoundException("Delete customer -DAL-: There is no suitable customer in data");

            var deleteedCustomer = GetCustomer(id);
            DataSource.customers.Remove(deleteedCustomer.Id);
            deleteedCustomer.IsDeleted = true;
            DataSource.customers.Add(deleteedCustomer.Id,deleteedCustomer);
        }
    }
}
