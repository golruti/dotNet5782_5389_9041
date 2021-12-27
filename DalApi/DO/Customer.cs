using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    /// <summary>
    /// This is a type that represents a customer in the shipping company -
    /// Including customer details.
    /// </summary>
    public struct Customer
    {
        #region properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        #endregion

        #region ToString
        /// <summary>
        /// String of details for drone
        /// </summary>
        /// <returns>String of details for drone</returns>
        public override string ToString()
        {
            return ("\nId: " + Id + "\nName: " + Name + "\nPhone: " + Phone +
                "\nLongitude: " + Longitude + "\nLattitude: " + Latitude + "\n");
        }
        #endregion

        #region ctor
        public Customer(int id, string name, string phone, double longitude, double lattitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Longitude = longitude;
            Latitude = lattitude;
        }
        #endregion
    }
}

