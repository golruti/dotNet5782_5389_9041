using BO;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    partial class BL
    {
        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        internal DO.DroneCharge GetDroneInChargByID(int id)
        {
            try
            {
                return dal.GetDroneCharge(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get drone charge -BL-" + ex.Message);
            }
        }
        //---------------------------------------------Show list----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns a list of drones loaded at a particular base station
        /// </summary>
        /// <param name="stationId">The station ID</param>
        /// <returns>The list of drones in charge</returns>
        public IEnumerable<DroneInCharging> GetDronesInCharging(int stationId)
        {
            IEnumerable<DO.DroneCharge> list = dal.GetDronesCharges(droneCharge => droneCharge.IsDeleted ==false && droneCharge.StationId == stationId);
            if (list.Count() == 0)
                return Enumerable.Empty<DroneInCharging>();
            List<DroneInCharging> droneInChargings = new();
            DroneForList droneToList;
            foreach (var drone in list)
            {
                droneToList = drones.FirstOrDefault(d => (d.Id == drone.DroneId));
                if (droneToList != default(DroneForList))
                {
                    droneInChargings.Add(new DroneInCharging() { Id = drone.DroneId, Battery = droneToList.Battery });
                }
            }
            return droneInChargings;
        }

        /// <summary>
        /// The function returns the list drone charges
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DroneInCharging> GetDronesInCharging()
        {
            IEnumerable<DO.DroneCharge> list = dal.GetDronesCharges().Where(dc => dc.IsDeleted == false);
            if (list.Count() == 0)
                return Enumerable.Empty<DroneInCharging>();
            List<DroneInCharging> droneInChargings = new();
            DroneForList droneToList;
            foreach (var drone in list)
            {
                droneToList = drones.FirstOrDefault(d => (d.Id == drone.DroneId));
                if (droneToList != default(DroneForList))
                {
                    droneInChargings.Add(new DroneInCharging() { Id = drone.DroneId, Battery = droneToList.Battery });
                }
            }
            return droneInChargings;
        }
    }
}
