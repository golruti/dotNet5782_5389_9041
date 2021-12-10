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
        public CustomerDelivery CustomerSender { get; set; }
        public CustomerDelivery CustomerReceives { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DroneInParcel DroneParcel { get; set; }
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
            return ("------\nid: " + Id + 
                "\nWeight: " + Weight + "\nPriority: " + Priority + "\nRequested: " + Requested + "\nDroneld: " 
                + "\nScheduled: " + Scheduled + "\nPickedUp: " + PickedUp + "\nDelivered: " + Delivered + "\n------\n");
        }
        public Parcel(int id, int sender,int receiver,WeightCategories weight,Priorities priority,DroneInParcel droneParcel)
        {
            Id = id;
            Weight = weight;
            Priority = priority; 
            DroneParcel = droneParcel;
        }

        public Parcel()
        { }
    }
}

