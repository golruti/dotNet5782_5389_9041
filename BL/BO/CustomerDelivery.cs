using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class CustomerDelivery
    {
        public int Id { get; set; }
        public Enums.WeightCategories Weight { get; set; }
        public Enums.Priorities Priority { get; set; }
        public Enums.ParselStatus Status { get; set; }
        public int Sender { get; set; }
        public int Target { get; set; }
    }
}

