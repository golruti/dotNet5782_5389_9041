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

        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.First(dc => dc.DroneId == droneId);
            if (droneCharge.GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Get drone -DAL-: There is no suitable customer in data");
            return droneCharge;
        }

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
    }
}
