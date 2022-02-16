using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class Parcel
    {
        #region properties
        public int Id { get; set; }
        public CustomerDelivery CustomerSender { get; set; }
        public CustomerDelivery CustomerReceives { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel DroneParcel { get; set; }
        //נוצר
        public DateTime? Requested { get; set; } = null;
        //התחבר לרחפן
        public DateTime? Scheduled { get; set; } = null;
        //נאסף
        public DateTime? PickedUp { get; set; } = null;
        //הגיע ליעד
        public DateTime? Delivered { get; set; } = null;
        #endregion

        #region ToString 
        public override string ToString()
        {
            return ("------\nid: " + Id +
                "\nWeight: " + Weight + "\nPriority: " + Priority + "\nRequested: " + Requested + "\nDroneld: "
                + "\nScheduled: " + Scheduled + "\nPickedUp: " + PickedUp + "\nDelivered: " + Delivered + "\n------\n");
        }
        #endregion
    }
}

