using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;

namespace IBL.BO
{
    class SkimmerInThePackage
    {
        public int Id { get; set; }
        public int Battery { get; set; }
        public Location location { get; set; }
    }
}
