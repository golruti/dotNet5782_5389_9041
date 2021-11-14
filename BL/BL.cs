using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DalObject;
using IBL.BO;



namespace IBL
{
    class BL : IBL
    {
        private IDAl.IDal dal;
        private List<DroneForList> drones;
        private static Random rand = new Random();

        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();

            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneForList
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (Enums.WeightCategories)drone.MaxWeight,
                    DeliveryId = 0,
                    Battery = 1

                });
            }

        }


        public Drone CreateLogicDrone(int idDrone)
        {
            var dalDrones = _dal.GetDrones();
            foreach (var drone in dalDrones)
            {

            }
        }







        ///// <summary>
        ///// Assigning a parcel to a drone
        ///// </summary>
        ///// <param name="idxParcel">Id of the parcel</param>
        //public void UpdateParcelScheduled(int idxParcel)
        //{
        //    for (int i = 0; i < DataSource.drones.Count; ++i)
        //    {
        //        if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
        //        {
        //            Parcel p = DataSource.parcels[idxParcel];
        //            p.Scheduled = new DateTime();
        //            p.Droneld = DataSource.drones[i].Id;
        //            DataSource.parcels[idxParcel] = p;

        //            Drone d = DataSource.drones[i];
        //            d.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;

        //            d.MaxWeight = DataSource.parcels[idxParcel].Weight;

        //            DataSource.drones[DataSource.drones.Count] = d;
        //            break;
        //        }
        //    }
        //}
    }
}
