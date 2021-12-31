using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Customer : INotifyPropertyChanged
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
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private IEnumerable<ParcelToCustomer> fromCustomer;
        public IEnumerable<ParcelToCustomer> FromCustomer
        {
            get { return fromCustomer; }
            set
            {
                fromCustomer= value;
                OnPropertyChanged(nameof(FromCustomer));
            }
        }

        private IEnumerable<ParcelToCustomer> toCustomer;
        public IEnumerable<ParcelToCustomer> ToCustomer {
            get { return toCustomer; }
            set
            {
                toCustomer = value;
                OnPropertyChanged(nameof(ToCustomer));
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
