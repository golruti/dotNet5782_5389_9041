using System;

namespace IDAL
{
    namespace DO
    {
        public struct BaseStation
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
            public int ChargeSlote { get; set; }

            /// <summary>
            /// String of details for station
            /// </summary>
            /// <returns>String of details for station</returns>
            public override string ToString()
            {
                return ("\nid: " + Id + "\nname: " + Name + "\nLongitude: " + Longitude +
                    "\nLattitude: " + Latitude + "\nChargeSlote: " + ChargeSlote + "\n");
            }


            public BaseStation(int id, string name,double longitude,double lattitude,int chargeSlote)
            {
                Id = id;
                Name = name;
                Longitude = longitude;
                Latitude = lattitude;
                ChargeSlote = chargeSlote;
            }
        }
    }
}
