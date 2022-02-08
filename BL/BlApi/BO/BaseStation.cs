using System;
using System.Collections.Generic;

namespace BO
{
    public class BaseStation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPorts { get; set; }
        public Location Location { get; set; }
        public IEnumerable<DroneInCharging> DronesInCharging { get; set; }

        public override string ToString()
        {
            return $"------\nStation{Id} : {Name}\ncharge slots={AvailableChargingPorts}\nlocation={Location.Latitude},{Location.Latitude}\n------\n";
        }

    }
}


