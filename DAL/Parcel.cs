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
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public int Droneld { get; set; }

            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }

            //זמן יצירת חבילה
            public DateTime Requested { get; set; }
            //זמן שיוך
            public DateTime Scheduled { get; set; }
            //זמן איסוף
            public DateTime PickedUp { get; set; }
            //זמן הגעה
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

            /// <summary>
            /// The functions create a new instance of parsel and copy a deep copy
            /// </summary>
            /// <returns>parsel</returns>
            public Parcel Clone()
            {
                return new Parcel()
                {
                    Id = this.Id,
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

        }
    }
}
