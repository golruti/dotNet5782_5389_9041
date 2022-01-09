using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enums;


namespace PO
{
   public class ParcelByTransfer : INotifyPropertyChanged
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

        private CustomerDelivery sender;
        public CustomerDelivery Sender
        {
            get { return sender; }
            set
            {
                sender = value;
                OnPropertyChanged(nameof(Sender));
            }
        }

        private CustomerDelivery target;
        public CustomerDelivery Target
        {
            get { return target; }
            set
            {
                target = value;
                OnPropertyChanged(nameof(Target));
            }
        }

        private Location senderLocation;
        public Location SenderLocation
        {
            get { return senderLocation; }
            set
            {
                senderLocation = value;
                OnPropertyChanged(nameof(SenderLocation));
            }
        }

        private Location targetLocation;
        public Location TargetLocation
        {
            get { return targetLocation; }
            set
            {
                targetLocation = value;
                OnPropertyChanged(nameof(TargetLocation));
            }
        }

        private bool isDestinationParcel;
        public bool IsDestinationParcel
        {
            get { return isDestinationParcel; }
            set
            {
                isDestinationParcel = value;
                OnPropertyChanged(nameof(IsDestinationParcel));
            }
        }

        private double distance;
        public double Distance
        {
            get { return distance; }
            set
            {
                distance = value;
                OnPropertyChanged(nameof(Distance));
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
