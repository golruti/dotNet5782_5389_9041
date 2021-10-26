using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{

    public class DroneCharge
    {
        public DroneCharge(int droneId, int stationId)
        {
            DroneId = droneId;
            StationId = stationId;
        }
        public int DroneId { get; set; }
        public int StationId { get; set; }
    }
}