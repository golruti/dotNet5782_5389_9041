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


        //--------------------------------------------הצגת רשימת לקוחות לרשימה--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

        public void AddCustomer(Customer tempCustomer)
        {
            
            IDAL.DO.Customer customer = new IDAL.DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            dal.InsertCustomer(customer);
        }

        public void UpdateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer tempCustomer = dal.GetCustomer(id);
            dal.DeleteCustomer(id);
            IDAL.DO.Customer customer = new IDAL.DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Lattitude);
            dal.InsertCustomer(customer);
        }

    }
}
