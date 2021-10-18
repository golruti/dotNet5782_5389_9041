using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.initialize();
        }


        //stations
        public void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.indStation++] = station;
        }

 
       public void UpdateStation(Station station,int idxChangeStation)
        {
            DataSource.stations[idxChangeStation] = station;
        }

        //drone
        public void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.indDrone++] = drone;
        }

        public void UpdateDrone(Drone drone, int idxChangeDrone)
        {
            DataSource.drones[idxChangeDrone] = drone;
        }

        //customer
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.indCustomer++] = customer;
        }

        public void UpdateCustomer(Customer customer, int idxChangeCustomer)
        {
            DataSource.customers[idxChangeCustomer] = customer;
        }


        //parsel
        public void InsertParsel(Parsel parsel)
        {
            DataSource.parseles[DataSource.Config.indParsel++] = parsel;
        }

        public void UpdateParsel(Parsel parsel, int idxChangeParsel)
        {
            DataSource.parseles[idxChangeParsel] = parsel;
        }


    }
}
