using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
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
            IDAL.DO.Customer customer = new IDAL.DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            try
            {
                dal.InsertCustomer(customer);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested customer from the data and converts it to BL customer
        /// </summary>
        /// <param name="id">The requested customer</param>
        /// <returns>A Bl customer to print</returns>
        public Customer GetCustomer(int id)
        {
            try
            {
                return mapCustomer(dal.GetCustomer(id));
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// Convert a DAL customer to BL customer
        /// </summary>
        /// <param name="customer">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private Customer mapCustomer(IDAL.DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location(customer.Latitude, customer.Longitude),
                Phone = customer.Phone,
                //ReceivedParcels = findReceivedParcels(customer.Id),
                //ShippedParcels = findShippedParcels(customer.Id)
            };
        }

        //private List<CustomerDelivery> findReceivedParcels(int customerId)
        //{

        //}

        //private List<CustomerDelivery> findShippedParcels(int customerId)
        //{

        //}

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the customer list from DAL to the CustomerForList list
        /// </summary>
        /// <returns>The list of CustomerForList customers</returns>
        public IEnumerable<CustomerForList> GetCustomerForList()
        {
            List<CustomerForList> CustomerForList = new List<CustomerForList>();
            List<IDAL.DO.Parcel> parcels = (List<IDAL.DO.Parcel>)dal.GetParcels();
            foreach (var customer in dal.GetCustomers())
            {
                CustomerForList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default(DateTime))),
                    NumParcelWayToCustomer = dal.GetParcels()
                                        .Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime)) && !parcel.PickedUp.Equals(default(DateTime)))
                });
            }
            return CustomerForList;
        }



        public void UpdateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer tempCustomer = dal.GetCustomer(id);
            dal.DeleteCustomer(id);
            IDAL.DO.Customer customer = new IDAL.DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Latitude);
            dal.InsertCustomer(customer);
        }
    }
}
