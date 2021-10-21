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
            int u = 12;
        }


        //stations
        public static void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.indStation++] = station;
        }

 
       public void UpdateStation(Station station,int idxChangeStation)
        {
            DataSource.stations[idxChangeStation] = station;
        }

        public static void ToStringStation(int stationIdx)
        {
            Console.WriteLine($"Id:{0}./n Name:{1}", Station.Id, Station.Name) ;
        }

        public static void ToStringStations()
        {

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
            DataSource.parseles[DataSource.Config.indParsel].Id= DataSource.Config.indParsel;
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
        public bool UpdatedroneCarge(int idxStation,int idxDrone)
        {
            DroneCharge droneCharge = new DroneCharge();
            foreach(DroneCharge item in DataSource.droneCharges)
            {
                if(item.StationId== idxStation)
                {
                    return false;
                }
            }
            
                droneCharge.StationId = idxStation;
                droneCharge.DroneId = idxDrone;
                return true;

        }

        public void UpdatedroneCarge(int idxStation)
        {
            foreach (DroneCharge item in DataSource.droneCharges)
            {
                if (item.StationId == idxStation)
                {
                    DataSource.droneCharges.Remove(item);
                }
            }
        }


    }
}
