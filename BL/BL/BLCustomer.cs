﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL
    {

        //--------------------------------------------הוספת תחנת בסיס-------------------------------------------------------------------------------------------
        public void AddCustomer(Customer tempCustomer)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            dal.InsertCustomer(customer);
        }

        //---------------------------------------------הצגת לקוח לפי ID ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Customer GetCustomer(int id)
        {
            return mapCustomer(dal.GetCustomer(id));
        }


        private Customer mapCustomer(IDAL.DO.Customer customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Location = new Location(customer.Latitude, customer.Longitude),
                Phone = customer.Phone,
                ReceivedParcels = findReceivedParcels(customer.Id),
                ShippedParcels = findShippedParcels(customer.Id)
            };
        }

        private List<CustomerDelivery> findReceivedParcels(int customerId)
        {

        }

        private List<CustomerDelivery> findShippedParcels(int customerId)
        {

        }

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



        public void UpdateCustomer(int id, string name, string phone)
        {
            IDAL.DO.Customer tempCustomer = dal.GetCustomer(id);
            dal.DeleteCustomer(id);
            IDAL.DO.Customer customer = new IDAL.DO.Customer(id, name, phone, tempCustomer.Longitude, tempCustomer.Latitude);
            dal.InsertCustomer(customer);
        }




        //-------------------------לשימוש הקונסטרקטור
        private IDAL.DO.Customer FindSenderCustomerByDroneId(int DroneId)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            foreach (var parcel in dal.GetParcels())
            {
                if (parcel.Droneld == DroneId)
                {
                    customer = dal.GetById<IDAL.DO.Customer>(dal.GetCustomers(), parcel.SenderId);
                }
            }

            if (customer.Equals(default(IDAL.DO.Customer)))
            {
                throw new Exception();
            }
            return customer;
        }
    }
}