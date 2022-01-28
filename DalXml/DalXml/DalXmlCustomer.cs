using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXML
    {
        /// <summary>
        /// CreateCustomer is a method in the DalXml class.
        /// the method adds a new customer
        /// </summary>
        /// <param name="customer">the first Customer value</param>
        public void AddCustomer(Customer customer)
        {
            customer.IsDeleted = false;
            List<Customer> customersXml;
            try
            {
                customersXml = XMLTools.LoadListFromXmlSerializer<Customer>(customersPath);
            }
            catch(XMLFileLoadCreateException e) { throw e; }
            foreach (var item in customersXml)
            {
                if(item.Id == customer.Id && item.IsDeleted == false)
                {
                    throw new Exception();
                }
            }
            customersXml.Add(customer);
            try
            {
                XMLTools.SaveListToXmlSerializer<Customer>(customersXml, customersPath);
            }
            catch(XMLFileLoadCreateException e) { throw e; }
        }

        public Customer GetCustomer(int requestedId)
        {
            return XMLTools.LoadListFromXmlSerializer<Customer>(customersPath).Find(customer => customer.Id == requestedId);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return XMLTools.LoadListFromXmlSerializer<Customer>(customersPath);
        }
        public Customer GetCustomer(Predicate<Customer> predicate)
        {
            Customer customer = XMLTools.LoadListFromXmlSerializer<Customer>(customersPath).FirstOrDefault(customer => predicate(customer) && !(customer.IsDeleted));
            return customer;
        }

        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate)
        {
            IEnumerable<Customer> customers = XMLTools.LoadListFromXmlSerializer<Customer>(customersPath).Where(customer => predicate(customer) && !(customer.IsDeleted));
            return customers;
            
        }

        public void DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}
