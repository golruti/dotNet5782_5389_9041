using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace BL
{
    class BL : IBL.IBL
    {
        IDal.IDal _dal;
        public BL()
        {
            _dal = new DalObject.DalObject();
            IEnumerable drones = DalObject.GetDrones();



        }


        public BL_Drone CreateLogicDrone(int idDrone)
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
