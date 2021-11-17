using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }


        bool uniqueIDTaxCheck<T>(List<T> lst, int id)
        {
            foreach (var item in lst)
            {
                if ((int)item.GetType().GetProperty("id").GetValue(item, null) == id) 
                    return false;
            }
            return true;
        }


        T GetById<T>(List<T> lst, int id) where T : new()
        {
            return lst.Find(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
        }



       



        /// <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        /// 
        //	שליחת רחפן לטעינה בתחנת-בסיס
        public bool TryAddDroneCarge(int droneId)
        {

            //לבדוק אם באמת קיים רחפן עם איידי כזה
            //if (uniqueIDTaxCheck(DataSource.drones, droneId))
            //{
            //    throw new ArgumentException(" An element not exists in the list");
            //}


            //	יש לוודא שתחנת הבסיס פנויה לקבל את הרחפן לטעינה
            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default))
                return false;

            // הוספת רשומה של ישות טעינת סוללת רחפן
            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            DataSource.droneCharges.Add(droneCharge);
            //drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
            return true;
        }







        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        public bool TryRemoveDroneCarge(int droneId)
        {
            if (!DataSource.droneCharges.Any(dc => dc.DroneId == droneId))
                return false;
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);

            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            DataSource.droneCharges.Remove(droneCharge);
            //drone.Status = IDAL.DO.Enum.DroneStatuses.Available;
            return true;
        }
      




       






        public double[] DronePowerConsumptionRequest()
        {
            return (new double[5]{
                DataSource.Config.vacant,
                 DataSource.Config.CarriesLightWeigh,
                  DataSource.Config.CarriesMediumWeigh,
                   DataSource.Config.CarriesHeavyWeight,
                    DataSource.Config.ChargingRatel
                  });
        }
    }
}
