using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class CustomerForList : INotifyPropertyChanged
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

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string phone;
        public string Phone
        {
            get { return Phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private int numParcelSentDelivered;

        public int NumParcelSentDelivered {
            get { return numParcelSentDelivered; }
            set
            {
                numParcelSentDelivered = value;
                OnPropertyChanged(nameof(NumParcelSentDelivered));
            }
        }

        private int numParcelSentNotDelivered;
        public int NumParcelSentNotDelivered {
            get { return numParcelSentNotDelivered; }
            set
            {
                numParcelSentNotDelivered = value;
                OnPropertyChanged(nameof(NumParcelSentNotDelivered));
            }
        }

        private int numParcelReceived;
        public int NumParcelReceived {
            get { return numParcelReceived; }
            set
            {
                numParcelReceived = value;
                OnPropertyChanged(nameof(NumParcelReceived));
            }
        }

        private int numParcelWayToCustomer;
        public int NumParcelWayToCustomer {
            get { return numParcelWayToCustomer; }
            set
            {
                numParcelWayToCustomer = value;
                OnPropertyChanged(nameof(NumParcelWayToCustomer));
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
