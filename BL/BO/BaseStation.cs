using System;
using System.Collections.Generic;
namespace IBL.BO
{
    public class BaseStation : ILocatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPorts { get; set; }
        public Location Location { get; set; }
        public IEnumerable<DroneInCharging> DronesInCharging { get; set; }

        public override string ToString()
        {
            return $"Station #{Id}: {Name}, charge slots={AvailableChargingPorts}, location={Location}";
        }

        public BaseStation( int id, string name, double longitude, double latitude, int availableChargingStations)
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


  