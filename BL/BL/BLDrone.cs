using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL
    {
        //פונקציה לשימוש הקונסטרקטור
        //בדיקה אם הרחפן מבצע משלוח
        private bool IsDroneMakesDelivery(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                ParcelForList newParcel = new ParcelForList();
                //חבילה שלא סופקה והרחפן שויך
                if (parcel.Droneld == droneId &&
                    !(parcel.Requested.Equals(default(DateTime))) && parcel.Delivered.Equals(default(DateTime)))
                {
                    return true;
                }
            }
            return false;
        }


        //--------------------------------------------הצגת רשימת רחפנים לרשימה---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<DroneForList> GetDroneForList()
        {
            return drones;
        }

        public void AddDrone(int id, string model, int maxWeight, double longitude, double latitude)
        {
            Drone tempDrone = new Drone(id, model, (Enums.WeightCategories)maxWeight, DroneStatuses.Maintenance, rand.Next(20, 41), longitude, latitude);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDrone.Id, tempDrone.Model, (IDAL.DO.Enum.WeightCategories)tempDrone.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void UpdateDrone(int id, string model)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (IDAL.DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.InsertDrone(drone);
        }
        public void UpdateDroneLocation(int id,)
        {

        }
    }
}
