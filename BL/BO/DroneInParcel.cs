using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    class DroneInDelivery : ILocatable
    {
        public int Id { get; set; }
        public int Battery { get; set; }
        public Location Location { get; set; }
    }
}
