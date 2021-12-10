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
        public void InsertDroneCharge(DroneCharge droneCharge);
        public DroneCharge GetDroneCharge(int droneId);
        public IEnumerable<DroneCharge> GetDronesCharges();
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate);
        public int CountFullChargeSlots(int id);
    }
}
