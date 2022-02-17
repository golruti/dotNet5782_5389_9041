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
        //--------------------------------------------Adding----------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            if (!GetCustomer(customer.Id).Equals(default(Customer)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a customer - DAL");
            customer.IsDeleted = false;
            DataSource.customers.Add(customer);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int idCustomer)
        {
            var customer = DataSource.customers.FirstOrDefault(c => c.Id == idCustomer && !(c.IsDeleted));
            return customer;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            Customer customer = DataSource.customers.FirstOrDefault(customer => predicate(customer) && !(customer.IsDeleted));
            return customer;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers = new List<Customer>();
            customers = DataSource.customers;
            return customers;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            IEnumerable<Customer> customers = new List<Customer>();
            customers = DataSource.customers.Where(customer => predicate(customer));
            return customers;
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
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
