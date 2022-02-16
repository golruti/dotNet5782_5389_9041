using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Device.Location;


namespace BL
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
        internal double distance(double Latitude1, double Latitude2, double Longitude1, double Longitude2)
        {
            int R = 6371 * 1000; // metres
            double phi1 = Latitude1 * Math.PI / 180; // φ, λ in radians
            double phi2 = Latitude2 * Math.PI / 180;
            double deltaPhi = (Latitude2 - Latitude1) * Math.PI / 180;
            double deltaLambda = (Longitude2 - Longitude1) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                       Math.Cos(phi1) * Math.Cos(phi2) *
                       Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c / 1000; // in kilometres
            return d;
        }
    }
}
