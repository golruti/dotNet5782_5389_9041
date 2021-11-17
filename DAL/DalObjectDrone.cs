using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {

        //------------------------------------------Add------------------------------------------
        /// <summary>
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void InsertDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
        }


        //------------------------------------------Display------------------------------------------
        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int idDrone)
        {
            return DataSource.drones.First(drone => drone.Id == idDrone);
        }




        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            return DataSource.drones.Select(drone => drone.Clone()).ToList();
        }

        //--------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------

        
        public void UpdateDroneStatus(int IdParcel)
        {
            //עדכון מצב הרחפן למשלוח ..צריך להעביר לבל 
            for (int j = 0; j < DataSource.drones.Count(); ++j)
            {
                if (DataSource.drones[j].Id == IdParcel)
                {
                    Drone d = DataSource.drones[j];
                    //d.Status = IDAL.DO.Enum.DroneStatuses.Delivery;
                    DataSource.drones[j] = d;
                }
            }
        }


        //שיוך חבילה לרחפן
        /// Assigning a parcel to a drone
        /// </summary>
        /// //לבדוק אם גם המשקל נכון והרחפן יכול לקחת אותה 
        /// <param name = "idxParcel" > Id of the parcel</param>
        public void UpdateParcelScheduled(int idxParcel)
        {
            //for (int i = 0; i < DataSource.drones.Count(); ++i)
            //{
            //    if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
            //    {
            //        DataSource.parcels[idxParcel].Scheduled = new DateTime();
            //        DataSource.parcels[idxParcel].Droneld = DataSource.drones[i].Id;
            //        DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
            //        DataSource.drones[i].MaxWeight = DataSource.parcels[idxParcel].Weight;
            //        break;
            //    }
            //}
        }

        public void DeleteDrone(int id)
        {
            List<Drone> tempDrones = (List<Drone>)GetDrones();
            tempDrones.RemoveAll(item => item.Id == id);
            DataSource.drones = tempDrones;
        }

    }
}
