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
                throw new Exception("Get customer -DAL-: There is no suitable customer in data");
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





        public void DeleteCustomer(int id)
        {
            List<Customer> tempCusromers = (List<Customer>)GetCustomers();
            tempCusromers.RemoveAll(item => item.Id == id);
        }

        //לשימוש הקונסטרקטוב בBL
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



    }
}
