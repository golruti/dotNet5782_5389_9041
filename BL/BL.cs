using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;


namespace BL
{
    class BL: IBL
    {
        internal List<Drone> drones = new();
        //למה STATIC
        static BL()
        {
            IDal.IDal DalObject = new DalObject.DalObject();
        }

        /// <summary>
        /// Assigning a parcel to a drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelScheduled(int idxParcel)
        {
            for (int i = 0; i < DataSource.drones.Count; ++i)
            {
                if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
                {
                    Parcel p = DataSource.parcels[idxParcel];
                    p.Scheduled = new DateTime();
                    p.Droneld = DataSource.drones[i].Id;
                    DataSource.parcels[idxParcel] = p;

                    Drone d = DataSource.drones[i];
                    d.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;

                    d.MaxWeight = DataSource.parcels[idxParcel].Weight;

                    DataSource.drones[DataSource.drones.Count] = d;
                    break;
                }
            }
        }
    }
}
