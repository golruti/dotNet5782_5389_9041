//using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public double Battery { get; set; }
        public Location Location { get; set; }
        public ParcelByTransfer Delivery { get; set; }

        public override string ToString()
        {
            return $"------\nDrone {Id} : {Model}\n{Status}\n{MaxWeight}\nlocation = {Location.Latitude},{Location.Latitude}/nbattery = {(int)(Battery)}%\n------\n ";
        }
    }
}




