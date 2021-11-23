using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class DroneInParcel
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location Location { get; set; }

        public DroneInParcel(int id, int battery, double longitude, double latitude)
        {
            Id = id;
            Battery = battery;
            Location = new Location(longitude, latitude);
        }
        public DroneInParcel()
        { }
    }
}
