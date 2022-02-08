using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class DroneForList
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public double Battery { get; set; }
        public DroneStatuses Status { get; set; }
        public Location Location { get; set; }
        public int ParcelDeliveredId { get; set; } = -1;
       


        public override string ToString()
        {
            return $"Drone #{Id}: model={Model}, {Status}, {MaxWeight}, location = {Location.Latitude}, {Location.Longitude}, battery={(int)(Battery)} ";
        }

    }
}
