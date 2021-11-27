using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL : IBL
    {
        ////רשימת רחפנים הנמצאים בטעינה בתחנה מסוימת
        //private List<DroneInCharging> DronesInCharging(int id)
        //{
        //    List<int> list = dal.GetDronechargingInStation(id);
        //    if (list.Count == 0)
        //        return new();
        //    List<DroneInCharging> droneInChargings = new();
        //    DroneForList droneToList;
        //    foreach (var idDrone in list)
        //    {
        //        droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
        //        if (droneToList != default)
        //        {
        //            droneInChargings.Add(new DroneInCharging() { Id = idDrone, Battery = droneToList.Battery });
        //        }
        //    }
        //    return droneInChargings;
        //}
    }
}
