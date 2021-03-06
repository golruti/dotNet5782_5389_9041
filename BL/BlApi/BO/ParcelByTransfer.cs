using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    //חבילה בהעברה
    public class ParcelByTransfer
    {
        #region properties
        public int Id { get; set; }
        public Priorities Priority { get; set; }
        public WeightCategories Weight { get; set; }
        public CustomerDelivery Sender { get; set; }
        public CustomerDelivery Target { get; set; }
        public Location SenderLocation { get; set; }
        public Location TargetLocation { get; set; }
        public bool IsDestinationParcel { get; set; }
        public double Distance { get; set; }
        #endregion
    }
}
