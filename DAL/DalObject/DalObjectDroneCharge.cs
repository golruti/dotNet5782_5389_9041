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
        public void AddDroneCharge(int droneId,int stationId)
        {
            DroneCharge droneCharge = new DroneCharge(droneId, stationId);
            DataSource.droneCharges.Add(droneCharge);
        }

        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);
            if (droneCharge.Equals(default(DroneCharge)))
            {
                throw new Exception();
            }
            return droneCharge;
        }

        //כמה תחנות תפוסות יש בתחנה מסוימת
        public int CountFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSource.droneCharges)
            {
                if (item.StationId == id)
                    ++count;
            }
            return count;
        }

        public void UpdateRelease(int id)
        {
            
            DataSource.droneCharges.RemoveAll(item=>id == item.DroneId);

        }
    }
}
