using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public DateTime? Time { get; set; }

            public DroneCharge(int droneId, int stationId)
            {
                DroneId = droneId;
                StationId = stationId;
                Time = DateTime.Now;
            }

            public DroneCharge Clone()
            {
                return new DroneCharge()
                {
                    DroneId = this.DroneId,
                    StationId = this.StationId,
                };

                
            }
          
        }
    }
}
