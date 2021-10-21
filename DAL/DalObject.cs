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

        //insert
        public static void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.IndStation++] = station;
        }

        public static void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.IndDrone++] = drone;
        }
        public static void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.IndCustomer++] = customer;
        }

        public static void InsertParsel(Parsel parsel)
        {
            DataSource.parseles[DataSource.Config.IndParsel++] = parsel;
            DataSource.parseles[DataSource.Config.IndParsel].Id = DataSource.Config.IndParsel;
        }





        //שיוך חבילה לרחפן
        public static void UpdateParseךScheduled(int idxParsel)
        {
            for (int i = 0; i < DataSource.Config.IndDrone; ++i)
            {
                if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
                {
                    DataSource.parseles[idxParsel].Scheduled = new DateTime();
                    DataSource.parseles[idxParsel].Droneld = DataSource.drones[i].Id;
                    DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
                    DataSource.drones[i].MaxWeight = DataSource.parseles[idxParsel].Weight;
                    break;
                }
            }
        }
        //אסיפת חבילה עי רחפן
        public static void UpdateParselPickedUp(int idxParsel)
        {
            DataSource.parseles[idxParsel].PickedUp = DateTime.Now;
            DataSource.drones[DataSource.parseles[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Delivery;
        }


        //אספקת חבילה ליעד
        public static void UpdateParselDelivered(int idxParsel)
        {
            DataSource.parseles[idxParsel].Delivered = DateTime.Now;
            DataSource.drones[DataSource.parseles[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Available;
        }




        // לבדוק!!! הפונקציות לא עובדות

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
