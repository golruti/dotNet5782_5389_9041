using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enums;

namespace PO
{
   public class Parcel : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private CustomerDelivery customerSender;
        public CustomerDelivery CustomerSender
        {
            get { return customerSender; }
            set
            {
                customerSender = value;
                OnPropertyChanged(nameof(CustomerSender));
            }
        }

        private CustomerDelivery customerReceives;
        public CustomerDelivery CustomerReceives
        {
            get { return customerReceives; }
            set
            {
                customerReceives = value;
                OnPropertyChanged(nameof(CustomerReceives));
            }
        }

        private WeightCategories weight;
        public WeightCategories Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private Priorities priority;
        public Priorities Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        private DroneInParcel droneParcel;
        public DroneInParcel DroneParcel
        {
            get { return droneParcel; }
            set
            {
                droneParcel = value;
                OnPropertyChanged(nameof(DroneParcel));
            }
        }

        private DateTime? requested;
        public DateTime? Requested
        {
            get { return requested; }
            set
            {
                requested = value;
                OnPropertyChanged(nameof(Requested));
            }
        }

        private DateTime? scheduled;
        public DateTime? Scheduled
        {
            get { return scheduled; }
            set
            {
                scheduled = value;
                OnPropertyChanged(nameof(Scheduled));
            }
        }

        private DateTime? pickedUp;
        public DateTime? PickedUp
        {
            get { return pickedUp; }
            set
            {
                pickedUp = value;
                OnPropertyChanged(nameof(PickedUp));
            }
        }

        private DateTime? delivered;
        public DateTime? Delivered
        {
            get { return delivered; }
            set
            {
                delivered = value;
                OnPropertyChanged(nameof(Delivered));
            }
        }
         

        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
