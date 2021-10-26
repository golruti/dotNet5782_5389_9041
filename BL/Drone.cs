using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;


namespace IBL.BO
{
  
        public class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            /// <summary>
            /// String of details for drone
            /// </summary>
            /// <returns>String of details for drone</returns>
            public override string ToString()
            {
            return ("\nId: " + Id + "\nModel: " + Model + "\nMaxWeight: " + MaxWeight +
                "\n\nStatus: " + Status + "\nBattery: " + Battery + "%\n");
            }

            
        }
    
}
