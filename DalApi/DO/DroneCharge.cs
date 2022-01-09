using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// This is a type that represents the claim of a drone -
    /// Including the details of the loaded drone and the station where it is loaded.
    /// </summary>
    public struct DroneCharge
    {
        public bool IsDeleted { get; set; }
        public int DroneId { get; set; }
        public int StationId { get; set; }
        public DateTime? Time { get; set; }

        public DroneCharge(int droneId, int stationId)
        {
            IsDeleted = false;
            DroneId = droneId;
            StationId = stationId;
            Time = DateTime.Now;
        }
    }
}

