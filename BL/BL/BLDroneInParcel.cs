using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL
    {
        private DroneInParcel mapDroneWithParcel(DroneForList drone)
        {
            return new DroneInParcel()
            {
                Id = drone.Id,
                Battery = drone.Battery,
                Location = drone.Location
            };
        }
    }
}
