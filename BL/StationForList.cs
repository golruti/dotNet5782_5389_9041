using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class StationForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableChargingStations { get; set; }
        public int BusyChargingStations { get; set; }
    }
}
