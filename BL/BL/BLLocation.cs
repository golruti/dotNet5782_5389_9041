using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using IBL.BO;
using static IBL.BO.Enums;


namespace IBL
{
    public partial class BL : IBL
    {
        private double Distance(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            var Coord1 = new GeoCoordinate(Latitude1, Longitude1);
            var Coord2 = new GeoCoordinate(Latitude2, Longitude2);
            return Coord1.GetDistanceTo(Coord2);
        }
    }
    
}
