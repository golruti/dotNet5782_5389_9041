using System;
using System.Collections.Generic;

using System.Linq;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL : IBL
    {
        /// <summary>
        /// The function returns a list of drones loaded at a particular base station
        /// </summary>
        /// <param name="id">The station ID</param>
        /// <returns>The list of drones in charge</returns>
        private List<DroneInCharging> DronesInCharging(int id)
        {
            List<IDAL.DO.DroneCharge> list = (List<IDAL.DO.DroneCharge>)dal.GetDronesCharges(droneCharge => droneCharge.StationId == id);
            if (list.Count == 0)
                return new();
            List<DroneInCharging> droneInChargings = new();
            DroneForList droneToList;
            foreach (var drone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == drone.DroneId));
                if (droneToList != default)
                {
                    droneInChargings.Add(new DroneInCharging() { Id = drone.DroneId, Battery = droneToList.Battery });
                }
            }
            return droneInChargings;
        }
    }
}
