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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer customer)
        {
            if (!GetCustomer(customer.Id).Equals(default(Customer)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a customer - DAL");
            customer.IsDeleted = false;

            AddItem(customersPath, customer);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int idCustomer)
        {
            return GetItem<Customer>(customersPath, idCustomer);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            return GetCustomers().FirstOrDefault(item => predicate(item));
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers()
        {
            return GetList<Customer>(customersPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            return GetCustomers().Where(item => predicate(item));
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(int id)
        {
            if (GetCustomer(id).Equals(default(Customer)))
                throw new KeyNotFoundException("Delete customer -DAL: There is no suitable customer in data");

            UpdateItem(customersPath, id, nameof(Customer.IsDeleted), true);
        }
    }
}
