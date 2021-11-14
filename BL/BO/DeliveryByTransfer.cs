using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class DeliveryByTransfer
    {
        public int Id { get; set; }
        public Priorities Priority { get; set; }
        public WeightCategories Weight { get; set; }
        public DeliveryToCustomer Sender { get; set; }
        public DeliveryToCustomer Target { get; set; }
        public Location SenderLocation { get; set; }
        public Location TargetLocation { get; set; }

        public double Distance { get; set; }
    }
}
