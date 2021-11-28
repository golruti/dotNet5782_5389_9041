using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using System.Device.Location;


namespace IBL
{
    public partial class BL
    {
        /// <summary>
        /// Calculates the distance between two points on the earth
        /// </summary>
        /// <param name="Latitude1">The source point</param>
        /// <param name="Longitude1">The source point</param>
        /// <param name="Latitude2">The Destination point</param>
        /// <param name="Longitude2">The Destination point</param>
        /// <returns></returns>
        private double Distance(double Latitude1, double Latitude2, double Longitude1, double Longitude2)
        {
            var Coord1 = new GeoCoordinate(Latitude1, Longitude2);
            var Coord2 = new GeoCoordinate(Latitude2, Longitude2);
            return Coord1.GetDistanceTo(Coord2);
        }
    }
}
