using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public CustomerDelivery Sender { get; set; }
        public CustomerDelivery Receiver { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel DroneParcel { get; set; }
        public DateTime Requested { get; set; }
        public DateTime Scheduled { get; set; }
        public DateTime PickedUp { get; set; }
        public DateTime Delivered { get; set; }
        public int IdSender { get; }
        public int IdReceiver { get; }

        /// <summary>
        /// String of details for parsel
        /// </summary>
        /// <returns>String of details for parsel</returns>
        public override string ToString()
        {
            return ("\nid: " + Id + "\nSenderId: " + SenderId + "\nTargetId: " + ReceiverId +
                "\nWeight: " + Weight + "\nRequested: " + Requested + "\nDroneld: " 
                + "\nScheduled: " + Scheduled + "\nPickedUp: " + PickedUp + "\nDelivered: " + Delivered + "\n");
        }
        public Parcel(int id, CustomerDelivery sender,CustomerDelivery receiver,WeightCategories weight,Priorities priority,DroneInParcel droneParcel)
        {
            Id = id;
            Sender = sender;
            Receiver = receiver;
            Weight = weight;
            Priority = priority; 
            DroneParcel = droneParcel;
        }

        public Parcel(int idSender, int idReceiver, WeightCategories weight, Priorities priority)
        {
            IdSender = idSender;
            IdReceiver = idReceiver;
            Weight = weight;
            Priority = priority;
        }

        public Parcel()
        {

        }
    }
}

