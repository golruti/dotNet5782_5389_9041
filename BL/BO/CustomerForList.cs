using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class CustomerForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int ParcelSentDelivered { get; set; }
        public int ParcelSentNotDelivered { get; set; }
        public int RecievedParcel { get; set; }
        public int ParcelOnWayToCustomer { get; set; }

        public CustomerForList(int id,string name,string phone,int parcelSentDelivered, int parcelSentNotDelivered, int recievedParcel, int parcelOnWayToCustomer)
        {
            Id = id;
            Name = name;
            Phone = phone;
            ParcelSentDelivered = parcelSentDelivered;
            ParcelSentNotDelivered = parcelSentNotDelivered;
            RecievedParcel = recievedParcel;
            ParcelOnWayToCustomer = parcelOnWayToCustomer;
        }
    }
}

