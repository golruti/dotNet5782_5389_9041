using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.Enum;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public CustomerDelivery Sender { get; set; }
        public CustomerDelivery receiver { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel DroneParcel { get; set; }
        public DateTime Requested { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }

        /// <summary>
        /// String of details for parsel
        /// </summary>
        /// <returns>String of details for parsel</returns>
        public override string ToString()
        {
            return ("\nid: " + Id + "\nSenderId: " + SenderId + "\nTargetId: " + TargetId +
                "\nWeight: " + Weight + "\nRequested: " + Requested + "\nDroneld: " + Droneld
                + "\nScheduled: " + Scheduled + "\nPickedUp: " + PickedUp + "\nDelivered: " + Delivered + "\n");
        }


    }
}

