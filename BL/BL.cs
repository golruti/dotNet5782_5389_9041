using System;
using System.Collections.Generic;
using System.Linq;
using DalObject;
using IBL.BO;


namespace BL
{
    class BL /*: IBL.IBL*/
    {
        //internal List<Drone> drones = new();
        //internal List<Parcel> parcels = new();
        //internal List<Customer> customers = new();
        //internal List<Station> stations = new();


        public BL()
        {
            IDal.IDal DalObject = new DalObject.DalObject();
        }






        /// <summary>
        /// Assigning a parcel to a drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelScheduled(int idxParcel)
        {
            List<Drone> drones = DalObject.GetDrones();
            for (int i = 0; i < drones.Count(); ++i)
            {
                if (drones[i].Status == IBL.BO.Enum.DroneStatuses.Available)
                {
                    Parcel p = parcels[idxParcel];
                    p.Scheduled = new DateTime();
                    p.Droneld = drones[i].Id;
                    parcels[idxParcel] = p;

                    Drone d = DataSource.drones[i];
                    d.Status = IBL.BO.Enum.DroneStatuses.Maintenance;

                    d.MaxWeight = parcels[idxParcel].Weight;

                    DataSource.drones[DataSource.drones.Count] = d;
                    break;
                }

            }
        }
    




    /// <summary>
    /// Release drone from charging at base station
    /// </summary>
    /// <param name="droneId">Id of the drone</param>
    /// <returns>Returns the mother drone released from charging</returns>
    /// 
    //	צריך להיות בבל ..שחרור רחפן מטעינה בתחנת-בסיס
    public bool TryRemoveDroneCarge(int droneId)
    {
        if (!droneCharges.Any(dc => dc.DroneId == droneId))
            return false;
        var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);

        var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
        if (drone.Equals(default))
            return false;

        DataSource.droneCharges.Remove(droneCharge);
        drone.Status = IDAL.DO.Enum.DroneStatuses.Available;
        return true;
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
        var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
        if (drone.Equals(default))
            return false;

        //	יש לוודא שתחנת הבסיס פנויה לקבל את הרחפן לטעינה
        var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
        if (station.Equals(default))
            return false;

        // הוספת רשומה של ישות טעינת סוללת רחפן
        DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
        DataSource.droneCharges.Add(droneCharge);
        // צריך להעביר לבל..שינוי מצב הרחפן
        drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
        return true;
    }

    /// <summary>
    /// Delivery of a parcel to the destination
    /// </summary>
    /// <param name="idxParcel">Id of the parcel</param>
    /// //אספקת חבילה ליעד
    public void UpdateParcelDelivered(int idParcel)
    {
        for (int i = 0; i < DataSource.parcels.Count; ++i)
        {
            if (DataSource.parcels[i].Id == idParcel)
            {
                //עדכון זמן אספקת חבילה
                Parcel p = DataSource.parcels[i];
                p.Delivered = DateTime.Now;
                DataSource.parcels[i] = p;

                //עדכון מצב הרחפן לפנוי..צריך להעביר לבל
                for (int j = 0; j < DataSource.drones.Count(); ++j)
                {
                    if (DataSource.drones[j].Id == DataSource.parcels[i].Droneld)
                    {
                        Drone d = DataSource.drones[j];
                        d.Status = IDAL.DO.Enum.DroneStatuses.Available;
                        DataSource.drones[j] = d;
                        break;
                    }
                }
            }
            break;
        }
    }

    /// <summary>
    /// Package assembly by drone
    /// </summary>
    /// <param name="idxParcel">Id of the parcel</param>
    public void UpdateParcelPickedUp(int idParcel)
    {
        //אסיפת חבילה עי רחפן
        for (int i = 0; i < DataSource.parcels.Count(); ++i)
        {
            if (DataSource.parcels[i].Id == idParcel)
            {
                //עדכון זמן איסוף
                Parcel p = DataSource.parcels[i];
                p.PickedUp = DateTime.Now;
                DataSource.parcels[i] = p;

                //עדכון מצב הרחפן למשלוח ..צריך להעביר לבל 
                for (int j = 0; j < DataSource.drones.Count(); ++j)
                {
                    if (DataSource.drones[j].Id == DataSource.parcels[i].Droneld)
                    {
                        Drone d = DataSource.drones[j];
                        d.Status = IDAL.DO.Enum.DroneStatuses.Delivery;
                        DataSource.drones[j] = d;
                    }
                }
            }
        }
    }


}
}