using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enums;

namespace PO
{
    public class ParcelForList : INotifyPropertyChanged
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

        private string sendCustomer;
        public string SendCustomer
        {
            get { return sendCustomer; }
            set
            {
                sendCustomer = value;
                OnPropertyChanged(nameof(SendCustomer));
            }
        }

        private string receiveCustomer;
        public string ReceiveCustomer
        {
            get { return receiveCustomer; }
            set
            {
                receiveCustomer = value;
                OnPropertyChanged(nameof(ReceiveCustomer));
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

        private ParcelStatuses status;
        public ParcelStatuses Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
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
