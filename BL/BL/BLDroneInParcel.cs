using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {
        /// <summary>
        /// Convert a drone To List to Drone With Parcel
        /// </summary>
        /// <param name="drone">The drone to convert</param>
        /// <returns>The converter drone</returns>
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
