using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }


            /// <summary>
            /// String of details for drone
            /// </summary>
            /// <returns>String of details for drone</returns>
            public override string ToString()
            {
                return ("\nId: " + Id + "\nName: " + Name + "\nPhone: " + Phone +
                    "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\n");
            }

            /// <summary>
            /// The functions create a new instance of customer and copy a deep copy
            /// </summary>
            /// <returns>customer</returns>
            public Customer Clone()
            {
                return new Customer()
                {
                    Id = this.Id,
                    Name = this.Name,
                    Phone = this.Phone,
                    Longitude = this.Longitude,
                    Lattitude = this.Lattitude
                };
            }
        }
    }
}
