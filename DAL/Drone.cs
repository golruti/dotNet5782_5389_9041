using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        class Drone
        {
            public int id { get; set; }
            public string model { get; set; }
            public int maxWeight { get; set; }
            public bool status { get; set; }
            public double battery { get; set; }
        }
    }
}
