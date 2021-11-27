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
            if (!(uniqueIDTaxCheck(DataSource.drones, drone.Id)))
            {
                throw new Exception("Adding a drone - DAL");
            }
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
            Drone drone = DataSource.drones.First(drone => drone.Id == idDrone);
            if (drone.GetType().Equals(default))
                throw new Exception("Get drone -DAL-: There is no suitable customer in data");
            return drone;
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



        /// <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        /// 
        //	שליחת רחפן לטעינה בתחנת-בסיס
        public void TryAddDroneCarge(int droneId)
        {

            //לבדוק אם באמת קיים רחפן עם איידי כזה
            //ואם לא צריך לשלוח חריגה
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new Exception();

            //	יש לוודא שתחנת הבסיס פנויה לקבל את הרחפן לטעינה
            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default(BaseStation)))
                throw new Exception();

            // הוספת רשומה של ישות טעינת סוללת רחפן
            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            DataSource.droneCharges.Add(droneCharge);
        }





        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        /// //שחרור רחפן מטעינה בתחנת-בסיס
        public void TryRemoveDroneCarge(int droneId)
        {
            //אם לא מצא את הרחפן שרצו לשחרר
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new Exception();

            DataSource.droneCharges.Remove(GetDroneCharge(droneId));
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
        }

    }
}
