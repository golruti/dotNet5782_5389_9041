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



        //drone
        public void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.indDrone++] = drone;
        }

  

        //customer
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.indCustomer++] = customer;
        }

       


        //parsel
        public void InsertParsel(Parsel parsel)
        {
            DataSource.parseles[DataSource.Config.indParsel++] = parsel;
        }

        public void UpdateParselDelivered(int idxParsel,int droneId)
        {
            DataSource.parseles[idxParsel].Droneld= droneId;
        }

        public void UpdateParselPickedUp(int idxParsel)
        {
            
            DataSource.parseles[idxParsel].PickedUp = DateTime.Now;
        }

        public void UpdateParselDelivered(int idxParsel)
        {

            DataSource.parseles[idxParsel].Delivered = DateTime.Now;
        }

        //drone charge
        public void UpdatedroneCarge(int idxStation,int idxDrone)
        {
           

            DroneCarge droneCarge = new DroneCarge();
            droneCarge.StationId = idxStation;
            droneCarge.DroneId = idxDrone;
        }
    }
}
