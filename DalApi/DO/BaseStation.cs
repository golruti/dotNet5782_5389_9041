using System;

namespace DO
{
    /// <summary>
    /// This is a type that represents a base station-
    /// It is where the packages are and where the drone is loaded.
    /// </summary>
    public struct BaseStation
    {
        #region properties
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ChargeSlote { get; set; }
        public int AvailableChargingPorts { get; set; }
        #endregion

        #region ToString
        /// <summary>
        /// String of details for station
        /// </summary>
        /// <returns>String of details for station</returns>
        public override string ToString()
        {
            return ("\nid: " + Id + "\nname: " + Name + "\nLongitude: " + Longitude +
                "\nLattitude: " + Latitude + "\nChargeSlote: " + ChargeSlote + "\n");
        }
        #endregion

        #region ctor
        public BaseStation(int id, string name, double longitude, double lattitude, int chargeSlote)
        {
            IsDeleted = false;
            Id = id;
            Name = name;
            Longitude = longitude;
            Latitude = lattitude;
            ChargeSlote = chargeSlote;
            AvailableChargingPorts = chargeSlote;
        }
        #endregion
    }
}

