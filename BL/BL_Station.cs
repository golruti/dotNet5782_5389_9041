using System;

namespace IBL.BO
{
    public class BL_Station
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlote { get; set; }


            /// <summary>
            /// String of details for station
            /// </summary>
            /// <returns>String of details for station</returns>
            public override string ToString()
            {
                return ("\nid: " + Id + "\nname: " + Name + "\nLongitude: " + Longitude +
                    "\nLattitude: " + Lattitude + "\nChargeSlote: " + ChargeSlote + "\n");
            }


        }
    
}
