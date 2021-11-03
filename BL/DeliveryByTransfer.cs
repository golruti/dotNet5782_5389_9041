using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;

namespace IBL.BO
{
    class DeliveryByTransfer
    {
        public int Id { get; set; }
        
        public bool status { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public Location CollectionLocation { get; set; }
        public Location DeliveryLocation { get; set; }


        public double km { get; set; }
    }
}
