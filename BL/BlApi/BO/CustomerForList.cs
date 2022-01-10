using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int NumParcelSentDelivered { get; set; }
        public int NumParcelSentNotDelivered { get; set; }
        public int NumParcelReceived { get; set; }
        public int NumParcelWayToCustomer { get; set; }

        public override string ToString()
        {
            return $"Customer #{Id}: {Name}, Phone{Phone}, Num Parcel Sent Delivered = {NumParcelSentDelivered}, " +
                $"Num Parcel SentN ot Delivered = {NumParcelSentNotDelivered}, Num Parcel Received = {NumParcelReceived}, " +
                $" NumP arcel Way To Customer = {NumParcelWayToCustomer}";
        }

        public CustomerForList(int id, string name, string phone, int parcelSentDelivered, int parcelSentNotDelivered, int recievedParcel, int parcelOnWayToCustomer)
        {
            Id = id;
            Name = name;
            Phone = phone;
            NumParcelSentDelivered = parcelSentDelivered;
            NumParcelSentNotDelivered = parcelSentNotDelivered;
            NumParcelReceived = recievedParcel;
            NumParcelWayToCustomer = parcelOnWayToCustomer;
        }

        public CustomerForList()
        {

        }
    }
}

