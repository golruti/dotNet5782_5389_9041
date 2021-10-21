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
            DataSource.Initialize();
        }



        public static void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.IndStation++] = station;
        }

        //drone
        public static void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.IndDrone++] = drone;
        }

        public void UpdateDrone(Drone drone, int idxChangeDrone)
        {
            DataSource.drones[idxChangeDrone] = drone;
        }

        //customer
        public static void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.IndCustomer++] = customer;
        }



        //parsel
        public static void InsertParsel(Parsel parsel)
        {
            DataSource.parseles[DataSource.Config.IndParsel++] = parsel;
            DataSource.parseles[DataSource.Config.IndParsel].Id= DataSource.Config.IndParsel;
        }

        public void UpdateParselDelivered(int idxParsel,int droneId)
        {
            DataSource.parseles[idxParsel].Droneld= droneId;
        } 

        public static void UpdateParselPickedUp(int idxParsel)
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
