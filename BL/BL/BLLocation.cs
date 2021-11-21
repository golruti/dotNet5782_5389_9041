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
        private double Distance(double Latitude1, double Latitude2, double Longitude1, double Longitude2)
        {
            var Coord1 = new GeoCoordinate(Latitude1, Longitude2);
            var Coord2 = new GeoCoordinate(Latitude2, Longitude2);
            return Coord1.GetDistanceTo(Coord2);
        }
    }
}
