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
                    "\nLattitude: " + Lattitude + "\nChargeSlote: " + ChargeSlote + "\n");
            }

            /// <summary>
            /// The functions create a new instancenew of base-station and copy a deep copy
            /// </summary>
            /// <returns>station</returns>
            public BaseStation Clone()
            {
                return new BaseStation()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Longitude = this.Longitude,
                    Lattitude = this.Lattitude,
                    ChargeSlote = this.ChargeSlote
                };
            }

            public BaseStation(int id, string name,double longitude,double lattitude,int chargeSlote)
            {
                Id = id;
                Name = name;
                Longitude = longitude;
                Lattitude = lattitude;
                ChargeSlote = chargeSlote;
            }
        }
    }
}
