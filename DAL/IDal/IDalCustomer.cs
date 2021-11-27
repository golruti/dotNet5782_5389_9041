using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial interface IDal
    {
        public void InsertCustomer(Customer customer);
        public Customer GetCustomer(int idCustomer);
        public IEnumerable<Customer> GetCustomers();
        public void DeleteCustomer(int id);
        public IEnumerable<Customer> GetCustomersProvided();
        public Customer customerByDrone(int ParcelDeliveredId);
    }
}
