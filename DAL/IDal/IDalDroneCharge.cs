using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial interface IDal
    {
        public DroneCharge GetDroneCharge(int droneId);
        public IEnumerable<DroneCharge> GetDronesCharge();
        public int CountFullChargeSlots(int id);
        public List<int> GetDronechargingInStation(int id);
    }
}
