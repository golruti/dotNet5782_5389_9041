using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class BaseStationForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingPorts { get; set; }
        public int UsedChargingPorts { get; set; }

        public override string ToString()
        {
            return $"Station #{Id}: {Name}, Available Charging Ports = {AvailableChargingPorts}, Used Charging Ports = {UsedChargingPorts}";
        }
    }
}

