using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.Enum;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            private global::IBL.BO.CustomerDelivery sender;
            private global::IBL.BO.CustomerDelivery receiver;
            private object p;
            private DateTime now;
            private int v1;
            private int v2;
            private int v3;

            //public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int Droneld { get; set; } 
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            //public double TargetLongitude { get; set; }
            //public double TargetLattitude { get; set; }
            //public double SenderLongitude { get; set; }
            //public double SenderLattitude { get; set; }

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

            /// <summary>
            /// The functions create a new instance of parsel and copy a deep copy
            /// </summary>
            /// <returns>parsel</returns>
            public Parcel Clone()
            {
                return new Parcel()
                {
                    //Id = this.Id,
                    SenderId = this.SenderId,
                    TargetId = this.TargetId,
                    Weight = this.Weight,
                    Requested = this.Requested,
                    Droneld = this.Droneld,
                    Scheduled = this.Scheduled,
                    PickedUp = this.PickedUp,
                    Delivered = this.Delivered
                };
            }

            public Parcel(/*int id,*/int senderId,int targetId, WeightCategories weight, Priorities priority, int droneld, DateTime requested, DateTime scheduled, DateTime pickedUp ,DateTime delivered)
            {
                //Id = id;
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

            public Parcel(global::IBL.BO.CustomerDelivery sender, global::IBL.BO.CustomerDelivery receiver, WeightCategories weight, Priorities priority, object p, DateTime now, int v1, int v2, int v3) : this()
            {
                this.sender = sender;
                this.receiver = receiver;
                Weight = weight;
                Priority = priority;
                this.p = p;
                this.now = now;
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }
    }
}
