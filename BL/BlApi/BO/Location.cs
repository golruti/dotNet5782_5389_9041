using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Location
    {
        #region properties
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"{Longitude} , {Latitude}";
        }
        #endregion
    }
}
