using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.Enum;


namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }

            /// <summary>
            /// String of details for skimmer
            /// </summary>
            /// <returns>String of details for skimmer</returns>
            public override string ToString()
            {
                return ("\nId: " + Id + "\nModel: " + Model + "\nMaxWeight: " + MaxWeight + 
                    "\nStatus: " + Status + "\nBattery: " + Battery+"%\n");
            }

            /// <summary>
            /// constructor-create a new drone
            /// </summary>
            /// <returns>drone</returns>
            public Drone Clone()
            {
                return new Drone()
                {
                    Id = this.Id,
                    Model = this.Model,
                    MaxWeight = this.MaxWeight,
                    Status = this.Status,
                    Battery = this.Battery
                };
            }
        }
    }
}
