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
        public List<DroneInCharging> DronesInCharging { get; set; }

        public override string ToString()
        {
            return $"------\nStation{Id} : {Name}\ncharge slots={AvailableChargingPorts}\nlocation={Location.Latitude},{Location.Latitude}\n------\n";
        }

        public BaseStation(int id, string name, double longitude, double latitude, int availableChargingStations)
        {
            Id = id;
            Name = name;
            Location = new Location(longitude, latitude);
            AvailableChargingPorts = availableChargingStations;
            DronesInCharging = new List<DroneInCharging>();
        }

        public BaseStation()
        {

        }
    }
}


