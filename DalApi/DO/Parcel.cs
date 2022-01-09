using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;


namespace DO
{
    /// <summary>
    /// This is a type that represents a parcel that was sent / will be sent to the customer -
    /// Including the details of the parcel.
    /// </summary>
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        //לעשות ערך של -1
        public int Droneld { get; set; } 
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }


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


        public Parcel(int senderId, int targetId, WeightCategories weight, Priorities priority, int droneld, DateTime? requested, DateTime? scheduled, DateTime? pickedUp, DateTime? delivered):this()
        {
            SenderId = senderId;
            TargetId = targetId;
            Weight = weight;
            Priority = priority;
            Droneld = droneld;
            Requested = requested;
            Scheduled = scheduled;
            PickedUp = pickedUp;
            Delivered = delivered;
        }
    }
}

