using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BaseStation
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPorts { get; set; }
        public Location Location { get; set; }
        public IEnumerable<DroneInCharging> DronesInCharging { get; set; }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"------\nStation{Id} : {Name}\ncharge slots={AvailableChargingPorts}\nlocation={Location.Latitude},{Location.Latitude}\n------\n";
        }
        #endregion
    }
}


