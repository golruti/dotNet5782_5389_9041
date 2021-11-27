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
        public void InsertDroneCharge(int droneId,int stationId)
        {
            DroneCharge droneCharge = new DroneCharge(droneId, stationId);
            DataSource.droneCharges.Add(droneCharge);
        }

        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.First(dc => dc.DroneId == droneId);
            if (droneCharge.GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Get drone -DAL-: There is no suitable customer in data");
            return droneCharge;
        }

        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return DataSource.droneCharges.Select(drone => drone.Clone()).ToList();
        }

        //רשימת האינדקסים לרחפנים הנמצאים בטעינה בתחנה מסוימת
        public List<int> GetDronechargingInStation(int baseStationId)
        {
            List<int> list = new List<int>();
            foreach (var item in DataSource.droneCharges)
            {
                if (item.StationId == baseStationId)
                    list.Add(item.DroneId);
            }
            return list;
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
