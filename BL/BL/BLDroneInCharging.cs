using BO;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    partial class BL 
    {
        /// <summary>
        /// The function returns a list of drones loaded at a particular base station
        /// </summary>
        /// <param name="id">The station ID</param>
        /// <returns>The list of drones in charge</returns>
        public List<DroneInCharging> dronesInCharging(int id)
        {
            List<DO.DroneCharge> list = (List<DO.DroneCharge>)dal.GetDronesCharges(droneCharge => droneCharge.StationId == id);
            if (list.Count == 0)
                return new();
            List<DroneInCharging> droneInChargings = new();
            DroneForList droneToList;
            foreach (var drone in list)            {
                droneToList = drones.Values.FirstOrDefault(d => (d.Id == drone.DroneId));
                if (droneToList != default)
                {
                    droneInChargings.Add(new DroneInCharging() { Id = drone.DroneId, Battery = droneToList.Battery });
                }
            }
            return droneInChargings;
        }

        private DO.DroneCharge GetDroneInChargByID(int id)
        {
            return dal.GetDroneCharge(id);
        }
    }
}
