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
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);
            if (droneCharge.Equals(default(DroneCharge)))
            {
                throw new Exception();
            }
            return droneCharge;
        }

        public IEnumerable<DroneCharge> GetDronesCharge()
        {
            return DataSource.droneCharges.Select(drone => drone.Clone()).ToList();
        }

        /// <summary>
        /// Count a number of charging slots occupied at a particular station 
        /// </summary>
        /// <param name="id">the id number of a station</param>
        /// <returns>The counter of empty slots</returns>
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


        public List<int> GetDronechargingInStation(int id)
        {
            List<int> list = new List<int>();
            foreach (var item in DataSource.droneCharges)
            {
                if (item.StationId == id)
                    list.Add(item.DroneId);
            }
            return list;
        }

    }
}
