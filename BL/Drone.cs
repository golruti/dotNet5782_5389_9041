using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.Enum;


namespace IBL.BO
{
    public struct Drone
        {
            public int Id { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            /// <summary>
            /// String of details for drone
            /// </summary>
            /// <returns>String of details for drone</returns>
            public override string ToString()
            {
                return ("\nId: " + Id  +
                    "\n\nStatus: " + "%\n");
            }

     
        }
    
}
