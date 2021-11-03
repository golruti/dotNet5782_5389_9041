using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;

namespace IBL.BO
{
    class PackageInTransfer
    {
        public int Id { get; set; }
        public Priorities Priority { get; set; }
        public ParselStatus status { get; set; }
        public CustomerInDelivery Source { get; set; }
        public CustomerInDelivery Destination { get; set; }
    }
}
