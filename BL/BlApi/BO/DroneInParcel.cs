using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        #region properties
        public int Id { get; set; }
        public double Battery { get; set; }
        public Location Location { get; set; }
        #endregion
    }
}
